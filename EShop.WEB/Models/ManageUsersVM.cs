using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class ManageUsersVM
    {
        public IPagedList<UserDetailsVm> Users { get; set; }
        public UserManageFilter filters { get; set; }
    }
    public class UserDetailsVm
    {

        public string Id { get; set; }

        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "الرجاء إدخال بريد الكتروني صحيح")]
        [Display(Name = "البريد الالكتروني")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "الاسم ")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "الكنية")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "الاسم الكامل")]
        public string FullName { get; set; }

       

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة المرور")]
        [Compare("Password", ErrorMessage = "كلمة المرور وتأكيدها غير متطابقان")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "حالة الحساب")]
        public bool AccountStatus { set; get; }

        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "قفل الحساب")]
        public bool AccountLock { set; get; }

        //[Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "تاريخ التسحيل في الموقع")]
        public DateTime? DateCreation { get; set; }
       
    }

    public class UserManageFilter
    {

        [Display(Name = "اسم الزبون")]
        public string FullName { get; set; }

        [Display(Name = "البريد الالكتروني")]
        public string Email { get; set; }

        [Display(Name = "فرز حسب")]
        public Common.Sorts SortBy { get; set; }

        [Display(Name = "حجم الصفحة")]
        public int PageSize { get; set; }

        public int PageNum { get; set; }


    }
}