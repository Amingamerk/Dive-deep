using DiveDeep.Models;
using System.Xml.Serialization;

namespace DiveDeep.Persistence
{
    public interface ICartService
    {
        public Cart GetCart();
        public void AddItem(Product product, string? size, string? gender, DateTime startTime, DateTime endTime);
        public void RemoveItem(int productId, string? size, string? gender);
        public void ClearCart();
        public decimal GetTotalPrice();
    }
}
