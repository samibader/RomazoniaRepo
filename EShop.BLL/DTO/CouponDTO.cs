using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Common;

namespace EShop.BLL.DTO
{
    public class CouponDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string CurrencyName { get; set; }
        public double Value { get; set; }

        public string ValueDisplay
        {
            get { return Utils.GetValueCurrencyDisplay(CurrencyName, Value); }
        }
        public bool IsPercentage { get; set; }
    }
}
