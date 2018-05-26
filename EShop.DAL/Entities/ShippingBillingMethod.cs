using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class ShippingBillingMethod
    {
        public long Id { get; set; }
        public bool IsShipping { get; set; }
        public bool IsActive { get; set; }
        public virtual List<ShippingBillingMethodDescription> ShippingBillingMethodDescriptions { get; set; }

    }
}
