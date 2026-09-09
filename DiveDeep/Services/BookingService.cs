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

        public BookingValidationResult ValidateBooking(Booking booking)
        {
            var result = new BookingValidationResult();
            result.IsSuccessful = false;

            DateTime startTime = booking.StartTime;
            DateTime endTime = booking.EndTime;

            // Krav 1: Start dato skal være før slut dato
            if (startTime >= endTime)
            {
                result.Key = "EndTime";
                result.ErrorMessage = "Slut dato skal være efter start dato";
                return result;
            }

            // Krav 2: Start dato kan ikke være i fortiden
            if (startTime < DateTime.Now)
            {
                result.Key = "StartTime";
                result.ErrorMessage = "Start dato kan ikke være i fortiden";
                return result;
            }

            // Krav 3: Produktet skal være tilgængeligt i hele perioden
            var isAvailable = _productRepository.IsProductAvailable(
                booking.ProductId,
                startTime,
                endTime,
                booking.Quantity
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

        public List<Product> GetAvailableProducts(int categoryId, DateTime startDate, DateTime endDate)
        {
            var products = _productRepository.GetAll();
            return products
                .Where(p => p.Category == (Enums.ProductCategory)categoryId)
                .Where(p => _productRepository.IsProductAvailable(p.ProductId, startDate, endDate))
                .ToList();
        }
    }
}
