using DiveDeep.Models;
using DiveDeep.Persistence;
using System.Text.Json;

namespace DiveDeep.Services
{
    public class CartService : ICartService
    {
        private const string CartSessionKey = "Cart";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Cart GetCart()
        {
            ISession session = _httpContextAccessor.HttpContext!.Session;
            string? value = session.GetString(CartSessionKey);

            if (value == null)
            {
                return new Cart();
            }

            Cart? cart = JsonSerializer.Deserialize<Cart>(value);
            if (cart == null)
            {
                return new Cart();
            }
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            ISession session = _httpContextAccessor.HttpContext!.Session;
            session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
        }

        public void AddItem(Product product, string? size, string? gender, DateTime startTime, DateTime endTime)
        {
            Cart cart = GetCart();

            // Tjek om produktet allerede er i kurven med samme størrelse, køn og periode
            CartItem? existingItem = cart.Items.FirstOrDefault(i =>
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
                cart.Items.Add(item);
            }
            SaveCart(cart);
        }

        public void AddBundle(Bundle bundle, List<Product> products, DateTime startTime, DateTime endTime)
        {
            Cart cart = GetCart();

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

            cart.Bundles.Add(cartBundle);
            SaveCart(cart);
        }

        public void RemoveItem(int productId, string? size, string? gender)
        {
            Cart cart = GetCart();
            CartItem? item = cart.Items.FirstOrDefault(i =>
                i.ProductId == productId &&
                i.SelectedSize == size &&
                i.SelectedGender == gender);
            if (item != null)
            {
                cart.Items.Remove(item);
            }
            SaveCart(cart);
        }

        public void RemoveBundle(int index)
        {
            Cart cart = GetCart();
            if (index >= 0 && index < cart.Bundles.Count)
            {
                cart.Bundles.RemoveAt(index);
            }
            SaveCart(cart);
        }

        public void ClearCart()
        {
            Cart cart = GetCart();
            cart.Items.Clear();
            cart.Bundles.Clear();
            SaveCart(cart);
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