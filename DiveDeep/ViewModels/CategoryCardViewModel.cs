namespace DiveDeep.ViewModels
{
    // Én kategori-boks på produktoversigten.
    // Indeholder kun det, viewet skal bruge - ingen enum, ingen logik.
    public class CategoryCardViewModel
    {
        public string Title { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public string AltText { get; set; } = "";
        public string RouteId { get; set; } = "";
    }
}
