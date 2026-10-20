using DiveDeep.Data;
using DiveDeep.Lib.Models;
using DiveDeep.Models;
using DiveDeep.Persistence;
using DiveDeep.Services;
using DiveDeep.Services.HttpServices;
using DiveDeep.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.Controllers
{
    public class BundlesController : Controller
    {
        private readonly IBundleRepository _bundleRepository;
        private readonly IProductHttpService _productHttpService;
        private readonly IBookingHttpService _bookingHttpService;
        private readonly UserManager<ApplicationUser> _userManager;

        public BundlesController(IBundleRepository bundleRepository, IProductHttpService productHttpService, IBookingHttpService bookingHttpService, UserManager<ApplicationUser> userManager)
        {
            _bundleRepository = bundleRepository;
            _productHttpService = productHttpService;
            _bookingHttpService = bookingHttpService;
            _userManager = userManager;
        }

        // Skal rykkes til web api
        private async Task<float> GetCheapestTotalPerDay(Bundle bundle)
        {
            float sum = 0;
            foreach (ProductCategory productCategory in bundle.Categories)
            {
                List<ProductDto> products = await _productHttpService.GetByCategory(productCategory);

                if (products.Count == 0)
                {
                    continue;
                }

                float cheapestPrice = products[0].PricePerDay;
                foreach (ProductDto product in products)
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

        public async Task<IActionResult> Index()
        {
            List<BundleCardViewModel> bundleCardViewModels = new();

            foreach (Bundle bundle in _bundleRepository.GetAll())
            {
                BundleCardViewModel bundleCardViewModel = new();

                bundleCardViewModel.BundleId = bundle.BundleId;
                bundleCardViewModel.Name = bundle.Name;
                bundleCardViewModel.Description = bundle.Description;
                bundleCardViewModel.DiscountPercent = bundle.DiscountPercent;
                float cheapestTotalPerDay = await GetCheapestTotalPerDay(bundle);
                bundleCardViewModel.FromPricePerDay = bundle.ApplyDiscount(cheapestTotalPerDay);

                bundleCardViewModels.Add(bundleCardViewModel);
            }

            return View(bundleCardViewModels);
        }

        public async Task<IActionResult> Details(int id, string? dateRange)
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
            BookingValidationResult dateResult = BookingRules.ValidateDates(startTime, endTime);
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
                List<ProductDto> availableProducts = await _productHttpService.GetAvailableProducts(productCategory, startTime, endTime);
                foreach (ProductDto p in availableProducts)
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
        [Authorize]
        public async Task<IActionResult> Book(int bundleId, string? dateRange, List<int> productIds)
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

            BookingValidationResult dateResult = BookingRules.ValidateDates(startTime, endTime);
            if (!dateResult.IsSuccessful)
            {
                return RedirectToDetailsWithError(bundleId, dateRange, dateResult.ErrorMessage ?? "Ugyldig periode");
            }

            // Tjek ALLE produkter, før vi gemmer noget, så pakken aldrig bliver halvt booket
            BookingValidationResult bundleResult = await _bookingHttpService.ValidateBundleProducts(bundleId, productIds, startTime, endTime);
            if (!bundleResult.IsSuccessful)
            {
                return RedirectToDetailsWithError(bundleId, dateRange, bundleResult.ErrorMessage ?? "Pakken kunne ikke bookes");
            }

            // Alt er i orden: opret pakkebookingen med én booking per produkt
            BundleBookingDto bundleBooking = new();
            bundleBooking.BundleId = bundle.BundleId;
            bundleBooking.BundleName = bundle.Name;
            bundleBooking.DiscountPercent = bundle.DiscountPercent;
            bundleBooking.CreatedAt = DateTime.Now;

            string? userId = _userManager.GetUserId(User);

            foreach (int productId in productIds)
            {
                BookingDto booking = new();
                booking.ProductId = productId;
                booking.StartTime = startTime;
                booking.EndTime = endTime;
                booking.UserId = userId;
                bundleBooking.Bookings.Add(booking);
            }

            // Gemmer pakkebookingen og alle dens bookinger på en gang
            BookingValidationResult result = await _bookingHttpService.CreateBookings(new List<BookingDto>(), new List<BundleBookingDto> { bundleBooking });
            if (!result.IsSuccessful)
            {
                return RedirectToDetailsWithError(bundleId, dateRange, result.ErrorMessage ?? "Pakken kunne ikke bookes");
            }

            return RedirectToAction("Confirmation", new { id = result.CreatedBundleBookingId });
        }

        public async Task<IActionResult> Confirmation(int id)
        {
            BundleBookingDto? bundleBooking = await _bookingHttpService.GetBundleBookingById(id);
            if (bundleBooking == null || bundleBooking.Bookings.Count == 0)
            {
                return NotFound();
            }

            BundleConfirmationViewModel confirmationViewModel = new();
            confirmationViewModel.BundleName = bundleBooking.BundleName;
            confirmationViewModel.DiscountPercent = bundleBooking.DiscountPercent;

            // Alle bookinger i en pakke har samme periode, så vi tager datoerne fra den første
            BookingDto firstBooking = bundleBooking.Bookings[0];
            confirmationViewModel.StartDate = firstBooking.StartTime;
            confirmationViewModel.EndDate = firstBooking.EndTime;
            confirmationViewModel.Days = (firstBooking.EndTime - firstBooking.StartTime).Days;

            float fullPrice = 0;
            foreach (BookingDto booking in bundleBooking.Bookings)
            {
                ProductDto product = booking.Product;

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
