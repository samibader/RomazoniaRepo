using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class AddProductDTO
    {
        public long Id { get; set; }

        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string Comment { get; set; }
       
        public string EnglishDescription { get; set; }
       
        public string ArabicDescription { get; set; }
        public string ArabicMetaTagKeywords { get; set; }
        public string EnglishMetaTagKeywords { get; set; }
       
        public string ArabicMetaTagDescription { get; set; }
       
        public string EnglishMetaTagDescription { get; set; }
        public List<string> ArabicProductTags { get; set; }
        public List<string> EnglishProductTags { get; set; }

        public long CategoryId { get; set; }
        public long SubCategoryId { get; set; }
        public long BaseCategoryId { get; set; }
        public List<CategoryDTO> SubCategories { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public int MinimumQty { get; set; }
        public int Quantity { get; set; }

        public bool Status { get; set; }
        public long DesignerId { get; set; }
        public double Price { get; set; }
        public List<AttributeGroupDTO> AttributesGroups { get; set; }
        public List<AttributeDTO> Attributes { get; set; }

        public bool HasColor { get; set; }
        public bool HasSize { get; set; }
        public List<DesignerDTO> Designers { get; set; }

        public List<ColorValuesDTO> Colors { get; set; }
        public List<SizeValueDTO> Sizes { get; set; }
        public List<ProductOptionValueDTO> ProductOptionValues { get; set; }
        public List<string> ImagesUrls { get; set; }
        public DiscountDTO Discount { get; set; }
        public long? SizeCategoryId { get; set; }
        public List<SizeAttributeDTO> AllSizeAttributes { get; set; }
        public List<SizeAttributeValueDTO> SizeAttributes { get; set; }

    }
}
