using static DiveDeep.Models.Enums;
namespace DiveDeep.Models

{

public class BCD : Product
{
    public List<Size> Sizes { get; set; } = new();
    public override IEnumerable<string> SizeOptions => Sizes.Select(s => s.ToString());
}
}
