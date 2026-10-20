using DiveDeep.Data;
using DiveDeep.Lib.Models;
using DiveDeep.Models;
using DiveDeep.Services.HttpServices;
using DiveDeep.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiveDeep.Areas.Admin.Controllers
{
    public class BookingsController : AdminBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IBookingHttpService _bookingHttpService;

        private readonly IProductHttpService _productHttpService;

        private const string ConcurrencyErrorMessage = "Bookingen er blevet ændret af en anden, mens du redigerede den. Genindlæs siden og prøv igen.";

        public BookingsController(UserManager<ApplicationUser> userManager, IBookingHttpService bookingHttpService, IProductHttpService productHttpService)
        {
            _userManager = userManager;
            _bookingHttpService = bookingHttpService;
            _productHttpService = productHttpService;
        }

        public async Task<IActionResult> Index([FromQuery] BookingFilterViewModel filter)
        {
            filter.Users = _userManager.Users.OrderBy(u => u.Email).ToList();

            // alle bookinger, nyeste først
            List<BookingDto> bookings = await _bookingHttpService.GetAll();

            filter.Bookings = bookings
                .OrderByDescending(b => b.BookingId)
                .ToList();

            filter.Bookings = filter.Apply();

            return View(filter);
        }

        public async Task<IActionResult> Edit(int id)
        {
            BookingDto? booking = await _bookingHttpService.GetById(id);
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
            await FillEditViewModel(vm);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookingEditViewModel vm)
        {
            // udfyldes før vi gemmer, så viewet kan vises igen hvis det fejler
            await FillEditViewModel(vm);

            if (ModelState.IsValid == false)
            {
                return View(vm);
            }

            try
            {
                BookingValidationResult result = await _bookingHttpService.UpdateBooking(vm.Booking);
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
        public async Task<IActionResult> Delete(int id)
        {
            BookingDto? booking = await _bookingHttpService.GetById(id);
            if (booking == null)
            {
                return NotFound();
            }

            if (booking.BundleBookingId != null)
            {
                TempData["Error"] = "Bookingen er en del af en pakke. Slet hele pakken i stedet.";
                return RedirectToAction(nameof(Index));
            }

            await _bookingHttpService.Delete(id);
            TempData["Success"] = $"Booking #{id} er slettet";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditBundle(int id)
        {
            BundleBookingDto? bundleBooking = await _bookingHttpService.GetBundleBookingById(id);
            if (bundleBooking == null || bundleBooking.Bookings.Count == 0)
            {
                return NotFound();
            }

            BundleBookingEditViewModel vm = new();
            vm.BundleBookingId = bundleBooking.BundleBookingId;

            // alle bookinger i pakken har samme periode
            vm.StartTime = bundleBooking.Bookings[0].StartTime;
            vm.EndTime = bundleBooking.Bookings[0].EndTime;

            foreach (BookingDto booking in bundleBooking.Bookings)
            {
                BundleBookingItemViewModel item = new();
                item.BookingId = booking.BookingId;
                item.ProductId = booking.ProductId;
                item.RowVersion = booking.RowVersion;
                vm.Items.Add(item);
            }

            await FillBundleViewModel(vm, bundleBooking);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBundle(BundleBookingEditViewModel vm)
        {
            BundleBookingDto? bundleBooking = await _bookingHttpService.GetBundleBookingById(vm.BundleBookingId);
            if (bundleBooking == null)
            {
                return NotFound();
            }

            // udfyldes før vi gemmer, så viewet kan vises igen hvis det fejler
            await FillBundleViewModel(vm, bundleBooking);

            if (ModelState.IsValid == false)
            {
                return View(vm);
            }

            // lav formens felter om til bookinger
            List<BookingDto> bookings = new();
            foreach (BundleBookingItemViewModel item in vm.Items)
            {
                BookingDto booking = new();
                booking.BookingId = item.BookingId;
                booking.ProductId = item.ProductId;
                booking.RowVersion = item.RowVersion;
                bookings.Add(booking);
            }

            try
            {
                BookingValidationResult result = await _bookingHttpService.UpdateBundleBooking(bookings, vm.StartTime, vm.EndTime);
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
        public async Task<IActionResult> DeleteBundle(int id)
        {
            await _bookingHttpService.DeleteBundleBooking(id);
            TempData["Success"] = $"Pakke #{id} er slettet";

            return RedirectToAction(nameof(Index));
        }

        // felter der kun vises, og derfor ikke kommer med tilbage fra formen
        private async Task FillEditViewModel(BookingEditViewModel vm)
        {
            BookingDto? booking = await _bookingHttpService.GetById(vm.Booking.BookingId);
            
            if (booking == null) return;

            vm.ProductName = $"{booking.Product.Brand} {booking.Product.Model}";
            vm.UserEmail = await GetUserEmail(booking.UserId);
            vm.Variants = await _productHttpService.GetVariantsById(booking.Product.ProductId);
        }

        private async Task FillBundleViewModel(BundleBookingEditViewModel vm, BundleBookingDto bundleBooking)
        {
            vm.BundleName = bundleBooking.BundleName;
            vm.UserEmail = await GetUserEmail(bundleBooking.Bookings[0].UserId);

            foreach (BundleBookingItemViewModel item in vm.Items)
            {
                BookingDto? booking = bundleBooking.Bookings.FirstOrDefault(b => b.BookingId == item.BookingId);
                if (booking == null) continue;

                item.CategoryTitle = CategoryInfo.Categories[booking.Product.Category].Title;
                item.ProductName = $"{booking.Product.Brand} {booking.Product.Model}";
                item.Variants = await _productHttpService.GetVariantsById(booking.Product.ProductId);
            }
        }

        // bookingen kender kun brugerens id, og login ligger stadig i web appen
        private async Task<string> GetUserEmail(string userId)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "";
            }

            return user.Email ?? "";
        }
    }
}
