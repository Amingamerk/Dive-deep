using DiveDeep.Data;
using DiveDeep.Models;
using Microsoft.EntityFrameworkCore;

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
            return _diveDeepContext.Products.ToList();
        }

        public Product? GetById(int id)
        {
            var product = _diveDeepContext.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                //TODO
            }
            return product;
        }

        public void Delete(int id)
        {
            Product? product = GetById(id);
            if (product == null) return;
            _diveDeepContext.Products.Remove(product);
            _diveDeepContext.SaveChanges();
        }

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

        //public int getavailablecount(int productid, datetime startdate, datetime enddate)
        //{
        //    return 1 - (_divedeepcontext.bookings
        //        .where(b =>
        //            b.productid == productid &&
        //            b.starttime < enddate &&
        //            b.endtime > startdate
        //        )
        //        .sum(b => b.quantity)
        //    );
        //}

        //public List<string> GetBlockedDates(int productId, DateTime startDate, DateTime endDate)
        //{
        //    var blockedDates = new List<string>();
        //    var bookings = _diveDeepContext.Bookings
        //        .Where(b =>
        //            b.ProductId == productId &&
        //            b.StartTime < endDate &&
        //            b.EndTime > startDate
        //        )
        //        .ToList();

        //    foreach (var booking in bookings)
        //    {
        //        var blockStart = booking.StartTime > startDate ? booking.StartTime : startDate;
        //        var blockEnd = booking.EndTime < endDate ? booking.EndTime : endDate;

        //        blockedDates.Add($"{blockStart:dd/MM/yyyy} - {blockEnd:dd/MM/yyyy}");
        //    }

        //    return blockedDates;
        //}
        public void Update(int id, Product product) 
        {
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

        public ProductImage? GetImage(int productImageId)
        {
            return _diveDeepContext.ProductImages.FirstOrDefault(i => i.ProductImageId == productImageId);
        }

        public void SaveImage(int productId, byte[] data, string contentType)
        {
            Product? product = GetById(productId);
            if (product == null) return;

            ProductImage? productImage = null;
            if (product.ProductImageId != null)
            {
                productImage = GetImage(product.ProductImageId.Value);
            }

            // tjek om modellen ikke har et billede endnu
            if (productImage == null)
            {
                productImage = new ProductImage();
                _diveDeepContext.ProductImages.Add(productImage);
            }

            productImage.Image = data;
            productImage.ContentType = contentType;

            // sørger for at alle varianter af modellen har samme billede
            foreach (Product variant in GetVariants(product.Brand, product.Model))
            {
                variant.Image = productImage;
            }

            _diveDeepContext.SaveChanges();
        }

        public List<DateTime> GetBookedDates(int productId, DateTime fromDate, DateTime toDate)
        {
            List<Booking> bookings = _diveDeepContext.Bookings
                .Where(b => b.ProductId == productId && b.StartTime < toDate && b.EndTime > fromDate)
                .ToList();

            List<DateTime> bookedDates = new();

            foreach (Booking booking in bookings)
            {
                DateTime date = booking.StartTime.Date;

                while (date < booking.EndTime.Date)
                {
                    if (!bookedDates.Contains(date))
                    {
                        bookedDates.Add(date);
                    }

                    date = date.AddDays(1);
                }
            }
            return bookedDates;
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
