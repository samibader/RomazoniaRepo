using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class ShippingCompany
    {
        public long Id { get; set; }
        public double ShippingCost { get; set; }
        public bool IsActive { get; set; }
    }
}
