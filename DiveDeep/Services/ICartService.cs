using DiveDeep.Models;

namespace DiveDeep.Persistence
{
    public interface ICartService
    {
        public Cart GetCart();
        public void AddItem(Product product, string? size, string? gender, DateTime startTime, DateTime endTime);
        public void AddBundle(Bundle bundle, List<Product> products, DateTime startTime, DateTime endTime);
        public void RemoveItem(int productId, string? size, string? gender);
        public void RemoveBundle(int index);
        public void ClearCart();
    }
}