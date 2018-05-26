using EShop.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class AddSizeAttributeVM
    {
        public SizeAttributeDTO SizeAttribute { get; set; }
        public List<OptionManagerDTO> SizesCategories { get; set; }
        public List<SizeAttributeValueDTO> SizeAttributeValues { get; set; }
    }
}