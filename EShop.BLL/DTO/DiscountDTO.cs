using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
   public class DiscountDTO
    {
        public long Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public double Value { get; set; }
        public bool IsPercentage { get; set; }
        public List<ProductSKUDTO> DiscountSkus { get; set; }
       

    }
}
