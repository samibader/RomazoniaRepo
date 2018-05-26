using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.WEB.Models
{
    public class ProductOptionValueVM
    {
        public long ColorValueId { get; set; }
        public long SizeValueId { get; set; }
        public double Price { get; set; }
        public List<string> ImagesUrls { get; set; }
        public int Quantity { get; set; }
        public string PricePrefix { get; set; }


    }
}
