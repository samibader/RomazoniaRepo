using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Common;

namespace EShop.BLL.DTO
{
    public class ShippingCompanyDTO
    {
        public long Id { get; set; }
        public double ShippingCost { get; set; }
        public string ShippingCurrency { get; set; }

        public string ShippingCostDisplay
        {
            get
            {
                return Utils.GetValueCurrencyDisplay(ShippingCurrency, ShippingCost);
                
            }
        }

        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string MetaDescription { get; set; }
    }
}
