using EShop.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class OrderItemDTO
    {
        public long Id { get; set; }
        public string CurrencyName { get; set; }
        public long OrderId { get; set; }
        public string Name { get; set; }

        public string ColorArabic { get; set; }
        public string ColorEnglish { get; set; }
        public string SizeArabic { get; set; }
        public string SizeEnglish { get; set; }

        public string SKU { get; set; }
        public long SKUId { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public double UnitPrice { get; set; }
        public string Description { get; set; }
        public string UnitPriceDisplay
        {
            get
            {
                return Utils.GetValueCurrencyDisplay(CurrencyName, UnitPrice);
            }
        }

        [DisplayFormat(DataFormatString = "{0:C0}")]
         [DataType(DataType.Currency)]
        public double TotalPrice { get; set; }

        public string TotalPriceDisplay
        {
            get
            {
                return Utils.GetValueCurrencyDisplay(CurrencyName, TotalPrice);
            }
        }
    }
}
