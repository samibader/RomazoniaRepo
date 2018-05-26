using EShop.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class ProductManagerFiltersVM
    {
      
        [Display(Name = "الاسم العربي")]
        public string ArabicName { get; set; }

        
        [Display(Name = "الاسم الانكليزي")]
        public string EnglishName { get; set; }

        [Display(Name = "السعر الابتدائي")]
        public double? StartPrice { get; set; }

        [Display(Name = "السعر النهائي")]
        public double? EndPrice { get; set; }

        [Display(Name = "الحالة")]
        public bool? Status { get; set; }

        [Display(Name = "حجم الصفحة")]
        public int PageSize { get; set; }

        [Display(Name = "فرز حسب")]

        public Sorts SortBy { get; set; }
        
        public int PageNum { get; set; }
        

    }
}