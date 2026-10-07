using DiveDeep.API.Models;
namespace DiveDeep.API.Persistence
{
    public interface IBundleRepository
    {
        List<Bundle> GetAll();
        Bundle? GetById(int id);
    }
}
