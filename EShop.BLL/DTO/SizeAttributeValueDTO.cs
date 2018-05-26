using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class SizeAttributeValueDTO
    {
        public long Id { get; set; }

        [Display(Name = "SizeValueId")]
        public long SizeValueId { get; set; }


        public string SizeValueName { get; set; }
        public string SizeCategoryName { get; set; }

        [Display(Name = "SizeAttributeId")]
        public long SizeAttributeId { get; set; }

        [Display(Name = "oijoioijoij")]
        public double? Value { get; set; }
        public DateTime DateModified { get; set; }

       
    }
}
