using EShop.BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EShop.WEB.Models
{
    public class DiscountVM
    {
        public long Id { get; set; }


        [Required(ErrorMessage="الاسم العربي مطلوب")]
        public string ArabicName { get; set; }

        [Required(ErrorMessage = "الاسم الانكليزي مطلوب")]
        public string EnglishName { get; set; }

        [Required(ErrorMessage = "تاريخ البداية مطلوب")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "تاريخ الانتهاء مطلوب")]
        public string EndDate { get; set; }

        [Required(ErrorMessage = "القيمة مطلوبة")]
        public double Value { get; set; }


        public bool IsPercentage { get; set; }
        public List<ProductSKUDTO> DiscountSkus { get; set; }
        
    }
}
