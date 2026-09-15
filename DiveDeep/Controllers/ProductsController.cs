using DiveDeep.Models;
using DiveDeep.Persistence;
using DiveDeep.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            List<ProductCategory> categories = _productRepository.GetProductCategories();
            List<CategoryCardViewModel> viewModel = new();

            foreach (ProductCategory category in categories)
            {
                (string title, string imageFile, string altText) = CategoryInfo.Categories[category];

                viewModel.Add(new CategoryCardViewModel
                {
                    Title = title,
                    ImagePath = $"/images/products/categories/{imageFile}",
                    AltText = altText,
                    RouteId = category.ToString()
                });
            }

            return View(viewModel);
        }

        public IActionResult Category(ProductCategory category)
        {
            var products = _productRepository.GetByCategory(category)
                .GroupBy(p => new
                {
                    p.Brand,
                    p.Model
                })
                .Select(g => g.First())
                .ToList();

            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            List<Product> variants = 
                _productRepository.GetVariants(product.Brand, product.Model)
                    .OrderBy(v => v.ProductId)
                    .ToList();

            // create productviewmodel
            ProductViewModel pvm = new();

            pvm.Brand = product.Brand;
            pvm.Model = product.Model;
            pvm.PricePerDay = product.PricePerDay;
            pvm.VariantHeading = product.VariantHeading;
            pvm.SelectedProductId = product.ProductId;

            foreach (Product p in variants)
            {
                ProductVariantViewModel variant = new();
                variant.ProductId = p.ProductId;
                variant.Label = p.VariantLabel;
                variant.Group = p.VariantGroup;
                pvm.Variants.Add(variant);
            }

            DateTime fromDate = DateTime.Today;
            DateTime toDate = fromDate.AddMonths(12);

            List<DateTime> bookedDates = _productRepository.GetBookedDates(product.ProductId, fromDate, toDate);

            foreach (DateTime date in bookedDates)
            {
                pvm.BookedDates.Add(date.ToString("dd/MM/yyyy"));
            }

            return View(pvm);
        }
    }

}
