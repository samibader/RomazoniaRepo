using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class ProductRateVM
    {
        public long Id { get; set; }
        public long productId { get; set; }
        public string UserId { get; set; }

        public int Rate1 { get; set; }
        public int Rate2 { get; set; }
        public int Rate3 { get; set; }

        public int Rate { get; set; }
        public string Note { get; set; }
    }
}