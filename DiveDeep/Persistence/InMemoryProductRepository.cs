using DiveDeep.Models;
using static DiveDeep.Models.Enums;

namespace DiveDeep.Persistence
{
    public static class InMemoryProductRepository
    {
        private static readonly List<Product> products = new()
        {
            // ===== BCD =====
            new BCD { ProductId = 1, Category = ProductCategory.BCD, Brand = "Scubapro", Model = "Navigator Lite BCD", Sizes = new() { Size.Small }, PricePerDay = 125 },
            new BCD { ProductId = 2, Category = ProductCategory.BCD, Brand = "Scubapro", Model = "Navigator Lite BCD", Sizes = new() { Size.Medium }, PricePerDay = 125 },
            new BCD { ProductId = 3, Category = ProductCategory.BCD, Brand = "Scubapro", Model = "Navigator Lite BCD", Sizes = new() { Size.Large }, PricePerDay = 125 },
            new BCD { ProductId = 4, Category = ProductCategory.BCD, Brand = "Scubapro", Model = "BCD Glide", Sizes = new() { Size.Small }, PricePerDay = 140 },
            new BCD { ProductId = 5, Category = ProductCategory.BCD, Brand = "Scubapro", Model = "BCD Glide", Sizes = new() { Size.Medium }, PricePerDay = 140 },
            new BCD { ProductId = 6, Category = ProductCategory.BCD, Brand = "Scubapro", Model = "BCD Glide", Sizes = new() { Size.Large }, PricePerDay = 140 },
            new BCD { ProductId = 7, Category = ProductCategory.BCD, Brand = "Scubapro", Model = "BCD Hydros Pro", Sizes = new() { Size.Small }, PricePerDay = 200 },
            new BCD { ProductId = 8, Category = ProductCategory.BCD, Brand = "Scubapro", Model = "BCD Hydros Pro", Sizes = new() { Size.Medium }, PricePerDay = 200 },
            new BCD { ProductId = 9, Category = ProductCategory.BCD, Brand = "Scubapro", Model = "BCD Hydros Pro", Sizes = new() { Size.Large }, PricePerDay = 200 },
            new BCD { ProductId = 10, Category = ProductCategory.BCD, Brand = "Seac", Model = "BCD Modular", Sizes = new() { Size.Small }, PricePerDay = 145 },
            new BCD { ProductId = 11, Category = ProductCategory.BCD, Brand = "Seac", Model = "BCD Modular", Sizes = new() { Size.Medium }, PricePerDay = 145 },
            new BCD { ProductId = 12, Category = ProductCategory.BCD, Brand = "Seac", Model = "BCD Modular", Sizes = new() { Size.Large }, PricePerDay = 145 },

            // ===== DYKKERDRAGTER - DEFINITION 3MM =====
            // Herre
            new DiveSuit { ProductId = 13, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 14, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 15, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 16, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 17, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3 mm", PricePerDay = 100 },
            // Dame
            new DiveSuit { ProductId = 18, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 19, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 20, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 21, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 22, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3 mm", PricePerDay = 100 },

            // ===== DYKKERDRAGTER - DEFINITION 5MM =====
            // Herre
            new DiveSuit { ProductId = 23, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 24, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 25, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 26, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 27, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 100 },
            // Dame
            new DiveSuit { ProductId = 28, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 29, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 30, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 31, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 32, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 100 },

            // ===== DYKKERDRAGTER - DEFINITION 7MM =====
            // Herre
            new DiveSuit { ProductId = 33, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 34, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 35, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 36, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 37, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "7 mm", PricePerDay = 100 },
            // Dame
            new DiveSuit { ProductId = 38, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 39, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 40, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 41, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 42, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "7 mm", PricePerDay = 100 },

            // ===== DYKKERDRAGTER - W5 3.5MM =====
            // Herre
            new DiveSuit { ProductId = 43, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 44, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 45, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 46, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 47, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3.5 mm", PricePerDay = 100 },
            // Dame
            new DiveSuit { ProductId = 48, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 49, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 50, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 51, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 52, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3.5 mm", PricePerDay = 100 },

            // ===== DYKKERDRAGTER - PROTEUS 5MM =====
            // Herre
            new DiveSuit { ProductId = 53, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { ProductId = 54, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { ProductId = 55, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { ProductId = 56, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { ProductId = 57, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 120 },
            // Dame
            new DiveSuit { ProductId = 58, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { ProductId = 59, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { ProductId = 60, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { ProductId = 61, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { ProductId = 62, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 120 },

            // ===== DYKKERDRAGTER - EXODRY 4.0 =====
            // Herre
            new DiveSuit { ProductId = 63, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 300 },
            new DiveSuit { ProductId = 64, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 300 },
            new DiveSuit { ProductId = 65, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 300 },
            new DiveSuit { ProductId = 66, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 300 },
            new DiveSuit { ProductId = 67, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 300 },
            // Dame
            new DiveSuit { ProductId = 68, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 300 },
            new DiveSuit { ProductId = 69, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 300 },
            new DiveSuit { ProductId = 70, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 300 },
            new DiveSuit { ProductId = 71, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 300 },
            new DiveSuit { ProductId = 72, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 300 },

            // ===== DYKKERDRAGTER - D7 EVO =====
            // Herre
            new DiveSuit { ProductId = 73, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 320 },
            new DiveSuit { ProductId = 74, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 320 },
            new DiveSuit { ProductId = 75, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 320 },
            new DiveSuit { ProductId = 76, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 320 },
            new DiveSuit { ProductId = 77, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 320 },
            // Dame
            new DiveSuit { ProductId = 78, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 320 },
            new DiveSuit { ProductId = 79, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 320 },
            new DiveSuit { ProductId = 80, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 320 },
            new DiveSuit { ProductId = 81, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 320 },
            new DiveSuit { ProductId = 82, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 320 },

            // ===== DYKKERDRAGTER - E.LITE PLUS =====
            // Herre
            new DiveSuit { ProductId = 83, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 350 },
            new DiveSuit { ProductId = 84, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 350 },
            new DiveSuit { ProductId = 85, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 350 },
            new DiveSuit { ProductId = 86, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 350 },
            new DiveSuit { ProductId = 87, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 350 },
            // Dame
            new DiveSuit { ProductId = 88, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 350 },
            new DiveSuit { ProductId = 89, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 350 },
            new DiveSuit { ProductId = 90, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 350 },
            new DiveSuit { ProductId = 91, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 350 },
            new DiveSuit { ProductId = 92, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 350 },

            // ===== TANKE =====
            new Tank { ProductId = 93, Category = ProductCategory.Tank, Brand = "Scubapro", Model = "5 liter tank", Sizes = new() { Size.Small }, PricePerDay = 150 },
            new Tank { ProductId = 94, Category = ProductCategory.Tank, Brand = "Scubapro", Model = "10 liter tank", Sizes = new() { Size.Medium }, PricePerDay = 160 },
            new Tank { ProductId = 95, Category = ProductCategory.Tank, Brand = "Scubapro", Model = "12 liter tank", Sizes = new() { Size.Large }, PricePerDay = 170 },
            new Tank { ProductId = 96, Category = ProductCategory.Tank, Brand = "Scubapro", Model = "15 liter tank", Sizes = new() { Size.XtraLarge }, PricePerDay = 180 },

            // ===== REGULATORSÆT =====
            new RegulatorSet { ProductId = 97, Category = ProductCategory.RegulatorSet, Brand = "Scubapro", Model = "", FirstStep = "MK25EVO", SecondStep = "S600", Octopus = "R105", PricePerDay = 125 },
            new RegulatorSet { ProductId = 98, Category = ProductCategory.RegulatorSet, Brand = "Scubapro", Model = "", FirstStep = "MK17EVO", SecondStep = "C370", Octopus = "R095", PricePerDay = 100 },
            new RegulatorSet { ProductId = 99, Category = ProductCategory.RegulatorSet, Brand = "Scubapro", Model = "", FirstStep = "MK25EVO BT", SecondStep = "A700 Carbon BT", Octopus = "S270", PricePerDay = 150 },

            // ===== MASKE/SNORKEL =====
            new MaskSnorkel { ProductId = 100, Category = ProductCategory.MaskSnorkel, Brand = "Scubapro", Model = "Ghost", Sizes = new() { Size.Small }, PricePerDay = 50 },
            new MaskSnorkel { ProductId = 101, Category = ProductCategory.MaskSnorkel, Brand = "Scubapro", Model = "D-Mask", Sizes = new() { Size.Medium }, PricePerDay = 60 },
            new MaskSnorkel { ProductId = 102, Category = ProductCategory.MaskSnorkel, Brand = "Scubapro", Model = "Spectra Mini", Sizes = new() { Size.Large }, PricePerDay = 50 },
            new MaskSnorkel { ProductId = 103, Category = ProductCategory.MaskSnorkel, Brand = "Scubapro", Model = "Crystal VU", Sizes = new() { Size.XtraLarge }, PricePerDay = 75 },
            new MaskSnorkel { ProductId = 104, Category = ProductCategory.MaskSnorkel, Brand = "Fourth Element", Model = "Scout Kontrast", Sizes = new() { Size.Small }, PricePerDay = 75 },
            new MaskSnorkel { ProductId = 105, Category = ProductCategory.MaskSnorkel, Brand = "Fourth Element", Model = "Scout Enhance", Sizes = new() { Size.Medium }, PricePerDay = 75 },
            new MaskSnorkel { ProductId = 106, Category = ProductCategory.MaskSnorkel, Brand = "Tusa", Model = "Element", Sizes = new() { Size.Large }, PricePerDay = 75 },

            // ===== FINNER =====
            new Fins { ProductId = 107, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Jet Fin", Sizes = new() { Size.XtraSmall }, PricePerDay = 50 },
            new Fins { ProductId = 108, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Jet Fin", Sizes = new() { Size.Small }, PricePerDay = 50 },
            new Fins { ProductId = 109, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Jet Fin", Sizes = new() { Size.Medium }, PricePerDay = 50 },
            new Fins { ProductId = 110, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Jet Fin", Sizes = new() { Size.Large }, PricePerDay = 50 },
            new Fins { ProductId = 111, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Jet Fin", Sizes = new() { Size.XtraLarge }, PricePerDay = 50 },
            new Fins { ProductId = 112, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "GO Travel", Sizes = new() { Size.XtraSmall }, PricePerDay = 50 },
            new Fins { ProductId = 113, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "GO Travel", Sizes = new() { Size.Small }, PricePerDay = 50 },
            new Fins { ProductId = 114, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "GO Travel", Sizes = new() { Size.Medium }, PricePerDay = 50 },
            new Fins { ProductId = 115, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "GO Travel", Sizes = new() { Size.Large }, PricePerDay = 50 },
            new Fins { ProductId = 116, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "GO Travel", Sizes = new() { Size.XtraLarge }, PricePerDay = 50 },
            new Fins { ProductId = 117, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Seawing Supernova", Sizes = new() { Size.XtraSmall }, PricePerDay = 60 },
            new Fins { ProductId = 118, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Seawing Supernova", Sizes = new() { Size.Small }, PricePerDay = 60 },
            new Fins { ProductId = 119, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Seawing Supernova", Sizes = new() { Size.Medium }, PricePerDay = 60 },
            new Fins { ProductId = 120, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Seawing Supernova", Sizes = new() { Size.Large }, PricePerDay = 60 },
            new Fins { ProductId = 121, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Seawing Supernova", Sizes = new() { Size.XtraLarge }, PricePerDay = 60 },
            new Fins { ProductId = 122, Category = ProductCategory.Fins, Brand = "Seac", Model = "Propulsion", Sizes = new() { Size.XtraSmall }, PricePerDay = 50 },
            new Fins { ProductId = 123, Category = ProductCategory.Fins, Brand = "Seac", Model = "Propulsion", Sizes = new() { Size.Small }, PricePerDay = 50 },
            new Fins { ProductId = 124, Category = ProductCategory.Fins, Brand = "Seac", Model = "Propulsion", Sizes = new() { Size.Medium }, PricePerDay = 50 },
            new Fins { ProductId = 125, Category = ProductCategory.Fins, Brand = "Seac", Model = "Propulsion", Sizes = new() { Size.Large }, PricePerDay = 50 },
            new Fins { ProductId = 126, Category = ProductCategory.Fins, Brand = "Seac", Model = "Propulsion", Sizes = new() { Size.XtraLarge }, PricePerDay = 50 },
            new Fins { ProductId = 127, Category = ProductCategory.Fins, Brand = "Seac", Model = "ALA", Sizes = new() { Size.XtraSmall }, PricePerDay = 50 },
            new Fins { ProductId = 128, Category = ProductCategory.Fins, Brand = "Seac", Model = "ALA", Sizes = new() { Size.Small }, PricePerDay = 50 },
            new Fins { ProductId = 129, Category = ProductCategory.Fins, Brand = "Seac", Model = "ALA", Sizes = new() { Size.Medium }, PricePerDay = 50 },
            new Fins { ProductId = 130, Category = ProductCategory.Fins, Brand = "Seac", Model = "ALA", Sizes = new() { Size.Large }, PricePerDay = 50 },
            new Fins { ProductId = 131, Category = ProductCategory.Fins, Brand = "Seac", Model = "ALA", Sizes = new() { Size.XtraLarge }, PricePerDay = 50 },
            new Fins { ProductId = 132, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Tech", Sizes = new() { Size.XtraSmall }, PricePerDay = 75 },
            new Fins { ProductId = 133, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Tech", Sizes = new() { Size.Small }, PricePerDay = 75 },
            new Fins { ProductId = 134, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Tech", Sizes = new() { Size.Medium }, PricePerDay = 75 },
            new Fins { ProductId = 135, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Tech", Sizes = new() { Size.Large }, PricePerDay = 75 },
            new Fins { ProductId = 136, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Tech", Sizes = new() { Size.XtraLarge }, PricePerDay = 75 },
            new Fins { ProductId = 137, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Rec Fin", Sizes = new() { Size.XtraSmall }, PricePerDay = 80 },
            new Fins { ProductId = 138, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Rec Fin", Sizes = [Size.Small], PricePerDay = 80 },
            new Fins { ProductId = 139, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Rec Fin", Sizes = [Size.Medium], PricePerDay = 80 },
            new Fins { ProductId = 140, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Rec Fin", Sizes = [Size.Large], PricePerDay = 80 },
            new Fins { ProductId = 141, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Rec Fin", Sizes = [Size.XtraLarge], PricePerDay = 80 }
        };
        public static List<Product> GetAll() => products;
        public static Product? GetById(int id) => products.FirstOrDefault(product => product.ProductId == id);
        public static List<Product> GetByCategory(ProductCategory category) => products.Where(p => p.Category == category).ToList();

        // Get all distinct productcategories
        public static List<ProductCategory> GetProductCategories()
        {
            return GetAll().Select(p => p.Category).Distinct().ToList();
        }

        public static void Add(Product product)
        {
            if (product == null) return;
            product.ProductId = products.Any() ? products.Max(p => p.ProductId) + 1 : 1;
            products.Add(product);
        }

        public static void Delete(int id) => products.RemoveAll(p => p.ProductId == id);

        public static void Update(int id, Product product)
        {
            var existing = GetById(id);
            if (existing == null || product == null) return;
            existing.Brand = product.Brand;
            existing.Model = product.Model;
            existing.PricePerDay = product.PricePerDay;
        }
        public static List<Product> GetVariants(string brand, string model)
        {
            return products
                .Where(p =>
                    p.Brand == brand &&
                    p.Model == model)
                .ToList();
        }

    }
}