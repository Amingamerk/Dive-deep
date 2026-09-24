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

        public void AddItem(Product product, string? size, string? gender, DateTime startTime, DateTime endTime)
        {
            // Tjek om produktet allerede er i kurven med samme størrelse, køn og periode
            CartItem? existingItem = _cart.Items.FirstOrDefault(i =>
                i.ProductId == product.ProductId &&
                i.SelectedSize == size &&
                i.SelectedGender == gender &&
                i.StartTime == startTime &&
                i.EndTime == endTime);

            if (existingItem != null)
            {
                // Hvis det allerede er der, øg quantity
                existingItem.Quantity++;
            }
            else
            {
                // Hvis ikke, tilføj nyt item
                CartItem item = CreateCartItem(product, size, gender, startTime, endTime);
                _cart.Items.Add(item);
            }
        }

        public void AddBundle(Bundle bundle, List<Product> products, DateTime startTime, DateTime endTime)
        {
            CartBundle cartBundle = new();
            cartBundle.BundleId = bundle.BundleId;
            cartBundle.BundleName = bundle.Name;
            cartBundle.DiscountPercent = bundle.DiscountPercent;
            cartBundle.StartTime = startTime;
            cartBundle.EndTime = endTime;

            // et CartItem per produkt i pakken
            foreach (Product product in products)
            {
                CartItem item = CreateCartItem(product, product.VariantLabel, product.VariantGroup, startTime, endTime);
                cartBundle.Items.Add(item);
            }

            _cart.Bundles.Add(cartBundle);
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

        public void RemoveBundle(int index)
        {
            if (index >= 0 && index < _cart.Bundles.Count)
            {
                _cart.Bundles.RemoveAt(index);
            }
        }

        public void ClearCart()
        {
            _cart.Items.Clear();
            _cart.Bundles.Clear();
        }

        // bruges til både enkelte produkter og pakker
        private CartItem CreateCartItem(Product product, string? size, string? gender, DateTime startTime, DateTime endTime)
        {
            CartItem item = new()
            {
                ProductId = product.ProductId,
                Brand = product.Brand,
                Model = product.Model ?? "",
                PricePerDay = product.PricePerDay,
                SelectedSize = size,
                SelectedGender = gender,
                ImagePath = $"/images/products/{product.Brand.ToLower()}.png",
                Quantity = 1,
                StartTime = startTime,
                EndTime = endTime
            };
            return item;
        }
    }
}