using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class AddSizeAttributeDTO
    {
        public SizeAttributeDTO SizeAttribute { get; set; }
        public List<SizeAttributeValueDTO> SizeAttributeValues { get; set; }
        public List<OptionManagerDTO> SizesCategories { get; set; }
    }
}
