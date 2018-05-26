using EShop.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EShop.WEB.Models
{
    public class AttributeFilter
    {
        [Display(Name = "معرف الواصفة")]
        public long Id { get; set; }
        [Display(Name = "معرف مجموعة الواصفات")]
        public long GroupId { get; set; }

        [Display(Name = " مجموعة الواصفات")]
        public string GroupName { get; set; }
        [Display(Name = "الاسم العربي")]
        public string ArabicName { get; set; }
        [Display(Name = "الاسم الانكليزي")]
        public string EnglishName { get; set; }
        [Display(Name = "فرز حسب")]
        public Sorts SortBy { get; set; }

        [Display(Name = "حجم الصفحة")]
        public int PageSize { get; set; }

        public int PageNum { get; set; }
    }
}
