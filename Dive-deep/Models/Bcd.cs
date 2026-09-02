using static Dive_deep.Models.Enums;
namespace Dive_deep.Models

{

public class Bcd : Product
{
    
    public List<Size> Sizes { get; set; } = new();
    public override IEnumerable<string> SizeOptions => Sizes.Select(s => s.ToString());
}
}
