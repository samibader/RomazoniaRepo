using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class DetailedCartVM
    {
        public double TotalPrice { get; set; }
        public string TotalPriceDisplay { get; set; }
        public List<DetailedCartItemVM> DetailedCartItems { get; set; }
    }
}