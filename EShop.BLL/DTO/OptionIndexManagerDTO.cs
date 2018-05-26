using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class OptionIndexManagerDTO
    {
        public List<OptionManagerDTO> Options { get; set; }
        public OptionFiltersDTO filters { get; set; }
    }
}
