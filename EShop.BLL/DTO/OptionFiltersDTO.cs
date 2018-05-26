using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class OptionFiltersDTO
    {
        [Display(Name = "معرف الخيار")]
        public long Id { get; set; }
        [Display(Name = "الاسم العربي")]
        public string ArabicName { get; set; }
        [Display(Name = "الاسم الانكليزي")]
        public string EnglishName { get; set; }
        [Display(Name = "فرز حسب")]
        public Common.Sorts SortBy { get; set; }

        [Display(Name = "حجم الصفحة")]
        public int PageSize { get; set; }

        public int PageNum { get; set; }

    }
}
