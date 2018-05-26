using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EShop.BLL.DTO;


namespace EShop.WEB.Models
{
    public class DetailedCartItemVM
    {
        public long Id { get; set; }
        public String Image { get; set; }
        public long SKUId { get; set; }
        public String  Name{ get; set; }
        public String  Description{ get; set; }
        public bool  Availabel{ get; set; }
        public ColorValuesDTO Color { get; set; }
        public SizeValueDTO Size { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public string PriceDisplay { get; set; }
        public string TotalPriceDisplay { get; set; }
        public int Quantity { get; set; }
    }
}