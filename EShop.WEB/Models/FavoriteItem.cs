using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EShop.BLL.DTO;
using EShop.Common;

namespace EShop.WEB.Models
{
    [Serializable]
    public class FavoriteItem
    {
        public long Id { get; set; }
        public long SkuId { get; set; }
        public String CurrencyName { get; set; }
        public string Name { get; set; }
        public ColorValuesDTO Color { get; set; }
        public SizeValueDTO Size { get; set; }
        public List<String> Images { get; set; }
        public bool IsAvailable { get; set; }
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