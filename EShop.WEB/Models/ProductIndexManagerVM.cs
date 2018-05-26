using EShop.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShop.WEB.Models
{
    public class ProductIndexManagerVM
    {
        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "معرف المنتج")]
        public long Id { get; set; }

        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "الاسم العربي")]
        public string ArabicName { get; set; }

        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "الاسم الانكليزي")]
        public string EnglishName { get; set; }

        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "التعليق")]
        [AllowHtml]
        public string Comment { get; set; }

        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "السعر")]
        public double Price { get; set; }
        public string CurrencyName { get; set; }

        public string PriceDisplay
        {
            get
            {
                return Utils.GetValueCurrencyDisplay(CurrencyName, Price);
            }
        }

        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "الحالة")]
        public bool Status { get; set; }


        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "{0} مطلوب", AllowEmptyStrings = false)]
        [Display(Name = "الكمية")]
        public int Quantity { get; set; }
    }
}