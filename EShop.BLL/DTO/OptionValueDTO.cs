using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class OptionValueDTO
    {
        public long  OptionValueId { get; set; }
        public string  Name { get; set; }
        public bool Selected { get; set; }
        public bool Available { get; set; }
    }
}
