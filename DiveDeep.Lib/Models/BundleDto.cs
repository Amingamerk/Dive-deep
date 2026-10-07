using System;
using System.Collections.Generic;
using System.Text;

namespace DiveDeep.Lib.Models
{
    public class BundleDto
    {
        public int BundleId { get; set; }

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public List<ProductCategory> Categories { get; set; } = new List<ProductCategory>();

        public float DiscountPercent { get; set; }
    }
}
