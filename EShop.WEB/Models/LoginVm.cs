using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class LoginVm
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), ErrorMessage = null)]
        [Display(Name = "Email", ResourceType = typeof(Resources.Views.EShopResource))]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), ErrorMessage = null)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.Views.EShopResource), ErrorMessage = null)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Views.EShopResource))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Resources.Views.EShopResource))]
        public bool RememberMe { get; set; }

    }
}