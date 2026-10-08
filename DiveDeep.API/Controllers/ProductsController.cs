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

        // Enkelt produkt
        private async Task<ProductDto> ConvertSingleToProductDto(Product product)
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

        // Flere produkter
        private async Task<List<ProductDto>> ConvertMultipleToProductDto(List<Product> products)
        {
            List<ProductDto> productDtos = new();

            foreach (Product product in products)
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

                productDtos.Add(productDto);
            }

            return productDtos;
        }

        // API metoder herunder

        // Få alle kategorier
        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _productRepository.GetProductCategories();
            if (categories.Count <= 0)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        // Få produkter ud fra kategorier
        [HttpGet("Categories/{category}")]
        public async Task<IActionResult> GetByCategory(ProductCategory category)
        {
            List<Product> products = await _productRepository.GetByCategory(category);

            if (products.Count <= 0)
            {
                return NotFound();
            }

            List<ProductDto> productDtos = await ConvertMultipleToProductDto(products);

            return Ok(productDtos);
        }

        // Få produkt ud fra id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Product? product = await _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            ProductDto productDto = await ConvertSingleToProductDto(product);

            return Ok(productDto);
        }


        // Få varianter ud fra id
        [HttpGet("{id}/Variants")]
        public async Task<IActionResult> GetVariantsById(int id)
        {
            Product? product = await _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }
            List<Product> products = new();
            if (product.Model != null)
            {
                products = await _productRepository.GetVariants(product.Brand, product.Model);
            }
            else
            {
                products = await _productRepository.GetVariants(product.Brand);
            }

            if (products.Count <= 0)
            {
                return NotFound();
            }

            List<ProductDto> productDtos = await ConvertMultipleToProductDto(products);

            return Ok(productDtos);
        }

        // Få bookede datoer i datospæn
        [HttpGet("{id}/BookedDates/{fromDate}/{toDate}")]
        public async Task<IActionResult> GetBookedDates(int id, DateTime fromDate, DateTime toDate)
        {
            // Tjek at produktet eksisterer
            Product? product = await _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            // Hent liste af bookede datoer i efterspurgt tidsspæn
            List<DateTime> bookedDates = await _productRepository.GetBookedDates(id, fromDate, toDate);

            return Ok(bookedDates);
        }

        // Få billedet som hører til produktet
        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetImageById(int id)
        {
            Product? product = await _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }
            if (product.ProductImageId.HasValue)
            {
                int productImageId = product.ProductImageId.Value;
                ProductImage? productImage = await _productRepository.GetImage(productImageId);
                
                if (productImage != null)
                {
                    return Ok(File(productImage.Image, productImage.ContentType));
                }
            }
            
            return NotFound();
        }

    }
}
