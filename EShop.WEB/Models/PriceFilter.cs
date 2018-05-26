using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class PriceFilter
    {
        public Tuple<int,int> Price { get; set; }
        public bool Checked { get; set; }
    }
}