using Dive_deep.Models;
using static Dive_deep.Models.Enums;
using System.Linq;

namespace DiveDeep.Persistence
{
    public static class ProductRepository
    {
        private static readonly List<Product> products = new()
        {
            // ===== BCD =====
            new Bcd { Id = 1, Category = ProductCategory.Bdc, Brand = "Scubapro", Model = "Navigator Lite BCD", Sizes = new() { Size.Small }, PricePerDay = 125 },
            new Bcd { Id = 2, Category = ProductCategory.Bdc, Brand = "Scubapro", Model = "Navigator Lite BCD", Sizes = new() { Size.Medium }, PricePerDay = 125 },
            new Bcd { Id = 3, Category = ProductCategory.Bdc, Brand = "Scubapro", Model = "Navigator Lite BCD", Sizes = new() { Size.Large }, PricePerDay = 125 },
            new Bcd { Id = 4, Category = ProductCategory.Bdc, Brand = "Scubapro", Model = "BCD Glide", Sizes = new() { Size.Small }, PricePerDay = 140 },
            new Bcd { Id = 5, Category = ProductCategory.Bdc, Brand = "Scubapro", Model = "BCD Glide", Sizes = new() { Size.Medium }, PricePerDay = 140 },
            new Bcd { Id = 6, Category = ProductCategory.Bdc, Brand = "Scubapro", Model = "BCD Glide", Sizes = new() { Size.Large }, PricePerDay = 140 },
            new Bcd { Id = 7, Category = ProductCategory.Bdc, Brand = "Scubapro", Model = "BCD Hydros Pro", Sizes = new() { Size.Small }, PricePerDay = 200 },
            new Bcd { Id = 8, Category = ProductCategory.Bdc, Brand = "Scubapro", Model = "BCD Hydros Pro", Sizes = new() { Size.Medium }, PricePerDay = 200 },
            new Bcd { Id = 9, Category = ProductCategory.Bdc, Brand = "Scubapro", Model = "BCD Hydros Pro", Sizes = new() { Size.Large }, PricePerDay = 200 },
            new Bcd { Id = 10, Category = ProductCategory.Bdc, Brand = "Seac", Model = "BCD Modular", Sizes = new() { Size.Small }, PricePerDay = 145 },
            new Bcd { Id = 11, Category = ProductCategory.Bdc, Brand = "Seac", Model = "BCD Modular", Sizes = new() { Size.Medium }, PricePerDay = 145 },
            new Bcd { Id = 12, Category = ProductCategory.Bdc, Brand = "Seac", Model = "BCD Modular", Sizes = new() { Size.Large }, PricePerDay = 145 },

            // ===== DYKKERDRAGTER - DEFINITION 3MM =====
            // Herre
            new DiveSuit { Id = 13, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { Id = 14, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { Id = 15, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { Id = 16, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { Id = 17, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3 mm", PricePerDay = 100 },
            // Dame
            new DiveSuit { Id = 18, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { Id = 19, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { Id = 20, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { Id = 21, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { Id = 22, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3 mm", PricePerDay = 100 },

            // ===== DYKKERDRAGTER - DEFINITION 5MM =====
            // Herre
            new DiveSuit { Id = 23, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { Id = 24, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { Id = 25, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { Id = 26, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { Id = 27, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 100 },
            // Dame
            new DiveSuit { Id = 28, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { Id = 29, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { Id = 30, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { Id = 31, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { Id = 32, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 100 },

            // ===== DYKKERDRAGTER - DEFINITION 7MM =====
            // Herre
            new DiveSuit { Id = 33, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { Id = 34, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { Id = 35, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { Id = 36, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { Id = 37, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "7 mm", PricePerDay = 100 },
            // Dame
            new DiveSuit { Id = 38, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { Id = 39, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { Id = 40, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { Id = 41, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { Id = 42, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Definition", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "7 mm", PricePerDay = 100 },

            // ===== DYKKERDRAGTER - W5 3.5MM =====
            // Herre
            new DiveSuit { Id = 43, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { Id = 44, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { Id = 45, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { Id = 46, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { Id = 47, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "3.5 mm", PricePerDay = 100 },
            // Dame
            new DiveSuit { Id = 48, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { Id = 49, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { Id = 50, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { Id = 51, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { Id = 52, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "W5", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "3.5 mm", PricePerDay = 100 },

            // ===== DYKKERDRAGTER - PROTEUS 5MM =====
            // Herre
            new DiveSuit { Id = 53, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { Id = 54, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { Id = 55, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { Id = 56, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { Id = 57, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Herre", Thickness = "5 mm", PricePerDay = 120 },
            // Dame
            new DiveSuit { Id = 58, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { Id = 59, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { Id = 60, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { Id = 61, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { Id = 62, Category = ProductCategory.DiveSuit, Brand = "Fourth Element", Model = "Proteus", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.Wetsuit }, Gender = "Dame", Thickness = "5 mm", PricePerDay = 120 },

            // ===== DYKKERDRAGTER - EXODRY 4.0 =====
            // Herre
            new DiveSuit { Id = 63, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 300 },
            new DiveSuit { Id = 64, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 300 },
            new DiveSuit { Id = 65, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 300 },
            new DiveSuit { Id = 66, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 300 },
            new DiveSuit { Id = 67, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 300 },
            // Dame
            new DiveSuit { Id = 68, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 300 },
            new DiveSuit { Id = 69, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 300 },
            new DiveSuit { Id = 70, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 300 },
            new DiveSuit { Id = 71, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 300 },
            new DiveSuit { Id = 72, Category = ProductCategory.DiveSuit, Brand = "Scubapro", Model = "Exodry 4.0", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 300 },

            // ===== DYKKERDRAGTER - D7 EVO =====
            // Herre
            new DiveSuit { Id = 73, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 320 },
            new DiveSuit { Id = 74, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 320 },
            new DiveSuit { Id = 75, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 320 },
            new DiveSuit { Id = 76, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 320 },
            new DiveSuit { Id = 77, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 320 },
            // Dame
            new DiveSuit { Id = 78, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 320 },
            new DiveSuit { Id = 79, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 320 },
            new DiveSuit { Id = 80, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 320 },
            new DiveSuit { Id = 81, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 320 },
            new DiveSuit { Id = 82, Category = ProductCategory.DiveSuit, Brand = "Waterproof", Model = "D7 Evo", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 320 },

            // ===== DYKKERDRAGTER - E.LITE PLUS =====
            // Herre
            new DiveSuit { Id = 83, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 350 },
            new DiveSuit { Id = 84, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 350 },
            new DiveSuit { Id = 85, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 350 },
            new DiveSuit { Id = 86, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 350 },
            new DiveSuit { Id = 87, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.drysuit }, Gender = "Herre", Thickness = null, PricePerDay = 350 },
            // Dame
            new DiveSuit { Id = 88, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.XtraSmall }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 350 },
            new DiveSuit { Id = 89, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.Small }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 350 },
            new DiveSuit { Id = 90, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.Medium }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 350 },
            new DiveSuit { Id = 91, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.Large }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 350 },
            new DiveSuit { Id = 92, Category = ProductCategory.DiveSuit, Brand = "Santi", Model = "E.Lite Plus", Sizes = new() { Size.XtraLarge }, SuitTypes = new() { SuitType.drysuit }, Gender = "Dame", Thickness = null, PricePerDay = 350 },

            // ===== TANKE =====
            new Tank { Id = 93, Category = ProductCategory.Tank, Brand = "Scubapro", Model = "5 liter tank", Sizes = new() { Size.Small }, PricePerDay = 150 },
            new Tank { Id = 94, Category = ProductCategory.Tank, Brand = "Scubapro", Model = "10 liter tank", Sizes = new() { Size.Medium }, PricePerDay = 160 },
            new Tank { Id = 95, Category = ProductCategory.Tank, Brand = "Scubapro", Model = "12 liter tank", Sizes = new() { Size.Large }, PricePerDay = 170 },
            new Tank { Id = 96, Category = ProductCategory.Tank, Brand = "Scubapro", Model = "15 liter tank", Sizes = new() { Size.XtraLarge }, PricePerDay = 180 },

            // ===== REGULATORSÆT =====
            new RegulatorSet { Id = 97, Category = ProductCategory.RegulatorSet, Brand = "Scubapro", Model = "MK25EVO / S600", FirstStep = "MK25EVO", SecondStep = "S600", Octopus = "R105", PricePerDay = 125 },
            new RegulatorSet { Id = 98, Category = ProductCategory.RegulatorSet, Brand = "Scubapro", Model = "MK17EVO / C370", FirstStep = "MK17EVO", SecondStep = "C370", Octopus = "R095", PricePerDay = 100 },
            new RegulatorSet { Id = 99, Category = ProductCategory.RegulatorSet, Brand = "Scubapro", Model = "MK25EVO BT / A700 Carbon BT", FirstStep = "MK25EVO BT", SecondStep = "A700 Carbon BT", Octopus = "S270", PricePerDay = 150 },

            // ===== MASKE/SNORKEL =====
            new MaskSnorkel { Id = 100, Category = ProductCategory.MaskSnorkel, Brand = "Scubapro", Model = "Ghost", Sizes = new() { Size.Small }, PricePerDay = 50 },
            new MaskSnorkel { Id = 101, Category = ProductCategory.MaskSnorkel, Brand = "Scubapro", Model = "D-Mask", Sizes = new() { Size.Medium }, PricePerDay = 60 },
            new MaskSnorkel { Id = 102, Category = ProductCategory.MaskSnorkel, Brand = "Scubapro", Model = "Spectra Mini", Sizes = new() { Size.Large }, PricePerDay = 50 },
            new MaskSnorkel { Id = 103, Category = ProductCategory.MaskSnorkel, Brand = "Scubapro", Model = "Crystal VU", Sizes = new() { Size.XtraLarge }, PricePerDay = 75 },
            new MaskSnorkel { Id = 104, Category = ProductCategory.MaskSnorkel, Brand = "Fourth Element", Model = "Scout Kontrast", Sizes = new() { Size.Small }, PricePerDay = 75 },
            new MaskSnorkel { Id = 105, Category = ProductCategory.MaskSnorkel, Brand = "Fourth Element", Model = "Scout Enhance", Sizes = new() { Size.Medium }, PricePerDay = 75 },
            new MaskSnorkel { Id = 106, Category = ProductCategory.MaskSnorkel, Brand = "Tusa", Model = "Element", Sizes = new() { Size.Large }, PricePerDay = 75 },

            // ===== FINNER =====
            new Fins { Id = 107, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Jet Fin", Sizes = new() { Size.XtraSmall }, PricePerDay = 50 },
            new Fins { Id = 108, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Jet Fin", Sizes = new() { Size.Small }, PricePerDay = 50 },
            new Fins { Id = 109, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Jet Fin", Sizes = new() { Size.Medium }, PricePerDay = 50 },
            new Fins { Id = 110, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Jet Fin", Sizes = new() { Size.Large }, PricePerDay = 50 },
            new Fins { Id = 111, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Jet Fin", Sizes = new() { Size.XtraLarge }, PricePerDay = 50 },
            new Fins { Id = 112, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "GO Travel", Sizes = new() { Size.XtraSmall }, PricePerDay = 50 },
            new Fins { Id = 113, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "GO Travel", Sizes = new() { Size.Small }, PricePerDay = 50 },
            new Fins { Id = 114, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "GO Travel", Sizes = new() { Size.Medium }, PricePerDay = 50 },
            new Fins { Id = 115, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "GO Travel", Sizes = new() { Size.Large }, PricePerDay = 50 },
            new Fins { Id = 116, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "GO Travel", Sizes = new() { Size.XtraLarge }, PricePerDay = 50 },
            new Fins { Id = 117, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Seawing Supernova", Sizes = new() { Size.XtraSmall }, PricePerDay = 60 },
            new Fins { Id = 118, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Seawing Supernova", Sizes = new() { Size.Small }, PricePerDay = 60 },
            new Fins { Id = 119, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Seawing Supernova", Sizes = new() { Size.Medium }, PricePerDay = 60 },
            new Fins { Id = 120, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Seawing Supernova", Sizes = new() { Size.Large }, PricePerDay = 60 },
            new Fins { Id = 121, Category = ProductCategory.Fins, Brand = "Scubapro", Model = "Seawing Supernova", Sizes = new() { Size.XtraLarge }, PricePerDay = 60 },
            new Fins { Id = 122, Category = ProductCategory.Fins, Brand = "Seac", Model = "Propulsion", Sizes = new() { Size.XtraSmall }, PricePerDay = 50 },
            new Fins { Id = 123, Category = ProductCategory.Fins, Brand = "Seac", Model = "Propulsion", Sizes = new() { Size.Small }, PricePerDay = 50 },
            new Fins { Id = 124, Category = ProductCategory.Fins, Brand = "Seac", Model = "Propulsion", Sizes = new() { Size.Medium }, PricePerDay = 50 },
            new Fins { Id = 125, Category = ProductCategory.Fins, Brand = "Seac", Model = "Propulsion", Sizes = new() { Size.Large }, PricePerDay = 50 },
            new Fins { Id = 126, Category = ProductCategory.Fins, Brand = "Seac", Model = "Propulsion", Sizes = new() { Size.XtraLarge }, PricePerDay = 50 },
            new Fins { Id = 127, Category = ProductCategory.Fins, Brand = "Seac", Model = "ALA", Sizes = new() { Size.XtraSmall }, PricePerDay = 50 },
            new Fins { Id = 128, Category = ProductCategory.Fins, Brand = "Seac", Model = "ALA", Sizes = new() { Size.Small }, PricePerDay = 50 },
            new Fins { Id = 129, Category = ProductCategory.Fins, Brand = "Seac", Model = "ALA", Sizes = new() { Size.Medium }, PricePerDay = 50 },
            new Fins { Id = 130, Category = ProductCategory.Fins, Brand = "Seac", Model = "ALA", Sizes = new() { Size.Large }, PricePerDay = 50 },
            new Fins { Id = 131, Category = ProductCategory.Fins, Brand = "Seac", Model = "ALA", Sizes = new() { Size.XtraLarge }, PricePerDay = 50 },
            new Fins { Id = 132, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Tech", Sizes = new() { Size.XtraSmall }, PricePerDay = 75 },
            new Fins { Id = 133, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Tech", Sizes = new() { Size.Small }, PricePerDay = 75 },
            new Fins { Id = 134, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Tech", Sizes = new() { Size.Medium }, PricePerDay = 75 },
            new Fins { Id = 135, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Tech", Sizes = new() { Size.Large }, PricePerDay = 75 },
            new Fins { Id = 136, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Tech", Sizes = new() { Size.XtraLarge }, PricePerDay = 75 },
            new Fins { Id = 137, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Rec Fin", Sizes = new() { Size.XtraSmall }, PricePerDay = 80 },
            new Fins { Id = 138, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Rec Fin", Sizes = new() { Size.Small }, PricePerDay = 80 },
            new Fins { Id = 139, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Rec Fin", Sizes = new() { Size.Medium }, PricePerDay = 80 },
            new Fins { Id = 140, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Rec Fin", Sizes = new() { Size.Large }, PricePerDay = 80 },
            new Fins { Id = 141, Category = ProductCategory.Fins, Brand = "Fourth Element", Model = "Rec Fin", Sizes = new() { Size.XtraLarge }, PricePerDay = 80 }
        };

        public static List<Product> GetAll() => products;
        public static Product? GetById(int id) => products.FirstOrDefault(product => product.Id == id);
        public static List<Product> GetByCategory(ProductCategory category) => products.Where(p => p.Category == category).ToList();

        // Get all distinct productcategories
        public static List<ProductCategory> GetProductCategories()
        {
            return GetAll().Select(p => p.Category).Distinct().ToList();
        }

        public static void Add(Product product)
        {
            if (product == null) return;
            product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);
        }

        public static void Delete(int id) => products.RemoveAll(p => p.Id == id);

        public static void Update(int id, Product product)
        {
            var existing = GetById(id);
            if (existing == null || product == null) return;
            existing.Brand = product.Brand;
            existing.Model = product.Model;
            existing.PricePerDay = product.PricePerDay;
        }
    }
}