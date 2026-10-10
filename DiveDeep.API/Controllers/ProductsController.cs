using DiveDeep.API.Models;
using DiveDeep.API.Persistence;
using DiveDeep.Lib.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiveDeep.API.Controllers
{
    [Route("api/products")]
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

        // Laver den rigtige type produkt ud fra kategorien
        private Product ConvertToProduct(ProductDto productDto)
        {
            Product product;

            switch (productDto.Category)
            {
                case ProductCategory.BCD:
                    BCD bcd = new();
                    bcd.Size = productDto.Size.GetValueOrDefault();
                    product = bcd;
                    break;

                case ProductCategory.DiveSuit:
                    DiveSuit diveSuit = new();
                    diveSuit.Size = productDto.Size.GetValueOrDefault();
                    diveSuit.SuitType = productDto.SuitType.GetValueOrDefault();
                    diveSuit.Gender = productDto.Gender ?? "";
                    diveSuit.Thickness = productDto.Thickness;
                    product = diveSuit;
                    break;

                case ProductCategory.Fins:
                    Fins fins = new();
                    fins.Size = productDto.Size.GetValueOrDefault();
                    product = fins;
                    break;

                case ProductCategory.Tank:
                    Tank tank = new();
                    tank.VolumeLiters = productDto.VolumeLiters.GetValueOrDefault();
                    product = tank;
                    break;

                case ProductCategory.RegulatorSet:
                    RegulatorSet regulatorSet = new();
                    regulatorSet.FirstStep = productDto.FirstStep ?? "";
                    regulatorSet.SecondStep = productDto.SecondStep ?? "";
                    regulatorSet.Octopus = productDto.Octopus ?? "";
                    product = regulatorSet;
                    break;

                default:
                    product = new MaskSnorkel();
                    break;
            }

            product.ProductId = productDto.ProductId;
            product.Brand = productDto.Brand;
            product.Model = productDto.Model;
            product.PricePerDay = productDto.PricePerDay;

            return product;
        }

        // API metoder herunder

        // Opdater produkt
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductDto productDto)
        {
            Product? existingProduct = await _productRepository.GetById(productDto.ProductId);
            if (existingProduct == null)
            {
                return NotFound();
            }

            Product product = ConvertToProduct(productDto);
            await _productRepository.Update(product);

            return NoContent();
        }

        // Tilføj produkt
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ProductDto productDto)
        {
            Product product = ConvertToProduct(productDto);
            try
            {
                await _productRepository.Add(product);

                // EF har lagt det nye id ind i product efter SaveChanges
                return Ok(product.ProductId);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // Tilføj billede til produkt
        [HttpPost("{productId}/add-image")]
        public async Task<IActionResult> SaveImageToProduct(int productId, [FromBody] ProductImageDto productImageDto)
        {
            try
            {
                await _productRepository.SaveImage(productId, productImageDto.Image, productImageDto.ContentType);
                return Created();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // Slet produkt
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productRepository.Delete(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // Få alle kategorier
        [HttpGet("categories")]
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
        [HttpGet("categories/{category}")]
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

        // Få alle produkter
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            List<Product> products = await _productRepository.GetAll();
            List<ProductDto> productDtos = new();
            if (products.Count <= 0)
            {
                return Ok(productDtos);
            }

            productDtos = await ConvertMultipleToProductDto(products);

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
        [HttpGet("{id}/variants")]
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

        // Tjek om der findes bookinger på produktet
        [HttpGet("{id}/has-bookings")]
        public async Task<IActionResult> HasBookings(int id)
        {
            // Tjek at produktet eksisterer
            Product? product = await _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            bool productHasBookings = false;
            productHasBookings = await _productRepository.HasBookings(id);

            return Ok(productHasBookings);
        }

        // Få bookede datoer i datospæn
        [HttpGet("{id}/booked-dates/{fromDate}/{toDate}")]
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
                    return File(productImage.Image, productImage.ContentType);
                }
            }
            
            return NotFound();
        }
    }
}
