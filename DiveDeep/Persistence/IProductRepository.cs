using DiveDeep.Models;

namespace DiveDeep.Persistence
{
    public interface IProductRepository
    {
        //void Add
        //void Delete(int id);
        List<Product> GetAll();
        Product? GetById(int id);
        //void Update
    }
}
