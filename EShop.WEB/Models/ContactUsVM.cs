using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class ContactUsVM
    {
        public long Id { get; set; }

        [Display(Name="الاسم الكامل")]
        [Required(ErrorMessage = "{0} مطلوب !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} مطلوب !")]
        [EmailAddress(ErrorMessage = "البريد الالكتروني غير صحيح !")]
        [Display(Name = "البريد الالكتروني")]
        public string Email { get; set; }

        [DisplayName("رقم الهاتف (اختياري)")]
        public string Phone { get; set; }

        [DisplayName("الموقع الالكتروني (اختياري)")]
        public string Website { get; set; }

        [Display(Name = "نص الرسالة")]
        [Required(ErrorMessage = "{0} مطلوب !")]
        public string ContactMessage { get; set; }
    }
}