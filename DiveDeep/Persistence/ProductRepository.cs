using DiveDeep.Data;
using DiveDeep.Models;
using static DiveDeep.Models.Enums;

namespace DiveDeep.Persistence
{
    public class ProductRepository : IProductRepository
    {

        private DiveDeepContext _diveDeepContext;

        public ProductRepository(DiveDeepContext diveDeepContext)
        {
            _diveDeepContext = diveDeepContext;
        }


        public List<Product> GetAll()
        {
            _diveDeepContext.Database.EnsureCreated();
            return _diveDeepContext.Products.ToList();
        }

        public Product? GetById(int id)
        {
            _diveDeepContext.Database.EnsureCreated();

            var product = _diveDeepContext.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                //TODO
            }
            return product;
        }

        public  void Delete(int id) => GetAll().RemoveAll(p => p.ProductId == id);

        public Booking? FindOverlappingBooking(int productId, DateTime startTime, DateTime endTime, int? excludedBookingId = null)
        {
            return _diveDeepContext.Bookings
                .FirstOrDefault(x =>
                    x.ProductId == productId &&
                    x.StartTime < endTime &&
                    x.EndTime > startTime &&
                    (excludedBookingId == null || x.BookingId != excludedBookingId)
                );
        }

        public bool IsProductAvailable(int productId, DateTime startDate, DateTime endDate, int requestedQuantity = 1)
        {
            var booking = FindOverlappingBooking(productId, startDate, endDate);
            return booking == null;
        }

        public List<Product> GetAvailableProducts(ProductCategory category, DateTime startDate, DateTime endDate)
        {
            return GetByCategory(category)
                .Where(p => !_diveDeepContext.Bookings.Any(b =>
                    b.ProductId == p.ProductId &&
                    b.StartTime < endDate &&
                    b.EndTime > startDate
                ))
                .ToList();
        }

        public  int GetAvailableCount(int productId, DateTime startDate, DateTime endDate)
        {
            return 1 - (_diveDeepContext.Bookings
                .Where(b =>
                    b.ProductId == productId &&
                    b.StartTime < endDate &&
                    b.EndTime > startDate
                )
                .Sum(b => b.Quantity)
            );
        }

        public List<string> GetBlockedDates(int productId, DateTime startDate, DateTime endDate)
        {
            var blockedDates = new List<string>();
            var bookings = _diveDeepContext.Bookings
                .Where(b =>
                    b.ProductId == productId &&
                    b.StartTime < endDate &&
                    b.EndTime > startDate
                )
                .ToList();

            foreach (var booking in bookings)
            {
                var blockStart = booking.StartTime > startDate ? booking.StartTime : startDate;
                var blockEnd = booking.EndTime < endDate ? booking.EndTime : endDate;

                blockedDates.Add($"{blockStart:dd/MM/yyyy} - {blockEnd:dd/MM/yyyy}");
            }

            return blockedDates;
        }
        public void Update(int id, Product product) {

            //var product = _diveDeepContext.Products.FirstOrDefault(p => p.ProductId == id);
            //if (product == null)
            //{
            //    TODO
            ////}
            //return product;

        }
        
        public List<Product> GetByCategory(ProductCategory category) => GetAll().Where(p => p.Category == category).ToList();

        public List<ProductCategory> GetProductCategories()
        {
            return GetAll().Select(p => p.Category).Distinct().ToList();
        }

        public List<Product> GetVariants(string brand, string model)
        {
            return GetAll()
                .Where(p =>
                    p.Brand == brand &&
                    p.Model == model)
                .ToList();
        }
    }
}
