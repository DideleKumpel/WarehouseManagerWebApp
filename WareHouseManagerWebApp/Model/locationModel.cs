﻿
namespace WareHouseManagerWebApp.Model
{
    public class locationModel
    {
        public int Id { get; set; }
        public string Shelf { get; set; }
        public string Row { get; set; }
        public string Level { get; set; }
        public double MaxCapacity { get; set; }
        public string ItemBarcode { get; set; }
        public bool IsOnLocation { get; set; }


        public productModel Product { get; set; }
    }
}
