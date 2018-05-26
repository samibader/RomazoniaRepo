using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class OrderItem
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }

        public string ColorArabic { get; set; }
        public string ColorEnglish { get; set; }
        public string SizeArabic { get; set; }
        public string SizeEnglish { get; set; }

        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }


    }
}
