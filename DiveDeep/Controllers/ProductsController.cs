using DiveDeep.Models;
using DiveDeep.Persistence;
using DiveDeep.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static DiveDeep.Models.Enums;

namespace DiveDeep.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        private readonly Dictionary<ProductCategory, (string Title, string ImageFile, string AltText)> categoryInfo = new()
        {
            [ProductCategory.BCD]          = ("BCD'er",            "BCD.png",           "BCD / vestsystem"),
            [ProductCategory.DiveSuit]     = ("Dykkerdragter",     "wetsuit.png",       "Dykkerdragt"),
            [ProductCategory.Fins]         = ("Finner",            "fins.png",          "Svømmefinner"),
            [ProductCategory.MaskSnorkel]  = ("Masker & snorkler", "mask.png",          "Dykkermaske og snorkel"),
            [ProductCategory.RegulatorSet] = ("Regulatorsæt",      "regulator_WIP.png", "Regulatorsæt"),
            [ProductCategory.Tank]         = ("Dykkertanke",       "tank.png",          "Dykkertank")
        };

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
                (string title, string imageFile, string altText) = categoryInfo[category];

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

        //public IActionResult Categories()
        //{
        //    var categories = Enum.GetValues<ProductCategory>();
        //    return View(categories);
        //}

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
            pvm.SizeLabel = product.SizeLabel ?? "Størrelse";
            pvm.SelectedProductId = product.ProductId;

            foreach (Product p in variants)
            {
                ProductVariantViewModel pvvm = new();

                pvvm.ProductId = p.ProductId;
                pvvm.Group = p.VariantGroup;

                // cool version:
                //pvvm.Label = p.SizeOptions != null ? string.Join(", ", p.SizeOptions) : (p.Model ?? "");

                // boring version:
                if (p.SizeOptions != null)
                {
                    pvvm.Label = string.Join(", ", p.SizeOptions);
                }
                else
                {
                    if (p.Model != null)
                    {
                        pvvm.Label = p.Model;
                    }
                    else
                    {
                        pvvm.Label = "";
                    }
                }
                pvm.Variants.Add(pvvm);
            }

            return View(pvm);


            //// Extract unique sizes from variants using the virtual SizeOptions property
            //var sizeOptions = variants
            //    .SelectMany(v => v.SizeOptions)
            //    .Distinct()
            //    .ToList();

            //// Store in ViewBag for the view
            //ViewBag.Variants = variants;
            //ViewBag.SizeOptions = sizeOptions;
        }

        [HttpPost]
        public IActionResult CheckAvailability(int productId, string startDate, string endDate)
        {
            if (!DateTime.TryParseExact(startDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out var parsedStartDate) ||
                !DateTime.TryParseExact(endDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out var parsedEndDate))
            {
                return Json(new { isAvailable = false, message = "Ugyldig datoformat" });
            }

            var isAvailable = _productRepository.IsProductAvailable(productId, parsedStartDate, parsedEndDate);
            var blockedDates = _productRepository.GetBlockedDates(productId, parsedStartDate, parsedEndDate);

            return Json(new
            {
                isAvailable = isAvailable,
                message = isAvailable ? "Produktet er tilgængeligt" : "Produktet er desværre ikke tilgængeligt for de valgte datoer",
                blockedDates = blockedDates
            });
        }
    }

}
