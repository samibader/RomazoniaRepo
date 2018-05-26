using EShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class ProductIndexManagerDTO
    {
        public long Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string Comment { get; set; }

        public double Price { get; set; }
        public string CurrencyName { get; set; }

        public string PriceDisplay
        {
            get
            {
                return Utils.GetValueCurrencyDisplay(CurrencyName, Price);
            }
        }
        public bool Status { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}
