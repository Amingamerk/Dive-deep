using DiveDeep.ViewModels;
using static DiveDeep.Models.Enums;
using DiveDeep.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.Controllers
{
    public class ProductsController : Controller
    {
        private static readonly Dictionary<ProductCategory, (string Title, string ImageFile, string AltText)> categoryInfo = new()
        {
            [ProductCategory.BCD]          = ("BCD'er",            "BCD.png",           "BCD / vestsystem"),
            [ProductCategory.DiveSuit]     = ("Dykkerdragter",     "wetsuit.png",       "Dykkerdragt"),
            [ProductCategory.Fins]         = ("Finner",            "fins.png",          "Svømmefinner"),
            [ProductCategory.MaskSnorkel]  = ("Masker & snorkler", "mask.png",          "Dykkermaske og snorkel"),
            [ProductCategory.RegulatorSet] = ("Regulatorsæt",      "regulator_WIP.png", "Regulatorsæt"),
            [ProductCategory.Tank]         = ("Dykkertanke",       "tank.png",          "Dykkertank")
        };

        public IActionResult Index()
        {
            List<ProductCategory> categories = ProductRepository.GetProductCategories();
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
    }
}
