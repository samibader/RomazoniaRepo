using PagedList;
using PagedList.Mvc;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EShop.Common;
using System.Web.Mvc;
namespace EShop.WEB.Models
{
    public class ManageCategoryVM
    {
        public IPagedList<CategoryDetailsVm> categories { get; set; }
        public CategoryManageFilter filters { get; set; }
        
    }
    public class CategoryDetailsVm
    {

        public long Id { get; set; }

        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "ترتيب الفرز")]
        public int SortOrder { get; set; }

        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "الحالة")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "الاسم الانكليزي")]
        public String EnglishName { set; get; }

        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "الاسم العربي")]
        public String ArabicName { set; get; }

        [AllowHtml]
        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "الوصف")]
        public String EnglishDescription { set; get; }

        [AllowHtml]
        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "الوصف")]
        public String ArabicDescription { set; get; }

        [AllowHtml]
        //[Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "Meta Description")]
        public String EnglishMetaDescription { set; get; }
        [AllowHtml]
        //[Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "وصف معلومات الفئة")]
        public String ArabicMetaDescription { set; get; }

        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "الاب")]
        public long ParentId { set; get; }
        public String ArabicPath { get; set; }
        public String EnglishPath { get; set; }
        public String ImageUrl { get; set; }
        public String BannerImage { get; set; }

        public DateTime? DateCreation { get; set; }
        public DateTime? DateModefied { get; set; }
    }

    public class CategoryManageFilter
    {
        [Display(Name = "معرف الفئة")]
        public long Id { get; set; }

        [Display(Name = "الاسم العربي")]
        public string ArabicName { get; set; }

        [Display(Name = "الاسم الانكليزي")]
        public string EnglishName { get; set; }

        [Display(Name = "الحالة")]
        public bool? Status { get; set; }

        [Display(Name = "تاريخ الاضافة")]
        public DateTime? DateCreation { get; set; }

        [Display(Name = "تاريخ التعديل")]
        public DateTime? DateModified { get; set; }

         [Display(Name = "فرز حسب")]
        public Common.Sorts SortBy { get; set; }

        [Display(Name = "حجم الصفحة")]
        public int PageSize { get; set; }

        public int PageNum { get; set; }


    }
}