using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class OptionDTO
    {
        public string Name  { get; set; }
        public long OptionId { get; set; }
        public int OptionSelectedValue { get; set; }
        public List<OptionValueDTO> values { get; set; }
        
    }
}
