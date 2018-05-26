using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EShop.WEB.Models
{
    public class DiscountFiltersVM
    {
        [Display(Name = "معرف التخيض")]
        public int Id { get; set; }
        [Display(Name = "الاسم العربي")]
        public string ArabicName { get; set; }
        [Display(Name = "الاسم الانكليزي")]
        public string EnglishName { get; set; }
        [Display(Name = "النوع")]
        public bool? IsPercentage { get; set; }
        [Display(Name = "فرز حسب")]
        public Common.Sorts SortBy { get; set; }

        [Display(Name = "حجم الصفحة")]
        public int PageSize { get; set; }

        public int PageNum { get; set; }
    }
}
