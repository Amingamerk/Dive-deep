using DiveDeep.Data;
using DiveDeep.Models;
using DiveDeep.Persistence;
using DiveDeep.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiveDeep.Services
{
    public class BookingService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IProductRepository productRepository, IBookingRepository bookingRepository)
        {
            _productRepository = productRepository;
            _bookingRepository = bookingRepository;

        }


        public List<Booking> GetAll()
        {
            return _bookingRepository.GetAll();
        }

        public List<Booking> GetByUserId(string id)
        {
            return _bookingRepository.GetByUserId(id);
        }

        public Booking? GetById(int id)
        {
            return _bookingRepository.GetById(id);
        }

        public void Delete(int id)
        {
            _bookingRepository.Delete(id);
        }

        public BundleBooking? GetBundleBookingById(int id)
        {
            return _bookingRepository.GetBundleBookingById(id);
        }

        public void DeleteBundleBooking(int id)
        {
            _bookingRepository.DeleteBundleBooking(id);
        }

        public BookingValidationResult UpdateBooking(Booking booking)
        {
            BookingValidationResult result = ValidateDates(booking.StartTime, booking.EndTime);
            if (result.IsSuccessful == false)
            {
                return result;
            }

            result = CheckAvailableForUpdate(booking);
            if (result.IsSuccessful == false)
            {
                return result;
            }

            _bookingRepository.Update(booking);
            return result;
        }

        public BookingValidationResult UpdateBundleBooking(List<Booking> bookings, DateTime startTime, DateTime endTime)
        {
            BookingValidationResult result = ValidateDates(startTime, endTime);
            if (result.IsSuccessful == false)
            {
                return result;
            }

            // alle produkter i pakken får den samme periode
            foreach (Booking booking in bookings)
            {
                booking.StartTime = startTime;
                booking.EndTime = endTime;

                BookingValidationResult availableResult = CheckAvailableForUpdate(booking);
                if (availableResult.IsSuccessful == false)
                {
                    return availableResult;
                }
            }

            _bookingRepository.UpdateBundleBooking(bookings);
            return result;
        }

        public List<Booking> Filter(List<Booking> bookings, BookingFilterViewModel filter)
        {
            List<Booking> result = bookings;

            if (filter.UserId != null)
            {
                result = result.Where(b => b.UserId == filter.UserId).ToList();
            }

            if (filter.Category != null)
            {
                result = result.Where(b => b.Product.Category == filter.Category).ToList();
            }

            if (filter.Period == "Kommende")
            {
                result = result.Where(b => b.StartTime > DateTime.Today).ToList();
            }
            else if (filter.Period == "Igangværende")
            {
                result = result.Where(b => b.StartTime <= DateTime.Today && b.EndTime >= DateTime.Today).ToList();
            }
            else if (filter.Period == "Afsluttede")
            {
                result = result.Where(b => b.EndTime < DateTime.Today).ToList();
            }

            if (filter.Type == "Enkelt")
            {
                result = result.Where(b => b.BundleBookingId == null).ToList();
            }
            else if (filter.Type == "Pakke")
            {
                result = result.Where(b => b.BundleBookingId != null).ToList();
            }

            return result;
        }

        // som ValidateBooking, men bookingen må gerne overlappe med sig selv
        private BookingValidationResult CheckAvailableForUpdate(Booking booking)
        {
            BookingValidationResult result = new();
            result.IsSuccessful = false;

            Booking? overlappingBooking = _bookingRepository.FindOverlappingBooking(booking.ProductId, booking.StartTime, booking.EndTime, booking.BookingId);
            if (overlappingBooking != null)
            {
                result.Key = "ProductId";
                result.ErrorMessage = "Produktet er desværre ikke tilgængeligt for de valgte datoer";

                // sæt produktets navn foran fejlbeskeden
                Product? product = _productRepository.GetById(booking.ProductId);
                if (product != null)
                {
                    result.ErrorMessage = $"{product.Brand} {product.Model}: {result.ErrorMessage}";
                }
                return result;
            }

            result.IsSuccessful = true;
            return result;
        }

        public BookingValidationResult ValidateDates(DateTime startTime, DateTime endTime)
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

        public BookingValidationResult CheckOverlappingBooking(List<Booking> bookings)
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

        public BookingValidationResult ValidateBooking(Booking booking)
        {
            var result = new BookingValidationResult();
            result.IsSuccessful = false;

            DateTime startTime = booking.StartTime;
            DateTime endTime = booking.EndTime;

            // tjek at datoerne er skrevet korrekt
            BookingValidationResult dateResult = ValidateDates(booking.StartTime, booking.EndTime);
            if (!dateResult.IsSuccessful)
            {
                return dateResult;
            }

            // tjek at produktet er tilgængeligt i hele perioden
            bool isAvailable = _productRepository.IsProductAvailable(
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
        public BookingValidationResult ValidateBundleProducts(Bundle bundle, List<Product> products, DateTime startTime, DateTime endTime)
        {
            BookingValidationResult result = new();
            result.IsSuccessful = false;

            // Der skal være valgt præcis ét produkt til hver del af pakken
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

                if (!_productRepository.IsProductAvailable(product.ProductId, startTime, endTime))
                {
                    result.ErrorMessage = $"{product.Brand} {product.Model} ({product.VariantLabel}) er desværre lige blevet booket. Vælg venligst noget andet.";
                    return result;
                }
            }

            result.IsSuccessful = true;
            return result;
        }

        public BookingValidationResult CreateBookings(List<Booking> bookings, List<BundleBooking> bundleBookings)
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

            result = CheckOverlappingBooking(allBookings);
            if (result.IsSuccessful == false)
            {
                return result;
            }

            foreach (Booking booking in allBookings)
            {
                BookingValidationResult tempBookingValidationResult = ValidateBooking(booking);
                // sæt produktets navn foran fejlbeskeden
                if (tempBookingValidationResult.IsSuccessful == false)
                {
                    Product? product = _productRepository.GetById(booking.ProductId);
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
                _bookingRepository.Add(booking);
            }
            foreach (BundleBooking bundleBooking in bundleBookings)
            {
                _bookingRepository.AddBundleBooking(bundleBooking);
            }

            return result;
        }
    }
}
