using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class Address
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string  Country { get; set; }
        public string UserId { get; set; }
        public bool IsDefault { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
