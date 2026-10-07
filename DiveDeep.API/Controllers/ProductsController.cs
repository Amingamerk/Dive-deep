using DiveDeep.API.Models;
using DiveDeep.API.Persistence;
using DiveDeep.Lib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Generelle "værktøjsmetoder" herunder

        private ProductDto ConvertToProductDto(Product product)
        {
            ProductDto productDto = new();
            productDto.ProductId = product.ProductId;
            productDto.Brand = product.Brand;
            if (product.Model != null)
            {
                productDto.Model = product.Model;
            }
            productDto.PricePerDay = product.PricePerDay;
            productDto.Category = product.Category;
            if (product.ProductImageId != null)
            {
                productDto.ProductImageId = product.ProductImageId;
            }
            productDto.VariantLabel = product.VariantLabel;
            productDto.VariantHeading = product.VariantHeading;
            if (product.VariantGroup != null)
            {
                productDto.VariantGroup = product.VariantGroup;
            }

            // Sæt size for Fins, BCD og DiveSuit
            if (product is Fins fins)
            {
                productDto.Size = fins.Size;
            }
            if (product is BCD bcd)
            {
                productDto.Size = bcd.Size;
            }
            if (product is DiveSuit diveSuit)
            {
                productDto.Size = diveSuit.Size;

                // Sæt SuitType & Gender hvis typen er DiveSuit
                productDto.SuitType = diveSuit.SuitType;
                productDto.Gender = diveSuit.Gender;

                // Sæt Thickness hvis DiveSuit er af SuitType Wetsuit
                if (diveSuit.SuitType == SuitType.Wetsuit)
                {
                    productDto.Thickness = diveSuit.Thickness;
                }
            }

            // Sæt RegulatorSet information
            if (product is RegulatorSet regulatorSet)
            {
                productDto.FirstStep = regulatorSet.FirstStep;
                productDto.SecondStep = regulatorSet.SecondStep;
                productDto.Octopus = regulatorSet.Octopus;
            }

            // Sæt VolumeLiters
            if (product is Tank tank)
            {
                productDto.VolumeLiters = tank.VolumeLiters;
            }

            return productDto;

        }

        // API metoder herunder

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _productRepository.GetProductCategories();
            if (categories.Count <= 0)
            {
                return NotFound();
            }
            return Ok(categories);
        }
    }
}
