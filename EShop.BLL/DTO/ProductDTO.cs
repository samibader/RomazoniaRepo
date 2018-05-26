using EShop.Common;
using EShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class ProductDTO
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public long SKUId { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }
        public string CurrencyName { get; set; }
        public double DiscountPercentage { get; set; }
        public double TotalPrice { get; set; }
        public string TotalPriceDisplay
        {
            get
            {
                return Utils.GetValueCurrencyDisplay(CurrencyName, TotalPrice);
            }
        }
        public double OrginalPrice { get; set; }
        public string OrginalPriceDisplay
        {
            get
            {
                return Utils.GetValueCurrencyDisplay(CurrencyName, OrginalPrice);
            }
        }
        public bool IsNew { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDiscounted { get; set; }
        public int Quantity { get; set; }
        public String Text { set; get; }
        public String MetaDescriptions { set; get; }
        public DateTime DateAdded { get; set; }

        public List<String> Images { get; set; }
        public DesignerDTO Designer { get; set;}

        public int SelectedColor { get; set; }
        public List<ColorValuesDTO> Colors { get; set; }
        public int SelectedSize { get; set; }
        public List<SizeValueDTO> Sizes { get; set; }
        public List<ProductRateDTO> rates { get; set; }
        public int TotalRate { get; set; }
        public List<TagDTO> Tags { get; set; }
        public List<SizeAttributeViewDTO> SizeAttributes { get; set; }
    }

}
