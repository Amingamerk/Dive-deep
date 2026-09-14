using DiveDeep.Models;
using DiveDeep.Persistence;

namespace DiveDeep.Services
{
    public class CartService : ICartService
    {
        private readonly Cart _cart = new();
        public Cart GetCart()
        {
            return _cart;
        }
        public void AddItem(Product product, string? size, string? gender)
        {
            // Tjek om produktet allerede er i kurven med samme størrelse og køn
            CartItem? existingItem = _cart.Items.FirstOrDefault(i => 
                i.ProductId == product.ProductId && 
                i.SelectedSize == size && 
                i.SelectedGender == gender);

            if (existingItem != null)
            {
                // Hvis det allerede er der, øg quantity
                existingItem.Quantity++;
            }
            else
            {
                // Hvis ikke, tilføj nyt item
                CartItem item = new()
                {
                    ProductId = product.ProductId,
                    Brand = product.Brand,
                    Model = product.Model ?? "",
                    PricePerDay = product.PricePerDay,
                    SelectedSize = size,
                    SelectedGender = gender,
                    ImagePath = $"/images/products/{product.Brand.ToLower()}.png",
                    Quantity = 1
                };
                _cart.Items.Add(item);
            }
        }

        public void ClearCart()
        {
            _cart.Items.Clear();
        }


        public decimal GetTotalPrice()
        {
            return (decimal)_cart.Items.Sum(i => i.PricePerDay * i.Quantity);
        }

        public void RemoveItem(int productId, string? size, string? gender)
        {
            CartItem? item = _cart.Items.FirstOrDefault(i => 
                i.ProductId == productId && 
                i.SelectedSize == size && 
                i.SelectedGender == gender);
            if (item != null)
            {
                _cart.Items.Remove(item);
            }
        }
    }
}
