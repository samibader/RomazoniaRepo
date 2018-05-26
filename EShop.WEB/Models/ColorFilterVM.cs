using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class ColorFilterVM
    {
        public String Name { get; set; }
        public String EnglishName { get; set; }
        public bool Checked { get; set; }
        public String Image { get; set; }
        public bool IsImage { get; set; }
    }
}