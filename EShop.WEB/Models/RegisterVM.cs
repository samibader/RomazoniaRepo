using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class RegisterVM
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
        [System.Web.Mvc.Remote("CheckExistingEmail", "Account", ErrorMessageResourceName = "EmailExists", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), ErrorMessage = null)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), ErrorMessage = null)]
        [StringLength(100, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), MinimumLength = 6, ErrorMessage = null)]
        [DataType(DataType.Password, ErrorMessage = null)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Views.EShopResource))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Views.EShopResource))]
        [Compare("Password", ErrorMessageResourceName = "PassNotEqual", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), ErrorMessage = null)]
        public string ConfirmPassword { get; set; }


    }

}