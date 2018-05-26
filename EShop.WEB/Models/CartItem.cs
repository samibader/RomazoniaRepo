using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    [Serializable]
    public class CartItem
    {
        public long Id { get; set; }
        public long SkuId { get; set; }
        public long Quantity { get; set; }
        public string Name { get; set; }
        public List<String> Images { get; set; }
        public double TotalPrice { get; set; }
        public string TotalPriceDisplay { get; set; }

    }
}