﻿
namespace WareHouseManagerWebApp.Model
{
    public class productModel
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Category { get; set; }
        public string Barcode { get; set;}
        public string Description { get; set; }

        public productModel Product { get; set; }
    }
}
