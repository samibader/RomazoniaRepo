using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class ProductRateDTO
    {
        public long Id { get; set; }
        public long productId { get; set; }
        public string UserId { get; set; }

        public int Rate1 { get; set; }
        public int Rate2 { get; set; }
        public int Rate3 { get; set; }

        public int Rate { get; set; }
        public string Note { get; set; }
        public DateTime date { get; set; }

    }
}
