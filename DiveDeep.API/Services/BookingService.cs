using DiveDeep.API.Models;
using DiveDeep.API.Persistence;
using DiveDeep.Lib.Models;
using Microsoft.EntityFrameworkCore;
namespace DiveDeep.API.Services
{
    public class BookingService : IBookingService
    {
        private const string ConcurrencyErrorMessage = "Bookingen er blevet ændret af en anden, mens du redigerede den. Genindlæs siden og prøv igen.";

        private readonly IProductRepository _productRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IBundleRepository _bundleRepository;

        public BookingService(IProductRepository productRepository, IBookingRepository bookingRepository, IBundleRepository bundleRepository)
        {
            _productRepository = productRepository;
            _bookingRepository = bookingRepository;
            _bundleRepository = bundleRepository;
        }

        // Generelle "værktøjsmetoder" herunder

        public async Task<BookingValidationResult> ValidateDates(DateTime startTime, DateTime endTime)
        {
            BookingValidationResult result = new BookingValidationResult();
            result.IsSuccessful = false;

            // tjek at startdato er før slut dato
            if (startTime >= endTime)
            {
                result.Key = "EndTime";
                result.ErrorMessage = "Slut dato skal være efter start dato";
                return result;
            }

            // tjek at startdato ikke er i fortiden
            if (startTime < DateTime.Today)
            {
                result.Key = "StartTime";
                result.ErrorMessage = "Start dato kan ikke være i fortiden";
                return result;
            }

            result.IsSuccessful = true;
            return result;
        }

        private async Task<BookingValidationResult> CheckOverlappingBooking(List<Booking> bookings)
        {
            BookingValidationResult bookingValidationResult = new();
            bookingValidationResult.IsSuccessful = false;

            // først finder vi produkter som er i kurven to gange
            List<IGrouping<int, Booking>> duplicates = bookings
                .GroupBy(x => x.ProductId)
                .Where(g => g.Count() > 1)
                .ToList();

            foreach (IGrouping<int, Booking> d in duplicates)
            {
                List<Booking> sameProduct = d.ToList();

                // Sammenlign hver booking med alle bookinger efter den
                for (int i = 0; i < sameProduct.Count; i++)
                {
                    for (int j = i + 1; j < sameProduct.Count; j++)
                    {
                        Booking firstBooking = sameProduct[i];
                        Booking secondBooking = sameProduct[j];

                        // To perioder overlapper, hvis den ene starter før den anden slutter og den ene slutter efter den anden starter
                        bool startsBeforeOtherEnds = firstBooking.StartTime < secondBooking.EndTime;
                        bool endsAfterOtherStarts = firstBooking.EndTime > secondBooking.StartTime;

                        if (startsBeforeOtherEnds && endsAfterOtherStarts)
                        {
                            bookingValidationResult.Key = "ProductId";
                            bookingValidationResult.ErrorMessage = "Det samme produkt ligger flere gange i kurven med overlappende datoer";
                            return bookingValidationResult;
                        }
                    }
                }
            }

            // Ingen overlap fundet
            bookingValidationResult.IsSuccessful = true;
            return bookingValidationResult;
        }

        private async Task<BookingValidationResult> ValidateBooking(Booking booking)
        {
            var result = new BookingValidationResult();
            result.IsSuccessful = false;

            DateTime startTime = booking.StartTime;
            DateTime endTime = booking.EndTime;

            // tjek at datoerne er skrevet korrekt
            BookingValidationResult dateResult = await ValidateDates(booking.StartTime, booking.EndTime);
            if (!dateResult.IsSuccessful)
            {
                return dateResult;
            }

            // tjek at produktet er tilgængeligt i hele perioden
            bool isAvailable = await _productRepository.IsProductAvailable(
                booking.ProductId,
                startTime,
                endTime
            );

            if (!isAvailable)
            {
                result.Key = "ProductId";
                result.ErrorMessage = "Produktet er desværre ikke tilgængeligt for de valgte datoer";
                return result;
            }

            result.IsSuccessful = true;
            return result;
        }

        // Tjekker at hvert produkt passer til sin plads i pakken og er ledigt i perioden
        public async Task<BookingValidationResult> ValidateBundleProducts(int bundleId, List<int> productIds, DateTime startTime, DateTime endTime)
        {
            BookingValidationResult result = await ValidateDates(startTime, endTime);
            if (result.IsSuccessful == false)
            {
                return result;
            }
            result.IsSuccessful = false;

            // Få fat i produkterne fra listen
            List<Product> products = new();
            foreach (int id in productIds)
            {
                Product? product = await _productRepository.GetById(id);
                if (product != null)
                {
                    products.Add(product);
                }
            }

            var bundle = _bundleRepository.GetById(bundleId);
            if (bundle == null)
            {
                result.ErrorMessage = "Valgte pakke findes ikke";
                return result;
            }

            // Der skal være valgt præcis ét produkt til hver del af pakken
            // Vi sammenligner bare antal på hver, de burde være ens
            if (products.Count != bundle.Categories.Count)
            {
                result.ErrorMessage = "Vælg udstyr til alle dele af pakken";
                return result;
            }

            for (int i = 0; i < products.Count; i++)
            {
                Product product = products[i];

                // Dropdowns kommer i samme rækkefølge som pakkens kategorier
                if (product.Category != bundle.Categories[i])
                {
                    result.ErrorMessage = "Det valgte udstyr passer ikke til pakken";
                    return result;
                }
                bool available = await _productRepository.IsProductAvailable(product.ProductId, startTime, endTime);
                if (!available)
                {
                    result.ErrorMessage = $"{product.Brand} {product.Model} ({product.VariantLabel}) er desværre lige blevet booket. Vælg venligst noget andet.";
                    return result;
                }
            }

            result.IsSuccessful = true;
            return result;
        }


