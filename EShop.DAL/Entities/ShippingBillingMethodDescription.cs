using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class ShippingBillingMethodDescription
    {
        public long Id { get; set; }
        public long LanguageId { get; set; }
        public long ShippingBillingMethodId { get; set; }
        public string Name { get; set; }
        public string MetaDescription { get; set; }
        public virtual ShippingBillingMethod ShippingBillingMethod { get; set; }
        public virtual Language Language { get; set; }

    }
}
