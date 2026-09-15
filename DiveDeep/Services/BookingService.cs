using System;
using System.Collections.Generic;
using System.Linq;
using DiveDeep.Models;
using DiveDeep.Persistence;

namespace DiveDeep.Services
{
    public class BookingService
    {
        private readonly IProductRepository _productRepository;

        public BookingService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
    }
}
