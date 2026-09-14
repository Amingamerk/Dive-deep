using Microsoft.AspNetCore.Mvc;
using DiveDeep.Persistence;
using DiveDeep.Models;
using DiveDeep.Services;
using DiveDeep.ViewModels;

namespace DiveDeep.Controllers
{
    public class BundlesController : Controller
    {
        private readonly IBundleRepository _bundleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly BookingService _bookingService;

        public BundlesController(IBundleRepository bundleRepository, IProductRepository productRepository, IBookingRepository bookingRepository, BookingService bookingService)
        {
            _bundleRepository = bundleRepository;
            _productRepository = productRepository;
            _bookingRepository = bookingRepository;
            _bookingService = bookingService;
        }

        private float GetCheapestTotalPerDay(Bundle bundle)
        {
            float sum = 0;
            foreach (ProductCategory productCategory in bundle.Categories)
            {
                List<Product> products = _productRepository.GetByCategory(productCategory);

                if (products.Count == 0)
                {
                    continue;
                }

                float cheapestPrice = products[0].PricePerDay;
                foreach (Product product in products)
                {
                    if (product.PricePerDay < cheapestPrice)
                    {
                        cheapestPrice = product.PricePerDay;
                    }
                }
                sum += cheapestPrice;
            }

            return sum;
        }

        // Sender kunden tilbage til pakkesiden med en fejlbesked.
        // TempData bruges, fordi ModelState forsvinder ved en redirect
        private IActionResult RedirectToDetailsWithError(int bundleId, string? dateRange, string errorMessage)
        {
            TempData["Error"] = errorMessage;
            return RedirectToAction("Details", new { id = bundleId, dateRange = dateRange });
        }

        public IActionResult Index()
        {
            List<BundleCardViewModel> bundleCardViewModels = new();

            foreach (Bundle bundle in _bundleRepository.GetAll())
            {
                BundleCardViewModel bundleCardViewModel = new();

                bundleCardViewModel.BundleId = bundle.BundleId;
                bundleCardViewModel.Name = bundle.Name;
                bundleCardViewModel.Description = bundle.Description;
                bundleCardViewModel.DiscountPercent = bundle.DiscountPercent;
                bundleCardViewModel.FromPricePerDay = bundle.ApplyDiscount(GetCheapestTotalPerDay(bundle));

                bundleCardViewModels.Add(bundleCardViewModel);
            }

            return View(bundleCardViewModels);
        }

