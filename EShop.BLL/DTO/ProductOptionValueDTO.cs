using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EShop.BLL.DTO
{
    public class ProductOptionValueDTO
    {
        [Display(Name = "لون الخيار")]
        [Required(ErrorMessage="يجب اختيار لون للخيار المضاف")]
        public long? ColorValueId { get; set; }

        [Display(Name = "قياس الخيار")]
        [Required(ErrorMessage = "يجب اختيار قياس للخيار المضاف")]
        public long? SizeValueId { get; set; }

        [Required(ErrorMessage = "حقل السعر في الخيار مطلوب")]
        public double Price { get; set; }
        public List<string> ImagesUrls { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int Quantity { get; set; }
        public string PricePrefix { get; set; }
        public List<string> ArabicTags { get; set; }
        public List<string> EnglishTags { get; set; }


    }
}
