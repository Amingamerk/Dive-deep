using DiveDeep.Models;
namespace DiveDeep.Persistence
{
    public interface IBundleRepository
    {
        List<Bundle> GetAll();
        Bundle? GetById(int id);
    }
}
