using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class CheckOut
    {
        public long SelectedShippingAddress { get; set; }
        public long SelectedBillingAddress { get; set; }
        public long SelectedShippingCompany { get; set; }
        public long SelectedShippingMethod { get; set; }
        public long SelectedBillingMethod { get; set; }
        [Key]
        public string Id  { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int Step { get; set; }
        public string CouponCode { get; set; }
        public bool IsSame { get; set; }
    }

}
