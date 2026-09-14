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
                        variant.Label = $"{p.VariantGroup} - {p.VariantLabel} ({newPricePerDay.ToString("0")}kr/dag)";
                    }
                    else
                    {
                        variant.Label = $"{p.VariantLabel} ({newPricePerDay.ToString("0")}kr/dag)";
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

    }
}
