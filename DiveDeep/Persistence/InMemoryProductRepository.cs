using DiveDeep.Models;

namespace DiveDeep.Persistence
{
    public static class InMemoryProductRepository
    {
        private static readonly List<Product> products = new()
        {
            // ===== BCD ===== (databladet har kun S, M, L)
            new BCD { ProductId = 1, Brand = "Scubapro", Model = "Navigator Lite BCD", Size = Size.Small, PricePerDay = 125 },
            new BCD { ProductId = 2, Brand = "Scubapro", Model = "Navigator Lite BCD", Size = Size.Medium, PricePerDay = 125 },
            new BCD { ProductId = 3, Brand = "Scubapro", Model = "Navigator Lite BCD", Size = Size.Large, PricePerDay = 125 },
            new BCD { ProductId = 4, Brand = "Scubapro", Model = "BCD Glide", Size = Size.Small, PricePerDay = 140 },
            new BCD { ProductId = 5, Brand = "Scubapro", Model = "BCD Glide", Size = Size.Medium, PricePerDay = 140 },
            new BCD { ProductId = 6, Brand = "Scubapro", Model = "BCD Glide", Size = Size.Large, PricePerDay = 140 },
            new BCD { ProductId = 7, Brand = "Scubapro", Model = "BCD Hydros Pro", Size = Size.Small, PricePerDay = 200 },
            new BCD { ProductId = 8, Brand = "Scubapro", Model = "BCD Hydros Pro", Size = Size.Medium, PricePerDay = 200 },
            new BCD { ProductId = 9, Brand = "Scubapro", Model = "BCD Hydros Pro", Size = Size.Large, PricePerDay = 200 },
            new BCD { ProductId = 10, Brand = "Seac", Model = "BCD Modular", Size = Size.Small, PricePerDay = 145 },
            new BCD { ProductId = 11, Brand = "Seac", Model = "BCD Modular", Size = Size.Medium, PricePerDay = 145 },
            new BCD { ProductId = 12, Brand = "Seac", Model = "BCD Modular", Size = Size.Large, PricePerDay = 145 },

            // ===== DYKKERDRAGTER - DEFINITION 3MM =====
            // Herre
            new DiveSuit { ProductId = 13, Brand = "Scubapro", Model = "Definition", Size = Size.XtraSmall, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 14, Brand = "Scubapro", Model = "Definition", Size = Size.Small, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 15, Brand = "Scubapro", Model = "Definition", Size = Size.Medium, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 16, Brand = "Scubapro", Model = "Definition", Size = Size.Large, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 17, Brand = "Scubapro", Model = "Definition", Size = Size.XtraLarge, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "3 mm", PricePerDay = 100 },
            // Dame
            new DiveSuit { ProductId = 18, Brand = "Scubapro", Model = "Definition", Size = Size.XtraSmall, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 19, Brand = "Scubapro", Model = "Definition", Size = Size.Small, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 20, Brand = "Scubapro", Model = "Definition", Size = Size.Medium, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 21, Brand = "Scubapro", Model = "Definition", Size = Size.Large, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "3 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 22, Brand = "Scubapro", Model = "Definition", Size = Size.XtraLarge, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "3 mm", PricePerDay = 100 },

            // ===== DYKKERDRAGTER - DEFINITION 5MM =====
            // Herre
            new DiveSuit { ProductId = 23, Brand = "Scubapro", Model = "Definition", Size = Size.XtraSmall, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 24, Brand = "Scubapro", Model = "Definition", Size = Size.Small, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 25, Brand = "Scubapro", Model = "Definition", Size = Size.Medium, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 26, Brand = "Scubapro", Model = "Definition", Size = Size.Large, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 27, Brand = "Scubapro", Model = "Definition", Size = Size.XtraLarge, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "5 mm", PricePerDay = 100 },
            // Dame
            new DiveSuit { ProductId = 28, Brand = "Scubapro", Model = "Definition", Size = Size.XtraSmall, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 29, Brand = "Scubapro", Model = "Definition", Size = Size.Small, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 30, Brand = "Scubapro", Model = "Definition", Size = Size.Medium, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 31, Brand = "Scubapro", Model = "Definition", Size = Size.Large, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 32, Brand = "Scubapro", Model = "Definition", Size = Size.XtraLarge, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "5 mm", PricePerDay = 100 },

            // ===== DYKKERDRAGTER - DEFINITION 7MM =====
            // Herre
            new DiveSuit { ProductId = 33, Brand = "Scubapro", Model = "Definition", Size = Size.XtraSmall, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 34, Brand = "Scubapro", Model = "Definition", Size = Size.Small, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 35, Brand = "Scubapro", Model = "Definition", Size = Size.Medium, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 36, Brand = "Scubapro", Model = "Definition", Size = Size.Large, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 37, Brand = "Scubapro", Model = "Definition", Size = Size.XtraLarge, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "7 mm", PricePerDay = 100 },
            // Dame
            new DiveSuit { ProductId = 38, Brand = "Scubapro", Model = "Definition", Size = Size.XtraSmall, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 39, Brand = "Scubapro", Model = "Definition", Size = Size.Small, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 40, Brand = "Scubapro", Model = "Definition", Size = Size.Medium, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 41, Brand = "Scubapro", Model = "Definition", Size = Size.Large, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "7 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 42, Brand = "Scubapro", Model = "Definition", Size = Size.XtraLarge, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "7 mm", PricePerDay = 100 },

            // ===== DYKKERDRAGTER - W5 3.5MM =====
            // Herre
            new DiveSuit { ProductId = 43, Brand = "Waterproof", Model = "W5", Size = Size.XtraSmall, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 44, Brand = "Waterproof", Model = "W5", Size = Size.Small, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 45, Brand = "Waterproof", Model = "W5", Size = Size.Medium, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 46, Brand = "Waterproof", Model = "W5", Size = Size.Large, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 47, Brand = "Waterproof", Model = "W5", Size = Size.XtraLarge, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "3.5 mm", PricePerDay = 100 },
            // Dame
            new DiveSuit { ProductId = 48, Brand = "Waterproof", Model = "W5", Size = Size.XtraSmall, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 49, Brand = "Waterproof", Model = "W5", Size = Size.Small, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 50, Brand = "Waterproof", Model = "W5", Size = Size.Medium, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 51, Brand = "Waterproof", Model = "W5", Size = Size.Large, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "3.5 mm", PricePerDay = 100 },
            new DiveSuit { ProductId = 52, Brand = "Waterproof", Model = "W5", Size = Size.XtraLarge, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "3.5 mm", PricePerDay = 100 },

            // ===== DYKKERDRAGTER - PROTEUS 5MM =====
            // Herre
            new DiveSuit { ProductId = 53, Brand = "Fourth Element", Model = "Proteus", Size = Size.XtraSmall, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { ProductId = 54, Brand = "Fourth Element", Model = "Proteus", Size = Size.Small, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { ProductId = 55, Brand = "Fourth Element", Model = "Proteus", Size = Size.Medium, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { ProductId = 56, Brand = "Fourth Element", Model = "Proteus", Size = Size.Large, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { ProductId = 57, Brand = "Fourth Element", Model = "Proteus", Size = Size.XtraLarge, SuitType = SuitType.Wetsuit, Gender = "Herre", Thickness = "5 mm", PricePerDay = 120 },
            // Dame
            new DiveSuit { ProductId = 58, Brand = "Fourth Element", Model = "Proteus", Size = Size.XtraSmall, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { ProductId = 59, Brand = "Fourth Element", Model = "Proteus", Size = Size.Small, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { ProductId = 60, Brand = "Fourth Element", Model = "Proteus", Size = Size.Medium, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { ProductId = 61, Brand = "Fourth Element", Model = "Proteus", Size = Size.Large, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "5 mm", PricePerDay = 120 },
            new DiveSuit { ProductId = 62, Brand = "Fourth Element", Model = "Proteus", Size = Size.XtraLarge, SuitType = SuitType.Wetsuit, Gender = "Dame", Thickness = "5 mm", PricePerDay = 120 },

            // ===== DYKKERDRAGTER - EXODRY 4.0 (tørdragt, ingen tykkelse) =====
            // Herre
            new DiveSuit { ProductId = 63, Brand = "Scubapro", Model = "Exodry 4.0", Size = Size.XtraSmall, SuitType = SuitType.Drysuit, Gender = "Herre", Thickness = null, PricePerDay = 300 },
            new DiveSuit { ProductId = 64, Brand = "Scubapro", Model = "Exodry 4.0", Size = Size.Small, SuitType = SuitType.Drysuit, Gender = "Herre", Thickness = null, PricePerDay = 300 },
            new DiveSuit { ProductId = 65, Brand = "Scubapro", Model = "Exodry 4.0", Size = Size.Medium, SuitType = SuitType.Drysuit, Gender = "Herre", Thickness = null, PricePerDay = 300 },
            new DiveSuit { ProductId = 66, Brand = "Scubapro", Model = "Exodry 4.0", Size = Size.Large, SuitType = SuitType.Drysuit, Gender = "Herre", Thickness = null, PricePerDay = 300 },
            new DiveSuit { ProductId = 67, Brand = "Scubapro", Model = "Exodry 4.0", Size = Size.XtraLarge, SuitType = SuitType.Drysuit, Gender = "Herre", Thickness = null, PricePerDay = 300 },
            // Dame
            new DiveSuit { ProductId = 68, Brand = "Scubapro", Model = "Exodry 4.0", Size = Size.XtraSmall, SuitType = SuitType.Drysuit, Gender = "Dame", Thickness = null, PricePerDay = 300 },
            new DiveSuit { ProductId = 69, Brand = "Scubapro", Model = "Exodry 4.0", Size = Size.Small, SuitType = SuitType.Drysuit, Gender = "Dame", Thickness = null, PricePerDay = 300 },
            new DiveSuit { ProductId = 70, Brand = "Scubapro", Model = "Exodry 4.0", Size = Size.Medium, SuitType = SuitType.Drysuit, Gender = "Dame", Thickness = null, PricePerDay = 300 },
            new DiveSuit { ProductId = 71, Brand = "Scubapro", Model = "Exodry 4.0", Size = Size.Large, SuitType = SuitType.Drysuit, Gender = "Dame", Thickness = null, PricePerDay = 300 },
            new DiveSuit { ProductId = 72, Brand = "Scubapro", Model = "Exodry 4.0", Size = Size.XtraLarge, SuitType = SuitType.Drysuit, Gender = "Dame", Thickness = null, PricePerDay = 300 },

            // ===== DYKKERDRAGTER - D7 EVO (tørdragt) =====
            // Herre
            new DiveSuit { ProductId = 73, Brand = "Waterproof", Model = "D7 Evo", Size = Size.XtraSmall, SuitType = SuitType.Drysuit, Gender = "Herre", Thickness = null, PricePerDay = 320 },
            new DiveSuit { ProductId = 74, Brand = "Waterproof", Model = "D7 Evo", Size = Size.Small, SuitType = SuitType.Drysuit, Gender = "Herre", Thickness = null, PricePerDay = 320 },
            new DiveSuit { ProductId = 75, Brand = "Waterproof", Model = "D7 Evo", Size = Size.Medium, SuitType = SuitType.Drysuit, Gender = "Herre", Thickness = null, PricePerDay = 320 },
            new DiveSuit { ProductId = 76, Brand = "Waterproof", Model = "D7 Evo", Size = Size.Large, SuitType = SuitType.Drysuit, Gender = "Herre", Thickness = null, PricePerDay = 320 },
            new DiveSuit { ProductId = 77, Brand = "Waterproof", Model = "D7 Evo", Size = Size.XtraLarge, SuitType = SuitType.Drysuit, Gender = "Herre", Thickness = null, PricePerDay = 320 },
            // Dame
            new DiveSuit { ProductId = 78, Brand = "Waterproof", Model = "D7 Evo", Size = Size.XtraSmall, SuitType = SuitType.Drysuit, Gender = "Dame", Thickness = null, PricePerDay = 320 },
            new DiveSuit { ProductId = 79, Brand = "Waterproof", Model = "D7 Evo", Size = Size.Small, SuitType = SuitType.Drysuit, Gender = "Dame", Thickness = null, PricePerDay = 320 },
            new DiveSuit { ProductId = 80, Brand = "Waterproof", Model = "D7 Evo", Size = Size.Medium, SuitType = SuitType.Drysuit, Gender = "Dame", Thickness = null, PricePerDay = 320 },
            new DiveSuit { ProductId = 81, Brand = "Waterproof", Model = "D7 Evo", Size = Size.Large, SuitType = SuitType.Drysuit, Gender = "Dame", Thickness = null, PricePerDay = 320 },
            new DiveSuit { ProductId = 82, Brand = "Waterproof", Model = "D7 Evo", Size = Size.XtraLarge, SuitType = SuitType.Drysuit, Gender = "Dame", Thickness = null, PricePerDay = 320 },

            // ===== DYKKERDRAGTER - E.LITE PLUS (tørdragt) =====
            // Herre
            new DiveSuit { ProductId = 83, Brand = "Santi", Model = "E.Lite Plus", Size = Size.XtraSmall, SuitType = SuitType.Drysuit, Gender = "Herre", Thickness = null, PricePerDay = 350 },
            new DiveSuit { ProductId = 84, Brand = "Santi", Model = "E.Lite Plus", Size = Size.Small, SuitType = SuitType.Drysuit, Gender = "Herre", Thickness = null, PricePerDay = 350 },
            new DiveSuit { ProductId = 85, Brand = "Santi", Model = "E.Lite Plus", Size = Size.Medium, SuitType = SuitType.Drysuit, Gender = "Herre", Thickness = null, PricePerDay = 350 },
            new DiveSuit { ProductId = 86, Brand = "Santi", Model = "E.Lite Plus", Size = Size.Large, SuitType = SuitType.Drysuit, Gender = "Herre", Thickness = null, PricePerDay = 350 },
            new DiveSuit { ProductId = 87, Brand = "Santi", Model = "E.Lite Plus", Size = Size.XtraLarge, SuitType = SuitType.Drysuit, Gender = "Herre", Thickness = null, PricePerDay = 350 },
            // Dame
            new DiveSuit { ProductId = 88, Brand = "Santi", Model = "E.Lite Plus", Size = Size.XtraSmall, SuitType = SuitType.Drysuit, Gender = "Dame", Thickness = null, PricePerDay = 350 },
            new DiveSuit { ProductId = 89, Brand = "Santi", Model = "E.Lite Plus", Size = Size.Small, SuitType = SuitType.Drysuit, Gender = "Dame", Thickness = null, PricePerDay = 350 },
            new DiveSuit { ProductId = 90, Brand = "Santi", Model = "E.Lite Plus", Size = Size.Medium, SuitType = SuitType.Drysuit, Gender = "Dame", Thickness = null, PricePerDay = 350 },
            new DiveSuit { ProductId = 91, Brand = "Santi", Model = "E.Lite Plus", Size = Size.Large, SuitType = SuitType.Drysuit, Gender = "Dame", Thickness = null, PricePerDay = 350 },
            new DiveSuit { ProductId = 92, Brand = "Santi", Model = "E.Lite Plus", Size = Size.XtraLarge, SuitType = SuitType.Drysuit, Gender = "Dame", Thickness = null, PricePerDay = 350 },

            // ===== TANKE =====
            // Samme Model på alle fire, så de bliver til ét produktkort med en "Volumen"-dropdown.
            new Tank { ProductId = 93, Brand = "Scubapro", Model = "Dykkertank", VolumeLiters = 5, PricePerDay = 150 },
            new Tank { ProductId = 94, Brand = "Scubapro", Model = "Dykkertank", VolumeLiters = 10, PricePerDay = 160 },
            new Tank { ProductId = 95, Brand = "Scubapro", Model = "Dykkertank", VolumeLiters = 12, PricePerDay = 170 },
            new Tank { ProductId = 96, Brand = "Scubapro", Model = "Dykkertank", VolumeLiters = 15, PricePerDay = 180 },

            // ===== REGULATORSÆT =====
            // Model var tom før, hvilket gav tre blanke valg i dropdown'en.
            new RegulatorSet { ProductId = 97, Brand = "Scubapro", Model = "Regulatorsæt", FirstStep = "MK25EVO", SecondStep = "S600", Octopus = "R105", PricePerDay = 125 },
            new RegulatorSet { ProductId = 98, Brand = "Scubapro", Model = "Regulatorsæt", FirstStep = "MK17EVO", SecondStep = "C370", Octopus = "R095", PricePerDay = 100 },
            new RegulatorSet { ProductId = 99, Brand = "Scubapro", Model = "Regulatorsæt", FirstStep = "MK25EVO BT", SecondStep = "A700 Carbon BT", Octopus = "S270", PricePerDay = 150 },

            // ===== MASKE/SNORKEL =====
            // OBS: databladet har ingen størrelser for masker/snorkler - størrelserne her er vores eget tilføj.
            new MaskSnorkel { ProductId = 100, Brand = "Scubapro", Model = "Ghost", PricePerDay = 50 },
            new MaskSnorkel { ProductId = 101, Brand = "Scubapro", Model = "D-Mask", PricePerDay = 60 },
            new MaskSnorkel { ProductId = 102, Brand = "Scubapro", Model = "Spectra Mini", PricePerDay = 50 },
            new MaskSnorkel { ProductId = 103, Brand = "Scubapro", Model = "Crystal VU", PricePerDay = 75 },
            new MaskSnorkel { ProductId = 104, Brand = "Fourth Element", Model = "Scout Kontrast", PricePerDay = 75 },
            new MaskSnorkel { ProductId = 105, Brand = "Fourth Element", Model = "Scout Enhance", PricePerDay = 75 },
            new MaskSnorkel { ProductId = 106, Brand = "Tusa", Model = "Element", PricePerDay = 75 },

            // ===== FINNER =====
            new Fins { ProductId = 107, Brand = "Scubapro", Model = "Jet Fin", Size = Size.XtraSmall, PricePerDay = 50 },
            new Fins { ProductId = 108, Brand = "Scubapro", Model = "Jet Fin", Size = Size.Small, PricePerDay = 50 },
            new Fins { ProductId = 109, Brand = "Scubapro", Model = "Jet Fin", Size = Size.Medium, PricePerDay = 50 },
            new Fins { ProductId = 110, Brand = "Scubapro", Model = "Jet Fin", Size = Size.Large, PricePerDay = 50 },
            new Fins { ProductId = 111, Brand = "Scubapro", Model = "Jet Fin", Size = Size.XtraLarge, PricePerDay = 50 },
            new Fins { ProductId = 112, Brand = "Scubapro", Model = "GO Travel", Size = Size.XtraSmall, PricePerDay = 50 },
            new Fins { ProductId = 113, Brand = "Scubapro", Model = "GO Travel", Size = Size.Small, PricePerDay = 50 },
            new Fins { ProductId = 114, Brand = "Scubapro", Model = "GO Travel", Size = Size.Medium, PricePerDay = 50 },
            new Fins { ProductId = 115, Brand = "Scubapro", Model = "GO Travel", Size = Size.Large, PricePerDay = 50 },
            new Fins { ProductId = 116, Brand = "Scubapro", Model = "GO Travel", Size = Size.XtraLarge, PricePerDay = 50 },
            new Fins { ProductId = 117, Brand = "Scubapro", Model = "Seawing Supernova", Size = Size.XtraSmall, PricePerDay = 60 },
            new Fins { ProductId = 118, Brand = "Scubapro", Model = "Seawing Supernova", Size = Size.Small, PricePerDay = 60 },
            new Fins { ProductId = 119, Brand = "Scubapro", Model = "Seawing Supernova", Size = Size.Medium, PricePerDay = 60 },
            new Fins { ProductId = 120, Brand = "Scubapro", Model = "Seawing Supernova", Size = Size.Large, PricePerDay = 60 },
            new Fins { ProductId = 121, Brand = "Scubapro", Model = "Seawing Supernova", Size = Size.XtraLarge, PricePerDay = 60 },
            new Fins { ProductId = 122, Brand = "Seac", Model = "Propulsion", Size = Size.XtraSmall, PricePerDay = 50 },
            new Fins { ProductId = 123, Brand = "Seac", Model = "Propulsion", Size = Size.Small, PricePerDay = 50 },
            new Fins { ProductId = 124, Brand = "Seac", Model = "Propulsion", Size = Size.Medium, PricePerDay = 50 },
            new Fins { ProductId = 125, Brand = "Seac", Model = "Propulsion", Size = Size.Large, PricePerDay = 50 },
            new Fins { ProductId = 126, Brand = "Seac", Model = "Propulsion", Size = Size.XtraLarge, PricePerDay = 50 },
            new Fins { ProductId = 127, Brand = "Seac", Model = "ALA", Size = Size.XtraSmall, PricePerDay = 50 },
            new Fins { ProductId = 128, Brand = "Seac", Model = "ALA", Size = Size.Small, PricePerDay = 50 },
            new Fins { ProductId = 129, Brand = "Seac", Model = "ALA", Size = Size.Medium, PricePerDay = 50 },
            new Fins { ProductId = 130, Brand = "Seac", Model = "ALA", Size = Size.Large, PricePerDay = 50 },
            new Fins { ProductId = 131, Brand = "Seac", Model = "ALA", Size = Size.XtraLarge, PricePerDay = 50 },
            new Fins { ProductId = 132, Brand = "Fourth Element", Model = "Tech", Size = Size.XtraSmall, PricePerDay = 75 },
            new Fins { ProductId = 133, Brand = "Fourth Element", Model = "Tech", Size = Size.Small, PricePerDay = 75 },
            new Fins { ProductId = 134, Brand = "Fourth Element", Model = "Tech", Size = Size.Medium, PricePerDay = 75 },
            new Fins { ProductId = 135, Brand = "Fourth Element", Model = "Tech", Size = Size.Large, PricePerDay = 75 },
            new Fins { ProductId = 136, Brand = "Fourth Element", Model = "Tech", Size = Size.XtraLarge, PricePerDay = 75 },
            new Fins { ProductId = 137, Brand = "Fourth Element", Model = "Rec Fin", Size = Size.XtraSmall, PricePerDay = 80 },
            new Fins { ProductId = 138, Brand = "Fourth Element", Model = "Rec Fin", Size = Size.Small, PricePerDay = 80 },
            new Fins { ProductId = 139, Brand = "Fourth Element", Model = "Rec Fin", Size = Size.Medium, PricePerDay = 80 },
            new Fins { ProductId = 140, Brand = "Fourth Element", Model = "Rec Fin", Size = Size.Large, PricePerDay = 80 },
            new Fins { ProductId = 141, Brand = "Fourth Element", Model = "Rec Fin", Size = Size.XtraLarge, PricePerDay = 80 }
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
