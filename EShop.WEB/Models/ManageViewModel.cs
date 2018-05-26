using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace EShop.WEB.Models
{
    public class AccountSideBar
    {
        public bool UserHasPassword { get; set; }
    }
    public class EditInfoViewModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), ErrorMessage = null)]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), ErrorMessage = null)]
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Views.EShopResource))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), ErrorMessage = null)]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), ErrorMessage = null)]
        [Display(Name = "LastName", ResourceType = typeof(Resources.Views.EShopResource))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), ErrorMessage = null)]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), ErrorMessage = null)]
        [Display(Name = "Email", ResourceType = typeof(Resources.Views.EShopResource))]
        [System.Web.Mvc.Remote("CheckExistingEmailOtherThanSelf", "Account", ErrorMessageResourceName = "EmailExists", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), ErrorMessage = null)]
        public string Email { get; set; }


        [Display(Name = "Country", ResourceType = typeof(Resources.Views.EShopResource))]
        public int CountryId { get; set; }


        [Display(Name = "City", ResourceType = typeof(Resources.Views.EShopResource))]
        public string City { get; set; }
    }
    public class IndexViewModel
    {
        
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        
    }

    public class ManageLoginsViewModel
    {
        public bool HasInstagramAccount { get; set; }
        
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required(ErrorMessage = "{0} مطلوب !")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), ErrorMessage = null)]
        [StringLength(100, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), MinimumLength = 6, ErrorMessage = null)]
        [DataType(DataType.Password, ErrorMessage = null)]
        [Display(Name = "كلمة المرور الحالية")]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), ErrorMessage = null)]
        [StringLength(100, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), MinimumLength = 6, ErrorMessage = null)]
        [DataType(DataType.Password, ErrorMessage = null)]
        [Display(Name = "كلمة المرور الجديدة")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة المرور")]
        [Compare("NewPassword", ErrorMessageResourceName = "PassNotEqual", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), ErrorMessage = null)]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required(ErrorMessage = "{0} مطلوب !")]
        [Phone]
        [Display(Name = "رقم الجوال")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required(ErrorMessage = "{0} مطلوب !")]
        [Display(Name = "كود التفعيل")]
        public string Code { get; set; }

        [Required(ErrorMessage = "{0} مطلوب !")]
        [Phone]
        [Display(Name = "رقم الجوال")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}