        public IActionResult Details(int id, string? dateRange)
        {
            BundleViewModel bundleViewModel = new();

            Bundle? bundle = _bundleRepository.GetById(id);
            if (bundle == null)
            {
                return NotFound();
            }

            bundleViewModel.BundleId = bundle.BundleId;
            bundleViewModel.Name = bundle.Name;
            bundleViewModel.DiscountPercent = bundle.DiscountPercent;
            bundleViewModel.AllSlotsAvailable = true;
            bundleViewModel.DateRange = dateRange;
            if (string.IsNullOrWhiteSpace(dateRange))
            {
                return View(bundleViewModel);
            }

            // Datoerne kunne ikke læses
            if (!DateRangeParser.TryParse(dateRange, out DateTime startTime, out DateTime endTime))
            {
                ModelState.AddModelError("DateRange", "Vælg en gyldig periode");
                return View(bundleViewModel);
            }

            // Datoerne kunne læses, men følger ikke reglerne
            BookingValidationResult dateResult = _bookingService.ValidateDates(startTime, endTime);
            if (!dateResult.IsSuccessful)
            {
                ModelState.AddModelError("DateRange", dateResult.ErrorMessage ?? "Ugyldig periode");
                return View(bundleViewModel);
            }

            // alt gik godt og vi kan nu bruge datoerne
            bundleViewModel.StartDate = startTime;
            bundleViewModel.EndDate = endTime;

            foreach (ProductCategory productCategory in bundle.Categories)
            {
                BundleSlotViewModel bundleSlotViewModel = new();
                bundleSlotViewModel.Title = CategoryInfo.Categories[productCategory].Title;
                List<Product> availableProducts = _productRepository.GetAvailableProducts(productCategory, startTime, endTime);
                foreach (Product p in availableProducts)
                {
                    ProductVariantViewModel variant = new();
                    variant.ProductId = p.ProductId;
                    float newPricePerDay = bundle.ApplyDiscount(p.PricePerDay);
                    if (p.VariantGroup != null)
                    {
                        variant.Label = $"{p.VariantGroup} - {p.VariantLabel} ({newPricePerDay.ToString("0")} kr/dag)";
                    }
                    else
                    {
                        variant.Label = $"{p.VariantLabel} ({newPricePerDay.ToString("0")} kr/dag)";
                    }
                    variant.Group = $"{p.Brand} {p.Model}";
                    bundleSlotViewModel.Options.Add(variant);
                }
                if (bundleSlotViewModel.Options.Count == 0)
                {
                    bundleViewModel.AllSlotsAvailable = false;
                }
                bundleViewModel.Slots.Add(bundleSlotViewModel);
            }
            return View(bundleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Book(int bundleId, string? dateRange, List<int> productIds)
        {
            Bundle? bundle = _bundleRepository.GetById(bundleId);
            if (bundle == null)
            {
                return NotFound();
            }

            // Datoerne kommer fra et skjult felt, så de tjekkes igen her
            if (!DateRangeParser.TryParse(dateRange, out DateTime startTime, out DateTime endTime))
            {
                return RedirectToDetailsWithError(bundleId, dateRange, "Vælg en gyldig periode");
            }

            BookingValidationResult dateResult = _bookingService.ValidateDates(startTime, endTime);
            if (!dateResult.IsSuccessful)
            {
                return RedirectToDetailsWithError(bundleId, dateRange, dateResult.ErrorMessage ?? "Ugyldig periode");
            }

            // Der skal være valgt præcis ét produkt til hver del af pakken
            if (productIds.Count != bundle.Categories.Count)
            {
                return RedirectToDetailsWithError(bundleId, dateRange, "Vælg udstyr til alle dele af pakken");
            }

            // Tjek ALLE produkter, før vi gemmer noget, så pakken aldrig bliver halvt booket
            for (int i = 0; i < productIds.Count; i++)
            {
                Product? product = _productRepository.GetById(productIds[i]);

                // Dropdowns kommer i samme rækkefølge som pakkens kategorier,
                // så produkt nummer i skal høre til kategori nummer i
                if (product == null || product.Category != bundle.Categories[i])
                {
                    return RedirectToDetailsWithError(bundleId, dateRange, "Det valgte udstyr passer ikke til pakken");
                }

                // Nogen kan have booket produktet, mens kunden valgte udstyr
                if (!_productRepository.IsProductAvailable(product.ProductId, startTime, endTime))
                {
                    return RedirectToDetailsWithError(bundleId, dateRange, $"{product.Brand} {product.Model} ({product.VariantLabel}) er desværre lige blevet booket. Vælg venligst noget andet.");
                }
            }

            // Alt er i orden: opret pakkebookingen med én booking per produkt
            BundleBooking bundleBooking = new();
            bundleBooking.BundleId = bundle.BundleId;
            bundleBooking.BundleName = bundle.Name;
            bundleBooking.DiscountPercent = bundle.DiscountPercent;
            bundleBooking.CreatedAt = DateTime.Now;

            foreach (int productId in productIds)
            {
                Booking booking = new();
                booking.ProductId = productId;
                booking.StartTime = startTime;
                booking.EndTime = endTime;
                bundleBooking.Bookings.Add(booking);
            }

            // Gemmer pakkebookingen og alle dens bookinger på en gang
            _bookingRepository.AddBundleBooking(bundleBooking);

            return RedirectToAction("Confirmation", new { id = bundleBooking.BundleBookingId });
        }

        public IActionResult Confirmation(int id)
        {
            BundleBooking? bundleBooking = _bookingRepository.GetBundleBookingById(id);
            if (bundleBooking == null || bundleBooking.Bookings.Count == 0)
            {
                return NotFound();
            }

            BundleConfirmationViewModel confirmationViewModel = new();
            confirmationViewModel.BundleName = bundleBooking.BundleName;
            confirmationViewModel.DiscountPercent = bundleBooking.DiscountPercent;

            // Alle bookinger i en pakke har samme periode, så vi tager datoerne fra den første
            Booking firstBooking = bundleBooking.Bookings[0];
            confirmationViewModel.StartDate = firstBooking.StartTime;
            confirmationViewModel.EndDate = firstBooking.EndTime;
            confirmationViewModel.Days = (firstBooking.EndTime - firstBooking.StartTime).Days;

            float fullPrice = 0;
            foreach (Booking booking in bundleBooking.Bookings)
            {
                Product product = booking.Product;

                // Dragter har også køn og tykkelse, fx "Herre – 5 mm"
                string variantText = product.VariantLabel;
                if (product.VariantGroup != null)
                {
                    variantText = $"{product.VariantGroup} - {product.VariantLabel}";
                }

                confirmationViewModel.ProductLabels.Add($"{product.Brand} {product.Model} ({variantText})");
                fullPrice += product.PricePerDay * confirmationViewModel.Days;
            }

            confirmationViewModel.FullPrice = fullPrice;

            // Brug rabatten, der blev gemt ved bookingen, og ikke pakkens nuværende rabat
            confirmationViewModel.TotalPrice = fullPrice * (100 - bundleBooking.DiscountPercent) / 100;

            return View(confirmationViewModel);
        }
    }
}
