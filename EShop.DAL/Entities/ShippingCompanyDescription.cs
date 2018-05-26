using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class ShippingCompanyDescription
    {
        public long Id { get; set; }
        public long LanguageId { get; set; }
        public long ShippingCompanyId { get; set; }
        public string Name { get; set; }
        public string MetaDescription { get; set; }
        public virtual ShippingCompany ShippingCompany { get; set; }
        public virtual Language Language { get; set; }
    }
}
