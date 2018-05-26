using EShop.BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShop.WEB.Models
{
    public class AddProductVM
    {

        public long? Id { get; set; }

        [Required(ErrorMessage="اسم المنتج باللغة الانكليزية مطلوب")]
        public string  EnglishName { get; set; }

        [Required(ErrorMessage = "اسم المنتج باللغة العربية مطلوب")]

        public string ArabicName { get; set; }

        [Required(ErrorMessage = "وصف المنتج باللغة الانكليزية مطلوب")]
        [AllowHtml]
        public string EnglishDescription { get; set; }


         [AllowHtml]
         [Required(ErrorMessage = "وصف المنتج باللغة العربية مطلوب")]
        public string ArabicDescription { get; set; }


        public string ArabicMetaTagKeywords { get; set; }
        public string EnglishMetaTagKeywords { get; set; }
         [AllowHtml]
        public string ArabicMetaTagDescription{ get; set; }
         [AllowHtml]
        public string EnglishMetaTagDescription { get; set; }

         [AllowHtml]
         public string Comment { get; set; }
         public List<string> ArabicProductTags { get; set; }
         public List<string> EnglishProductTags { get; set; }

        [Required(ErrorMessage = "يرجى تحديد فئة المنتج")]
        public long CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public long? BaseCategoryId { get; set; }
        public List<CategoryPathDTO> BaseCategories { get; set; }
        public List<CategoryDTO> SubCategories { get; set; }

        [Display(Name="الكمية الصغرى")]
        [Range(0, int.MaxValue, ErrorMessage = "الرجاء إدخال رقم صالح")]
        public int MinimumQty { get; set; }

        [Display(Name = "الكمية")]
        public int Quantity { get; set; }

        [Display(Name = "الحالة")]
        public bool Status { get; set; }

        [Display(Name = "المصمم")]
        [Required(ErrorMessage="يجب تحديد اسم المصمم")]
        public long DesignerId { get; set; }

        [Required(ErrorMessage = "يجب تحديد سعر المنتج")]
        [Range(0, double.MaxValue, ErrorMessage = "يجب ادخال ارقام في الحقل المخصص للسعر")]
        public double Price { get; set; }
        public List<AttributeGroupDTO> AttributesGroups { get; set; }
        public List<AttributeDTO> Attributes { get; set; }

        [Display(Name = "له لون")]
        public bool HasColor { get; set; }

        [Display(Name = "له قياس")]
        public bool HasSize { get; set; }
        public List<DesignerDTO> Designers { get; set; }
                
        public List<ColorValuesDTO> Colors { get; set; }
        public List<SizeValueDTO> Sizes { get; set; }


        public List<ProductOptionValueDTO> ProductOptionValues { get; set; }
        public List<string> ImagesUrls { get; set; }

        [Display(Name = "التخفيض")]
        public DiscountDTO Discount { get; set; }

        [Display(Name = "مجموعة القياسات")]
        public long? SizeCategoryId { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public List<SizeAttributeDTO> AllSizeAttributes { get; set; }
        public List<SizeAttributeValueDTO> SizeAttributes { get; set; }
        


    }
}