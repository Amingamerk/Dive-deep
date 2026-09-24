using DiveDeep.Data;
using DiveDeep.Models;
using DiveDeep.Persistence;
using DiveDeep.Services;
using DiveDeep.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiveDeep.Areas.Admin.Controllers
{
    public class BookingsController : AdminBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly BookingService _bookingService;
        private readonly IProductRepository _productRepository;

        private const string ConcurrencyErrorMessage = "Bookingen er blevet ændret af en anden, mens du redigerede den. Genindlæs siden og prøv igen.";

        public BookingsController(UserManager<ApplicationUser> userManager, BookingService bookingService, IProductRepository productRepository)
        {
            _userManager = userManager;
            _bookingService = bookingService;
            _productRepository = productRepository;

        }

        public IActionResult Index([FromQuery] BookingFilterViewModel filter)
        {
            filter.Users = _userManager.Users.OrderBy(u => u.Email).ToList();

            // alle bookinger, nyeste først
            List<Booking> bookings = _bookingService.GetAll()
                .OrderByDescending(b => b.BookingId)
                .ToList();

            filter.Bookings = _bookingService.Filter(bookings, filter);

            return View(filter);
        }

        public IActionResult Edit(int id)
        {
            Booking? booking = _bookingService.GetById(id);
            if (booking == null)
            {
                return NotFound();
            }

            // en del af en pakke skal redigeres sammen med resten af pakken
            if (booking.BundleBookingId != null)
            {
                return RedirectToAction(nameof(EditBundle), new { id = booking.BundleBookingId });
            }

            BookingEditViewModel vm = new();
            vm.Booking = booking;
            FillEditViewModel(vm);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookingEditViewModel vm)
        {
            // udfyldes før vi gemmer, så viewet kan vises igen hvis det fejler
            FillEditViewModel(vm);

            if (ModelState.IsValid == false)
            {
                return View(vm);
            }

            try
            {
                BookingValidationResult result = _bookingService.UpdateBooking(vm.Booking);
                if (result.IsSuccessful == false)
                {
                    ModelState.AddModelError("", result.ErrorMessage ?? "Bookingen kunne ikke gemmes");
                    return View(vm);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", ConcurrencyErrorMessage);
                return View(vm);
            }

            TempData["Success"] = $"Booking #{vm.Booking.BookingId} er gemt";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Booking? booking = _bookingService.GetById(id);
            if (booking == null)
            {
                return NotFound();
            }

            if (booking.BundleBookingId != null)
            {
                TempData["Error"] = "Bookingen er en del af en pakke. Slet hele pakken i stedet.";
                return RedirectToAction(nameof(Index));
            }

            _bookingService.Delete(id);
            TempData["Success"] = $"Booking #{id} er slettet";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditBundle(int id)
        {
            BundleBooking? bundleBooking = _bookingService.GetBundleBookingById(id);
            if (bundleBooking == null || bundleBooking.Bookings.Count == 0)
            {
                return NotFound();
            }

            BundleBookingEditViewModel vm = new();
            vm.BundleBookingId = bundleBooking.BundleBookingId;

            // alle bookinger i pakken har samme periode
            vm.StartTime = bundleBooking.Bookings[0].StartTime;
            vm.EndTime = bundleBooking.Bookings[0].EndTime;

            foreach (Booking booking in bundleBooking.Bookings)
            {
                BundleBookingItemViewModel item = new();
                item.BookingId = booking.BookingId;
                item.ProductId = booking.ProductId;
                item.RowVersion = booking.RowVersion;
                vm.Items.Add(item);
            }

            FillBundleViewModel(vm, bundleBooking);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBundle(BundleBookingEditViewModel vm)
        {
            BundleBooking? bundleBooking = _bookingService.GetBundleBookingById(vm.BundleBookingId);
            if (bundleBooking == null)
            {
                return NotFound();
            }

            // udfyldes før vi gemmer, så viewet kan vises igen hvis det fejler
            FillBundleViewModel(vm, bundleBooking);

            if (ModelState.IsValid == false)
            {
                return View(vm);
            }

            // lav formens felter om til bookinger
            List<Booking> bookings = new();
            foreach (BundleBookingItemViewModel item in vm.Items)
            {
                Booking booking = new();
                booking.BookingId = item.BookingId;
                booking.ProductId = item.ProductId;
                booking.RowVersion = item.RowVersion;
                bookings.Add(booking);
            }

            try
            {
                BookingValidationResult result = _bookingService.UpdateBundleBooking(bookings, vm.StartTime, vm.EndTime);
                if (result.IsSuccessful == false)
                {
                    ModelState.AddModelError("", result.ErrorMessage ?? "Pakken kunne ikke gemmes");
                    return View(vm);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", ConcurrencyErrorMessage);
                return View(vm);
            }

            TempData["Success"] = $"{vm.BundleName} #{vm.BundleBookingId} er gemt";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBundle(int id)
        {
            _bookingService.DeleteBundleBooking(id);
            TempData["Success"] = $"Pakke #{id} er slettet";

            return RedirectToAction(nameof(Index));
        }

        // felter der kun vises, og derfor ikke kommer med tilbage fra formen
        private void FillEditViewModel(BookingEditViewModel vm)
        {
            Booking? booking = _bookingService.GetById(vm.Booking.BookingId);
            if (booking == null) return;

            vm.ProductName = $"{booking.Product.Brand} {booking.Product.Model}";
            vm.UserEmail = booking.User.Email ?? "";
            vm.Variants = _productRepository.GetVariants(booking.Product.Brand, booking.Product.Model ?? "");
        }

        private void FillBundleViewModel(BundleBookingEditViewModel vm, BundleBooking bundleBooking)
        {
            vm.BundleName = bundleBooking.BundleName;
            vm.UserEmail = bundleBooking.Bookings[0].User.Email ?? "";

            foreach (BundleBookingItemViewModel item in vm.Items)
            {
                Booking? booking = bundleBooking.Bookings.FirstOrDefault(b => b.BookingId == item.BookingId);
                if (booking == null) continue;

                item.CategoryTitle = CategoryInfo.Categories[booking.Product.Category].Title;
                item.ProductName = $"{booking.Product.Brand} {booking.Product.Model}";
                item.Variants = _productRepository.GetVariants(booking.Product.Brand, booking.Product.Model ?? "");
            }
        }
    }
}
