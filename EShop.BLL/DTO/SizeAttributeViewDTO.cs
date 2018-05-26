using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
   public class SizeAttributeViewDTO
    {
        public string SizeAttributeName { get; set; }
        public double SizeAttributeValueCm { get; set; }
        public double SizeAttributeValueInch { get; set; }
    }
}
