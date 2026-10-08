using DiveDeep.API.Data;
using DiveDeep.API.Models;
using DiveDeep.Lib.Models;

namespace DiveDeep.API.Persistence
{
    public class ProductRepository : IProductRepository
    {

        private DiveDeepContext _diveDeepContext;

        public ProductRepository(DiveDeepContext diveDeepContext)
        {
            _diveDeepContext = diveDeepContext;
        }


        public async Task Add(Product product)
        {
            // en ny størrelse af en model der allerede findes, får modellens billede
            List<Product> variants = await GetVariants(product.Brand, product.Model ?? "");
            Product? variant = variants.FirstOrDefault();
            if (variant != null)
            {
                product.ProductImageId = variant.ProductImageId;
            }

            _diveDeepContext.Products.Add(product);
            _diveDeepContext.SaveChanges();
        }

        public async Task Update(Product product)
        {
            Product? productToUpdate = await GetById(product.ProductId);
            if (productToUpdate == null) return;

            productToUpdate.Brand = product.Brand;
            productToUpdate.Model = product.Model;
            productToUpdate.PricePerDay = product.PricePerDay;

            // felter som kun findes på den enkelte kategori
            if (productToUpdate is BCD bcd && product is BCD newBcd)
            {
                bcd.Size = newBcd.Size;
            }
            else if (productToUpdate is DiveSuit diveSuit && product is DiveSuit newDiveSuit)
            {
                diveSuit.Size = newDiveSuit.Size;
                diveSuit.SuitType = newDiveSuit.SuitType;
                diveSuit.Gender = newDiveSuit.Gender;
                diveSuit.Thickness = newDiveSuit.Thickness;
            }
            else if (productToUpdate is Fins fins && product is Fins newFins)
            {
                fins.Size = newFins.Size;
            }
            else if (productToUpdate is Tank tank && product is Tank newTank)
            {
                tank.VolumeLiters = newTank.VolumeLiters;
            }
            else if (productToUpdate is RegulatorSet regulatorSet && product is RegulatorSet newRegulatorSet)
            {
                regulatorSet.FirstStep = newRegulatorSet.FirstStep;
                regulatorSet.SecondStep = newRegulatorSet.SecondStep;
                regulatorSet.Octopus = newRegulatorSet.Octopus;
            }

            _diveDeepContext.SaveChanges();
        }

        public async Task<bool> HasBookings(int productId)
        {
            return _diveDeepContext.Bookings.Any(b => b.ProductId == productId);
        }

        public async Task<List<Product>> GetAll()
        {
            return _diveDeepContext.Products.ToList();
        }

        public async Task<Product?> GetById(int id)
        {
            var product = _diveDeepContext.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                //TODO
            }
            return product;
        }

        public async Task Delete(int id)
        {
            Product? product = await GetById(id);
            if (product == null) return;
            _diveDeepContext.Products.Remove(product);
            _diveDeepContext.SaveChanges();
        }

        public async Task<Booking?> FindOverlappingBooking(int productId, DateTime startTime, DateTime endTime, int? excludedBookingId = null)
        {
            return _diveDeepContext.Bookings
                .FirstOrDefault(x =>
                    x.ProductId == productId &&
                    x.StartTime < endTime &&
                    x.EndTime > startTime &&
                    (excludedBookingId == null || x.BookingId != excludedBookingId)
                );
        }

        public async Task<bool> IsProductAvailable(int productId, DateTime startDate, DateTime endDate, int requestedQuantity = 1)
        {
            Booking? booking = await FindOverlappingBooking(productId, startDate, endDate);
            return booking == null;
        }

        public async Task<List<Product>> GetAvailableProducts(ProductCategory category, DateTime startDate, DateTime endDate)
        {
            List<Product> productsByCategory = await GetByCategory(category);
            return productsByCategory
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
        public async Task Update(int id, Product product)
        {
            //var product = _diveDeepContext.Products.FirstOrDefault(p => p.ProductId == id);
            //if (product == null)
            //{
            //    TODO
            ////}
            //return product;

        }

        public async Task<List<Product>> GetByCategory(ProductCategory category)
        {
            List<Product> all = await GetAll();
            return all.Where(p => p.Category == category).ToList();
        }

        public async Task<List<ProductCategory>> GetProductCategories()
        {
            List<Product> all = await GetAll();
            return all.Select(p => p.Category).Distinct().ToList();
        }

        public async Task<ProductImage?> GetImage(int productImageId)
        {
            return _diveDeepContext.ProductImages.FirstOrDefault(i => i.ProductImageId == productImageId);
        }

        public async Task SaveImage(int productId, byte[] data, string contentType)
        {
            Product? product = await GetById(productId);
            if (product == null) return;

            ProductImage? productImage = null;
            if (product.ProductImageId != null)
            {
                productImage = await GetImage(product.ProductImageId.Value);
            }

            if (productImage == null)
            {
                productImage = new ProductImage();
                _diveDeepContext.ProductImages.Add(productImage);
            }

            productImage.Image = data;
            productImage.ContentType = contentType;

            // sørger for at alle varianter af modellen har samme billede
            List<Product> variants = await GetVariants(product.Brand, product.Model);
            foreach (Product variant in variants)
            {
                variant.Image = productImage;
            }

            _diveDeepContext.SaveChanges();
        }

        public async Task<List<DateTime>> GetBookedDates(int productId, DateTime fromDate, DateTime toDate)
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

        public async Task<List<Product>> GetVariants(string brand, string model)
        {
            List<Product> all = await GetAll();
            return all
                .Where(p => p.Brand == brand && p.Model == model)
                .ToList();
        }

        public async Task<List<Product>> GetVariants(string brand)
        {
            List<Product> all = await GetAll();
            return all
                .Where(p => p.Brand == brand)
                .ToList();
        }
    }
}
