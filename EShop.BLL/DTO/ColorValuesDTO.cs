using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.BLL.DTO
{
    public class ColorValuesDTO
    {

        public long ValueId { get; set; }
        public long OptionId { get; set; }
        public String ColorName { get; set; }
        public String ColorNameEnglish { get; set; }
        public String Image { get; set; }
        public String ProductImage { get; set; }
        public bool IsImages { get; set; }
    }
}