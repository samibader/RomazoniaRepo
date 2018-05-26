using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.WEB.Controllers
{
    public class SizeAttributeValueVM
    {
        public long Id { get; set; }

        public long SizeValueId { get; set; }
        public long SizeAttributeId { get; set; }
        public int Value { get; set; }
        public DateTime DateModified { get; set; }
    }
}
