using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class AddressVM
    {
        
        public long Id { get; set; }
        
        [Required]
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Views.EShopResource))]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "LastName", ResourceType = typeof(Resources.Views.EShopResource))]
        public string LastName { get; set; }
        
        [Required]
        [Display(Name = "العنوان")]
        public string Address1 { get; set; }
       
        [Required]
        [Display(Name = "Phone", ResourceType = typeof(Resources.Views.EShopResource))]
        public string Phone { get; set; }
        
        [Required]
        [Display(Name = "City", ResourceType = typeof(Resources.Views.EShopResource))]
        public string City { get; set; }
        
        [Required]
        [Display(Name = "الرمز البريدي")]
        public string PostCode { get; set; }
       
        [Required(ErrorMessage = "يجب تحديد الدولة ")]
        [Display(Name = "الدولة")]
        public string Country { get; set; }
       
        [Required]
        [Display(Name = "العنوان الافتراضي")]
        public bool IsDefault { get; set; }
    }
}