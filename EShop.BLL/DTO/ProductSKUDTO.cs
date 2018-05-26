using EShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class ProductSKUDTO
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public long SKUId { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }
        public string CurrencyName { get; set; }

        public double OrginalPrice { get; set; }
        public string OrginalPriceDisplay
        {
            get
            {
                return Utils.GetValueCurrencyDisplay(CurrencyName, OrginalPrice);
            }
        }
        public double DiscountPrice { get; set; }
        public string DiscountPriceDisplay
        {
            get
            {
                return Utils.GetValueCurrencyDisplay(CurrencyName, DiscountPrice);
            }
        }

        public List<String> Images { get; set; }
        public int SelectedColor { get; set; }
        public List<ColorValuesDTO> Colors { get; set; }
        public int SelectedSize { get; set; }
        public List<SizeValueDTO> Sizes { get; set; }


    }
}
