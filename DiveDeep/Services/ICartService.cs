using DiveDeep.Lib.Models;
using DiveDeep.Models;

namespace DiveDeep.Persistence
{
    public interface ICartService
    {
        public Cart GetCart();
        public void AddItem(ProductDto product, string? size, string? gender, DateTime startTime, DateTime endTime);
        public void AddBundle(Bundle bundle, List<ProductDto> products, DateTime startTime, DateTime endTime);
        public void RemoveItem(int productId, string? size, string? gender);
        public void RemoveBundle(int index);
        public void ClearCart();
    }
}