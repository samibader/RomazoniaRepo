using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace EShop.BLL.DTO
{
    public class SizeValueDTO
    {
        public bool Available { get; set; }
        public long  ValueId { get; set; }
        public long  OptionId { get; set; }
        public String Name { get; set; }
        public String EnglishName { get; set; }
    }
}