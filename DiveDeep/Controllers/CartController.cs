using DiveDeep.Data;
using DiveDeep.Models;
using DiveDeep.Persistence;
using DiveDeep.Services;
using DiveDeep.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DiveDeep.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly BookingService _bookingService;
        private readonly IProductRepository _productRepository;
        private readonly IBundleRepository _bundleRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ICartService cartService,
            BookingService bookingService,
            IProductRepository productRepository,
            IBundleRepository bundleRepository,
            UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _bookingService = bookingService;
            _productRepository = productRepository;
            _bundleRepository = bundleRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            Cart cart = _cartService.GetCart();
            CartViewModel vm = BuildCartViewModel(cart);
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, string dateRange, string? size, string? gender)
        {
            if (!DateRangeParser.TryParse(dateRange, out DateTime startTime, out DateTime endTime))
            {
                TempData["Error"] = "Vælg en gyldig periode";
                return RedirectToAction("Details", "Products", new { id = productId });
            }

            Product? product = _productRepository.GetById(productId);
            if (product == null)
            {
                return NotFound();
            }

            // Produktet lægges kun i kurven. Bookingen gemmes først, når kunden trykker "Book" i kurven
            _cartService.AddItem(product, size, gender, startTime, endTime);
            TempData["Success"] = $"{product.Brand} {product.Model} tilføjet til kurven!";

            return RedirectToAction("Details", "Products", new { id = productId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBundleToCart(int bundleId, string? dateRange, List<int> productIds)
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

            // find produkterne ud fra id'erne
            List<Product> products = new();
            foreach (int productId in productIds)
            {
                Product? product = _productRepository.GetById(productId);
                if (product == null)
                {
                    return RedirectToDetailsWithError(bundleId, dateRange, "Det valgte udstyr passer ikke til pakken");
                }
                products.Add(product);
            }

            // tjek at produkterne passer til pakken og er ledige
            BookingValidationResult bundleResult = _bookingService.ValidateBundleProducts(bundle, products, startTime, endTime);
            if (!bundleResult.IsSuccessful)
            {
                return RedirectToDetailsWithError(bundleId, dateRange, bundleResult.ErrorMessage ?? "Pakken kunne ikke lægges i kurven");
            }

            _cartService.AddBundle(bundle, products, startTime, endTime);
            TempData["Success"] = $"{bundle.Name} er lagt i kurven!";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Book()
        {
            Cart cart = _cartService.GetCart();

            // Find id på den bruger, der er logget ind
            string? userId = _userManager.GetUserId(User);

            // konverter CartItem til booking
            List<Booking> bookings = new();
            foreach (CartItem item in cart.Items)
            {
                bookings.Add(CreateBooking(item, userId));
            }

            // hver pakke bliver til en BundleBooking
            List<BundleBooking> bundleBookings = new();
            foreach (CartBundle cartBundle in cart.Bundles)
            {
                BundleBooking bundleBooking = new();
                bundleBooking.BundleId = cartBundle.BundleId;
                bundleBooking.BundleName = cartBundle.BundleName;
                bundleBooking.DiscountPercent = cartBundle.DiscountPercent;
                bundleBooking.CreatedAt = DateTime.Now;

                foreach (CartItem item in cartBundle.Items)
                {
                    bundleBooking.Bookings.Add(CreateBooking(item, userId));
                }

                bundleBookings.Add(bundleBooking);
            }

            BookingValidationResult bookingValidationResult = _bookingService.CreateBookings(bookings, bundleBookings);
            if (!bookingValidationResult.IsSuccessful)
            {
                TempData["Error"] = bookingValidationResult.ErrorMessage;
                return RedirectToAction(nameof(Index));
            }

            // byg oversigten før kurven tømmes
            CartViewModel confirmation = BuildCartViewModel(cart);
            TempData["Confirmation"] = JsonSerializer.Serialize(confirmation);

            _cartService.ClearCart();

            return RedirectToAction(nameof(Confirmation));
        }

        public IActionResult Confirmation()
        {
            // TempData er tom hvis siden genindlæses
            string? json = TempData["Confirmation"] as string;
            if (json == null)
            {
                return RedirectToAction(nameof(Index));
            }

            CartViewModel? confirmation = JsonSerializer.Deserialize<CartViewModel>(json);
            if (confirmation == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(confirmation);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId, string? size, string? gender)
        {
            _cartService.RemoveItem(productId, size, gender);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult RemoveBundle(int index)
        {
            _cartService.RemoveBundle(index);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            return RedirectToAction(nameof(Index));
        }

        // sender kunden tilbage til pakkesiden med en fejlbesked
        private IActionResult RedirectToDetailsWithError(int bundleId, string? dateRange, string errorMessage)
        {
            TempData["Error"] = errorMessage;
            return RedirectToAction("Details", "Bundles", new { id = bundleId, dateRange = dateRange });
        }

        private Booking CreateBooking(CartItem item, string? userId)
        {
            Booking booking = new();
            booking.ProductId = item.ProductId;
            booking.StartTime = item.StartTime;
            booking.EndTime = item.EndTime;
            booking.UserId = userId;
            return booking;
        }

        // bruges af både kurven og bekræftelsen
        private CartViewModel BuildCartViewModel(Cart cart)
        {
            CartViewModel vm = new();

            // kopier listerne, ellers bliver de tomme når kurven tømmes
            vm.Items = new List<CartItem>(cart.Items);
            vm.Bundles = new List<CartBundle>(cart.Bundles);

            decimal subTotal = 0;
            decimal discount = 0;

            foreach (CartItem item in cart.Items)
            {
                int days = (item.EndTime - item.StartTime).Days;
                subTotal += (decimal)item.PricePerDay * days * item.Quantity;
            }

            // pakker, rabatten trækkes fra til sidst
            foreach (CartBundle cartBundle in cart.Bundles)
            {
                int days = (cartBundle.EndTime - cartBundle.StartTime).Days;

                decimal bundleFullPrice = 0;
                foreach (CartItem item in cartBundle.Items)
                {
                    bundleFullPrice += (decimal)item.PricePerDay * days;
                }

                subTotal += bundleFullPrice;
                discount += bundleFullPrice * (decimal)cartBundle.DiscountPercent / 100;
            }

            vm.SubTotal = subTotal;
            vm.Discount = discount;
            vm.Total = subTotal - discount;

            return vm;
        }
    }
}