        // API metoder herunder

        public async Task<List<Booking>> GetAll()
        {
            return await _bookingRepository.GetAll();
        }

        public async Task<List<Booking>> GetByUserId(string id)
        {
            return await _bookingRepository.GetByUserId(id);
        }

        public async Task<Booking?> GetById(int id)
        {
            return await _bookingRepository.GetById(id);
        }

        public async Task Delete(int id)
        {
            await _bookingRepository.Delete(id);
        }

        public async Task<BundleBooking?> GetBundleBookingById(int id)
        {
            return await _bookingRepository.GetBundleBookingById(id);
        }

        public async Task DeleteBundleBooking(int id)
        {
            await _bookingRepository.DeleteBundleBooking(id);
        }

        public async Task<BookingValidationResult> UpdateBooking(Booking booking)
        {
            BookingValidationResult result = await ValidateDates(booking.StartTime, booking.EndTime);
            if (result.IsSuccessful == false)
            {
                return result;
            }

            result = await CheckAvailableForUpdate(booking);
            if (result.IsSuccessful == false)
            {
                return result;
            }

            try
            {
                await _bookingRepository.Update(booking);
            }
            catch (DbUpdateConcurrencyException)
            {
                result.IsSuccessful = false;
                result.ErrorMessage = ConcurrencyErrorMessage;
            }

            return result;
        }

        public async Task<BookingValidationResult> UpdateBundleBooking(List<Booking> bookings, DateTime startTime, DateTime endTime)
        {
            BookingValidationResult result = await ValidateDates(startTime, endTime);
            if (result.IsSuccessful == false)
            {
                return result;
            }

            // alle produkter i pakken får den samme periode
            foreach (Booking booking in bookings)
            {
                booking.StartTime = startTime;
                booking.EndTime = endTime;

                BookingValidationResult availableResult = await CheckAvailableForUpdate(booking);
                if (availableResult.IsSuccessful == false)
                {
                    return availableResult;
                }
            }

            try
            {
                await _bookingRepository.UpdateBundleBooking(bookings);
            }
            catch (DbUpdateConcurrencyException)
            {
                result.IsSuccessful = false;
                result.ErrorMessage = ConcurrencyErrorMessage;
            }

            return result;
        }

        // som ValidateBooking, men bookingen må gerne overlappe med sig selv
        private async Task<BookingValidationResult> CheckAvailableForUpdate(Booking booking)
        {
            BookingValidationResult result = new();
            result.IsSuccessful = false;

            Booking? overlappingBooking = await _bookingRepository.FindOverlappingBooking(booking.ProductId, booking.StartTime, booking.EndTime, booking.BookingId);
            if (overlappingBooking != null)
            {
                result.Key = "ProductId";
                result.ErrorMessage = "Produktet er desværre ikke tilgængeligt for de valgte datoer";

                // sæt produktets navn foran fejlbeskeden
                Product? product = await _productRepository.GetById(booking.ProductId);
                if (product != null)
                {
                    result.ErrorMessage = $"{product.Brand} {product.Model}: {result.ErrorMessage}";
                }
                return result;
            }

            result.IsSuccessful = true;
            return result;
        }


        public async Task<BookingValidationResult> CreateBookings(List<Booking> bookings, List<BundleBooking> bundleBookings)
        {
            BookingValidationResult result = new();

            // Saml alle bookinger i en samlet liste, så enkelte produkter og pakker tjekkes sammen
            List<Booking> allBookings = new();
            allBookings.AddRange(bookings);
            foreach (BundleBooking bundleBooking in bundleBookings)
            {
                allBookings.AddRange(bundleBooking.Bookings);
            }

            if (allBookings.Count == 0)
            {
                result.IsSuccessful = false;
                result.ErrorMessage = "Din kurv er tom";
                result.Key = "CartEmpty";
                return result;
            }

            result = await CheckOverlappingBooking(allBookings);
            if (result.IsSuccessful == false)
            {
                return result;
            }

            foreach (Booking booking in allBookings)
            {
                BookingValidationResult tempBookingValidationResult = await ValidateBooking(booking);
                // sæt produktets navn foran fejlbeskeden
                if (tempBookingValidationResult.IsSuccessful == false)
                {
                    Product? product = await _productRepository.GetById(booking.ProductId);
                    if (product != null)
                    {
                        tempBookingValidationResult.ErrorMessage = $"{product.Brand} {product.Model}: {tempBookingValidationResult.ErrorMessage}";
                    }
                    return tempBookingValidationResult;
                }
            }

            // Alt er i orden: så vi gemmer enkelte bookinger og pakkebookinger
            foreach (Booking booking in bookings)
            {
                await _bookingRepository.Add(booking);
            }
            foreach (BundleBooking bundleBooking in bundleBookings)
            {
                await _bookingRepository.AddBundleBooking(bundleBooking);
            }

            // en pakke bookes alene fra pakkesiden, og så skal web appen bruge id'et til bekræftelsen
            if (bookings.Count == 0 && bundleBookings.Count == 1)
            {
                result.CreatedBundleBookingId = bundleBookings[0].BundleBookingId;
            }

            return result;
        }
    }
}
