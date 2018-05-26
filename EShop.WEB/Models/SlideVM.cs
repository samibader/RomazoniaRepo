using EShop.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class SlideVM
    {

        public long Id { get; set; }

        [Required(ErrorMessage="الرجاء تحميل صورة")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "الرابط مطلوب")]
        public string Url { get; set; }
        public WebSites WebSite { get; set; }
    }
}