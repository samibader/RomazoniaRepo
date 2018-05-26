using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class DesignerVM
    {
        public long Id { get; set; }
        [Required(ErrorMessage="الاسم العربي مطلوب")]
        public string ArabicName { get; set; }

        [Required(ErrorMessage = "الاسم الانكليزي مطلوب")]
        public string EnglishName { get; set; }
        public string ImageUrl { get; set; }

    }
}