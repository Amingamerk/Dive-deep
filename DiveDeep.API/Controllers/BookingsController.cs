using DiveDeep.API.Models;
using DiveDeep.API.Services;
using DiveDeep.Lib.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DiveDeep.API.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // Få alle bookings
        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            List<Booking> bookings = await _bookingService.GetAll();

            return Ok(ToBookingDtos(bookings));
        }

        // Få alle Bookings ud fra userId
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetBookingByUserId(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return BadRequest();
            }

            List<Booking> bookings = await _bookingService.GetByUserId(id);

            return Ok(ToBookingDtos(bookings));
        }

        // Få specifik Booking ud fra bookingId
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByBookingId(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            Booking? booking = await _bookingService.GetById(id);
            
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(ToBookingDto(booking));
        }

        // Få specifik BundleBooking ud fra bookingId
        [HttpGet("bundlebooking/{id}")]
        public async Task<IActionResult> GetBundleBookingByiD(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            BundleBooking? booking = await _bookingService.GetBundleBookingById(id);

            if (booking == null)
            {
                return NotFound();
            }

            var response = booking.Adapt<BundleBookingDto>();

            return Ok(response);
        }

        // Slet Booking
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                await _bookingService.Delete(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // Slet BundleBooking
        [HttpDelete("bundlebooking/{id}")]
        public async Task<IActionResult> DeleteBundleBooking(int id)
        {
            try
            {
                await _bookingService.DeleteBundleBooking(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // Opdater Booking
        [HttpPut]
        public async Task<IActionResult> UpdateBooking([FromBody] BookingDto bookingDto)
        {
            Booking? existingBooking = await _bookingService.GetById(bookingDto.BookingId);
            if (existingBooking == null)
            {
                return NotFound();
            }

            Booking booking = ToBooking(bookingDto);
            BookingValidationResult result = await _bookingService.UpdateBooking(booking);

            return Ok(result);
        }

        // Opdater BundleBooking
        [HttpPut("bundlebooking")]
        public async Task<IActionResult> UpdateBundleBooking([FromBody] UpdateBundleBookingRequestDto request)
        {
            if (request.Bookings.Count == 0)
            {
                return BadRequest();
            }

            List<Booking> bookings = new();
            foreach (BookingDto bookingDto in request.Bookings)
            {
                bookings.Add(ToBooking(bookingDto));
            }

            // Servicen står for resten her fra;
            // den sætter perioden på hver booking, tjekker ledighed og gemmer
            BookingValidationResult result = await _bookingService.UpdateBundleBooking(bookings, request.StartTime, request.EndTime);

            return Ok(result);
        }

        // Opret Bookings
        [HttpPost]
        public async Task<IActionResult> CreateBookings([FromBody] CreateBookingsRequestDto request)
        {
            if (request.Bookings.Count == 0 && request.BundleBookings.Count == 0)
            {
                return BadRequest();
            }

            List<Booking> bookings = new();
            foreach (BookingDto bookingDto in request.Bookings)
            {
                bookings.Add(ToBooking(bookingDto));
            }

            List<BundleBooking> bundleBookings = new();
            foreach (BundleBookingDto bundleBookingDto in request.BundleBookings)
            {
                bundleBookings.Add(ToBundleBooking(bundleBookingDto));
            }

            BookingValidationResult result = await _bookingService.CreateBookings(bookings, bundleBookings);

            return Ok(result);
        }

        // Valider produkterne i en Bundle.
        // Den gemmer ikke noget og validerer bare, fordi kurven ligger stadig i sessionen i web appen
        [HttpPost("validate-bundle")]
        public async Task<IActionResult> ValidateBundleProducts([FromBody] ValidateBundleProductsRequestDto request)
        {
            if (request.BundleId <= 0 || request.ProductIds.Count <= 0)
            {
                return BadRequest();
            }

            BookingValidationResult result = await _bookingService.ValidateBundleProducts(request.BundleId, request.ProductIds, request.StartTime, request.EndTime);

            return Ok(result);
        }

        // Mapster henter ikke navnet ud af pakken af sig selv, så det sætter vi bagefter
        private BookingDto ToBookingDto(Booking booking)
        {
            BookingDto bookingDto = booking.Adapt<BookingDto>();
            bookingDto.BundleName = booking.BundleBooking?.BundleName;

            return bookingDto;
        }

        private List<BookingDto> ToBookingDtos(List<Booking> bookings)
        {
            List<BookingDto> bookingDtos = new();
            foreach (Booking booking in bookings)
            {
                bookingDtos.Add(ToBookingDto(booking));
            }

            return bookingDtos;
        }

        // Mapster kan ikke lave et Product, fordi det er en basisklasse med en type per kategori.
        // Vi skal kun bruge ProductId for at gemme, så de her to laver om i hånden
        private Booking ToBooking(BookingDto bookingDto)
        {
            Booking booking = new();
            booking.BookingId = bookingDto.BookingId;
            booking.ProductId = bookingDto.ProductId;
            booking.StartTime = bookingDto.StartTime;
            booking.EndTime = bookingDto.EndTime;
            booking.UserId = bookingDto.UserId;
            booking.RowVersion = bookingDto.RowVersion;

            return booking;
        }

        private BundleBooking ToBundleBooking(BundleBookingDto bundleBookingDto)
        {
            BundleBooking bundleBooking = new();
            bundleBooking.BundleId = bundleBookingDto.BundleId;
            bundleBooking.BundleName = bundleBookingDto.BundleName;
            bundleBooking.DiscountPercent = bundleBookingDto.DiscountPercent;
            bundleBooking.CreatedAt = bundleBookingDto.CreatedAt;

            foreach (BookingDto bookingDto in bundleBookingDto.Bookings)
            {
                bundleBooking.Bookings.Add(ToBooking(bookingDto));
            }

            return bundleBooking;
        }
    }
}
