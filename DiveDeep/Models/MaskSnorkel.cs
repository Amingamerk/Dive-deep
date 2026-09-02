using static DiveDeep.Models.Enums;
namespace DiveDeep.Models
{
    public class MaskSnorkel : Product
    {
       
        public List<Size> Sizes { get; set; } = new();
        public override IEnumerable<string> SizeOptions => Sizes.Select(s => s.ToString());

        public override string SizeLabel => "Størrelse";
    }
}
