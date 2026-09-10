using DiveDeep.Models;
using static DiveDeep.Models.Enums;
namespace DiveDeep.Models
{
    public class DiveSuit : Product
    {
        
        public string Gender { get; set; } = " ";
        public string? Thickness { get; set; }

        public List<Size> Sizes { get; set; } = new();
        public override IEnumerable<string> SizeOptions => Sizes.Select(s => s.ToString());

        public List<SuitType> SuitTypes { get; set; } = new();
        public override IEnumerable<string> SuitTypeOptions => SuitTypes.Select(st => st.ToString());
    }
}

//Dette kunn være en løsning istedet? virker bedrel, men ved ikke om det er for teknisk til vores krav.

//public class DiveSuit : Product
//{
//    public List<Size> AvailableSizes { get; set; } = new();  // Hvad der FÅES
//    public Size? SelectedSize { get; set; }  // Hvad customer VALGTE

//    public override IEnumerable<string> SizeOptions =>
//        AvailableSizes.Select(s => s.ToString());
//}
//// I repository:
//new DiveSuit
//{
//    Id = 5,
//    AvailableSizes = new() { Size.XSmall, Size.Small, Size.Medium, Size.Large, Size.XLarge },
//    SelectedSize = null  // Vælges når man booker
//}