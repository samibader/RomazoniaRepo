using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using EShop.Common;
using EShop.DAL.Entities;
using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Services
{
    public class ProductManagerService : IProductManagerService
    {
        IUnitOfWork unitOfWork { get; set; }
        private readonly ICategoryService _categoryService;
        public ProductManagerService(IUnitOfWork uow, ICategoryService _categoryService)
        {
            unitOfWork = uow;
            this._categoryService = _categoryService;
        }

        public OperationDetails DeleteProduct(long productId)
        {
            Product product = unitOfWork.ProductRepository.GetByID(productId);

            List<ProductDescription> productDescriptions = product.ProductDescriptions.ToList();
            foreach (var desc in productDescriptions)
            {
                unitOfWork.ProductDescriptionRepository.Delete(desc);
                unitOfWork.Save();
            }

            List<Image> productImages = product.Images.ToList();
            foreach (var img in productImages)
            {
                unitOfWork.ImageRepository.Delete(img);
                unitOfWork.Save();
            }

            List<ProductRate> productRate = product.ProductRates.ToList();
            foreach (var rate in productRate)
            {
                unitOfWork.ProductRateRepository.Delete(rate);
                unitOfWork.Save();
            }

            List<ProductSKUOptionValue> productSKUoptionValues =
                unitOfWork.ProductSKUOptionValueRepository.Get(c => c.ProductId == productId).ToList();
            foreach (var proskuopval in productSKUoptionValues)
            {
                unitOfWork.ProductSKUOptionValueRepository.Delete(proskuopval);
                unitOfWork.Save();
            }

            List<ProductSizeAttribute> productSizeAttributes = product.SizeAttributes.ToList();
            foreach (var sizeattr in productSizeAttributes)
            {
                unitOfWork.ProductSizeAttributeRepository.Delete(sizeattr);
                unitOfWork.Save();
            }

            List<ProductAttributeValue> productAtteibuteValues = product.ProductAttributeValues.ToList();
            foreach (var attrVal in productAtteibuteValues)
            {
                unitOfWork.ProductAttributeValueRepository.Delete(attrVal);
                unitOfWork.Save();
            }

            List<Option> productOptions = product.Options.ToList();
            foreach (var option in productOptions)
            {
                List<OptionDescription> optionDescriptions = option.OptionDescriptions.ToList();
                foreach (var desc in optionDescriptions)
                {
                    unitOfWork.OptionDescriptionRepository.Delete(desc);
                    unitOfWork.Save();
                }

                List<OptionValue> optionValues = option.OptionValues.ToList();
                foreach (var value in optionValues)
                {
                    List<OptionValueDescription> valueDescriptions = value.OptionValueDescriptions.ToList();
                    foreach (var desc in valueDescriptions)
                    {
                        unitOfWork.OptionValueDescriptionRepository.Delete(desc);
                        unitOfWork.Save();
                    }

                    List<Image> valueImages = value.Images.ToList();
                    foreach (var img in valueImages)
                    {
                        unitOfWork.ImageRepository.Delete(img);
                        unitOfWork.Save();
                    }

                    unitOfWork.OptionValueRepository.Delete(value);
                    unitOfWork.Save();
                }
                unitOfWork.OptionRepository.Delete(option);
                unitOfWork.Save();
            }

            List<ProductSKU> SKUs = product.ProductSKUs.ToList();
            foreach (var sku in SKUs)
            {
                List<ProductDiscount> skuDiscounts = sku.ProductSkuDiscounts.ToList();
                foreach (var discount in skuDiscounts)
                {
                    unitOfWork.DiscountRepository.Delete(discount);
                    unitOfWork.Save();
                }

                

                List<Tag> skuTags = sku.Tags.ToList();
                foreach (var tag in skuTags.ToList())
                {
                    skuTags.Remove(tag);
                   // unitOfWork.TagRepository.Delete(tag);
                    unitOfWork.Save();
                }

                List<ShoppingCart> skuCarts = sku.ShoppingCarts.ToList();
                foreach (var item in skuCarts)
                {
                    unitOfWork.ShoppingCartRepository.Delete(item);
                    unitOfWork.Save();
                }
                unitOfWork.ProductSKURepository.Delete(sku);
                unitOfWork.Save();
            }
            unitOfWork.ProductRepository.Delete(product);
            unitOfWork.Save();
            return new OperationDetails(true, "تمت عملية الحذف بنجاح", "");

        }

        public List<SizeCategoryDTO> GetSizeCategories(Langs l)
        {
            List<OptionHelper> categories = unitOfWork.OptionHelperRepository.Get(c => true).ToList();
            List<SizeCategoryDTO> sizeCats = new List<SizeCategoryDTO>();
            foreach (var cat in categories)
            {
                if (cat.Id != 3)
                {
                    SizeCategoryDTO dto = new SizeCategoryDTO();

                    dto.Name = (long)l == 1 ? cat.ArabicName : cat.EnglishName;
                    dto.Id = cat.Id;
                    sizeCats.Add(dto);
                }
            }
            return sizeCats;
        }
        public List<TagDTO> GetTagByTerm(string query)
        {
            List<Tag> tags = unitOfWork.TagRepository.Get(c => c.Text.ToLower().Contains(query.ToLower())).ToList();
            List<TagDTO> tagsDtos = new List<TagDTO>();
            foreach (var tag in tags)
            {
                TagDTO tagDto = new TagDTO();
                tagDto.Name = tag.Text;
                tagsDtos.Add(tagDto);

            }
            return tagsDtos;
        }
        public List<ColorValuesDTO> GetAllColors()
        {
            List<ColorValuesDTO> colors = new List<ColorValuesDTO>();
            List<ColorHelper> colorHelpers = unitOfWork.ColorHelperRepository.Get(c => true).ToList();
            foreach (var col in colorHelpers)
            {
                ColorValuesDTO color = new ColorValuesDTO();
                color.ColorName = col.Name;
                color.ColorName = col.ArabicName;
                color.ValueId = col.Id;
                colors.Add(color);
            }
            return colors;
        }
        public List<SizeValueDTO> GetAllSizes()
        {
            List<SizeValueDTO> sizes = new List<SizeValueDTO>();
            List<SizeHelper> sizeHelpers = unitOfWork.SizeHelperRepository.Get(c => true).ToList();
            foreach (var si in sizeHelpers)
            {
                SizeValueDTO size = new SizeValueDTO();
                size.Name = si.ArabicName;
                size.ValueId = si.Id;
                sizes.Add(size);
            }
            return sizes;
        }

        public List<SizeValueDTO> GetSizesOfCat(long? sizeCatId)
        {
            List<SizeValueDTO> sizes = new List<SizeValueDTO>();
            List<SizeHelper> sizeHelpers = new List<SizeHelper>();
            if (sizeCatId == null)
                sizeHelpers = unitOfWork.SizeHelperRepository.Get(c => true).ToList();
            else
                sizeHelpers = unitOfWork.SizeHelperRepository.Get(c => c.OptionId == sizeCatId.Value).ToList();

            foreach (var si in sizeHelpers)
            {
                SizeValueDTO size = new SizeValueDTO();
                size.Name = si.ArabicName;
                size.ValueId = si.Id;
                sizes.Add(size);
            }
            return sizes;
        }
        public List<DesignerDTO> GetAllDesigners(Langs l)
        {
            List<DesignerDTO> designersDTOs = new List<DesignerDTO>();
            List<Designer> designers = unitOfWork.DesignerRepository.Get(c => true).ToList();
            foreach (var des in designers)
            {
                DesignerDTO designer = new DesignerDTO();
                designer.DesignerName = des.Name;
                designer.DesignerId = des.Id;
                designersDTOs.Add(designer);
            }
            return designersDTOs;
        }

        public OperationDetails AddProduct(AddProductDTO productDto)
        {
            Product product = new Product();
            product.CategoryId = productDto.CategoryId;
            product.DesignerId = productDto.DesignerId;
            product.Minimum = productDto.MinimumQty;
            product.Price = productDto.Price;
            product.Status = productDto.Status;
            product.DateAdded = DateTime.Now;
            product.DateModefied = DateTime.Now;
            product.Comment = productDto.Comment;


            long arabicLang = (long)Langs.Arabic;
            long englishLang = (long)Langs.English;



            product = unitOfWork.ProductRepository.Insert(product);
            unitOfWork.Save();
            long productId = product.Id;

            foreach (var attribute in productDto.Attributes)
            {
                ProductAttributeValue arabicDesc = new ProductAttributeValue();
                ProductAttributeValue englishDesc = new ProductAttributeValue();

                arabicDesc.LanguageId = arabicLang;
                arabicDesc.ProductId = product.Id;
                arabicDesc.Text = attribute.ArabicName;
                arabicDesc.AttributeId = attribute.Id;

                englishDesc.LanguageId = englishLang;
                englishDesc.ProductId = product.Id;
                englishDesc.Text = attribute.EnglishName;
                englishDesc.AttributeId = attribute.Id;

                unitOfWork.ProductAttributeValueRepository.Insert(arabicDesc);
                unitOfWork.ProductAttributeValueRepository.Insert(englishDesc);
                unitOfWork.Save();
            }

            foreach (var image in productDto.ImagesUrls)
            {
                Image img = new Image();
                img.ProductId = productId;
                img.ImageUrl = image;
                unitOfWork.ImageRepository.Insert(img);
                unitOfWork.Save();
            }

            ProductDescription arabicDescription = new ProductDescription();
            ProductDescription englishDescription = new ProductDescription();

            arabicDescription.LanguageId = (long)Langs.Arabic;
            arabicDescription.MetaDescriptions = productDto.ArabicMetaTagDescription;
            arabicDescription.MetaKeywords = productDto.ArabicMetaTagKeywords;
            arabicDescription.Name = productDto.ArabicName;
            arabicDescription.ProductId = productId;
            arabicDescription.Text = productDto.ArabicDescription;
            arabicDescription.DateAdded = DateTime.Now;
            arabicDescription.DateModefied = DateTime.Now;

            englishDescription.LanguageId = (long)Langs.English;
            englishDescription.MetaDescriptions = productDto.EnglishMetaTagDescription;
            englishDescription.MetaKeywords = productDto.EnglishMetaTagKeywords;
            englishDescription.Name = productDto.EnglishName;
            englishDescription.ProductId = productId;
            englishDescription.Text = productDto.EnglishDescription;
            englishDescription.DateAdded = DateTime.Now;
            englishDescription.DateModefied = DateTime.Now;

            unitOfWork.ProductDescriptionRepository.Insert(arabicDescription);
            unitOfWork.ProductDescriptionRepository.Insert(englishDescription);
            unitOfWork.Save();

            if (productDto.HasColor)
            {
                long? colorValueId = productDto.ProductOptionValues.FirstOrDefault().ColorValueId;
                long optionId = unitOfWork.ColorHelperRepository.GetByID(colorValueId).OptionId;
                List<long?> colors = productDto.ProductOptionValues.Select(c => c.ColorValueId).Distinct().ToList();
                addOption(optionId, productId, true, colors);
            }
            if (productDto.HasSize)
            {
                long? sizeValueId = productDto.ProductOptionValues.FirstOrDefault().SizeValueId;
                long optionId = unitOfWork.SizeHelperRepository.GetByID(sizeValueId).OptionId;
                List<long?> sizes = productDto.ProductOptionValues.Select(c => c.SizeValueId).Distinct().ToList();
                addOption(optionId, productId, false, sizes);

                foreach (var sizeAttribute in productDto.SizeAttributes)
                {
                    ProductSizeAttribute sizeAttr = new ProductSizeAttribute();
                    sizeAttr.ProductId = product.Id;
                    sizeAttr.DateAdded = DateTime.Now;
                    sizeAttr.DateModified = DateTime.Now;
                    sizeAttr.SizeAttributeId = sizeAttribute.SizeAttributeId;
                    sizeAttr.SizeCategoryId = unitOfWork.SizeHelperRepository.Get(c => c.Id == sizeAttribute.Id).Select(g => g.OptionId).FirstOrDefault();
                    sizeAttr.ValueId = sizeAttribute.SizeValueId;
                    sizeAttr.Value =sizeAttribute.Value??0;
                    sizeAttr = unitOfWork.ProductSizeAttributeRepository.Insert(sizeAttr);
                    unitOfWork.Save();
                }
            }
            long max;
            if (!productDto.HasColor && !productDto.HasSize)
            {
                 max = 1;
                var list = unitOfWork.ProductSKURepository.Get(c => true).ToList();

                if (list.Any())
                    max = list.Max(c => c.SKUId) + 1;
                string SKU = "P" + productId;
                ProductSKU sku = new ProductSKU();
                sku.Price = 0;
                sku.ProductId = productId;
                sku.Quentity = productDto.Quantity;
                sku.SKUId = max;
                sku.SKU = SKU;
                unitOfWork.ProductSKURepository.Insert(sku);
                unitOfWork.Save();
            }


            
            foreach (var skuoptionvalue in productDto.ProductOptionValues)
            {
                max = 1;
                var list = unitOfWork.ProductSKURepository.Get(c => true).ToList();
                if (list.Any())
                    max = list.Max(c => c.SKUId) + 1;
                string SKU = "P" + productId;
                ProductSKU sku = new ProductSKU();
                sku.Price = (int)skuoptionvalue.Price;
                if (skuoptionvalue.PricePrefix == "-")
                    sku.Price *= -1;
                sku.ProductId = productId;
                sku.Quentity = skuoptionvalue.Quantity;
                sku.SKUId = max;
                ProductSKUOptionValue productSkuOptionValue1 = new ProductSKUOptionValue();
                ProductSKUOptionValue productSkuOptionValue2 = new ProductSKUOptionValue();
                if (productDto.HasColor)
                {
                    SKU += "C" + skuoptionvalue.ColorValueId;
                    productSkuOptionValue1.ProductId = productId;
                    productSkuOptionValue1.ValueId = skuoptionvalue.ColorValueId.Value;
                    productSkuOptionValue1.SKUId = sku.SKUId;
                    productSkuOptionValue1.OptionId = unitOfWork.ColorHelperRepository.
                        GetByID(skuoptionvalue.ColorValueId).OptionId;

                }
                if (productDto.HasSize)
                {
                    SKU += "S" + skuoptionvalue.SizeValueId;
                    productSkuOptionValue2.ProductId = productId;
                    productSkuOptionValue2.ValueId = skuoptionvalue.SizeValueId.Value;
                    productSkuOptionValue2.SKUId = sku.SKUId;
                    productSkuOptionValue2.OptionId = unitOfWork.SizeHelperRepository.
                        GetByID(skuoptionvalue.SizeValueId).OptionId;

                }
                sku.SKU = SKU;
                sku = unitOfWork.ProductSKURepository.Insert(sku);
                unitOfWork.Save();
                if (productDto.HasColor)
                {
                    unitOfWork.ProductSKUOptionValueRepository.Insert(productSkuOptionValue1);
                    unitOfWork.Save();
                }
                if (productDto.HasSize)
                {
                    unitOfWork.ProductSKUOptionValueRepository.Insert(productSkuOptionValue2);
                    unitOfWork.Save();
                }

                if (skuoptionvalue.ImagesUrls == null)
                    skuoptionvalue.ImagesUrls = new List<string>();

                foreach (var image in skuoptionvalue.ImagesUrls)
                {
                    Image img = new Image();
                    img.OptionValueProductId = productId;
                    img.OptionValueOptionId = unitOfWork.ColorHelperRepository.
                        GetByID(skuoptionvalue.ColorValueId).OptionId;
                    img.OptionValueValueId = skuoptionvalue.ColorValueId;
                    img.AlternateText = SKU;
                    img.ImageUrl = image;
                    unitOfWork.ImageRepository.Insert(img);
                    unitOfWork.Save();
                }
                sku.Tags = new List<Tag>();

                if (skuoptionvalue.ArabicTags == null)
                    skuoptionvalue.ArabicTags = new List<string>();

                foreach (var tag in skuoptionvalue.ArabicTags)
                {
                    Tag tg = unitOfWork.TagRepository.Get(c => c.LanguageId == (long)Langs.Arabic && c.Text == tag).FirstOrDefault();
                    if (tg == null)
                    {
                        tg = new Tag();
                        tg.LanguageId = Utils.getLanguage(Langs.Arabic);
                        tg.DateAdded = DateTime.Now;
                        tg.DateModefied = DateTime.Now;
                        tg.Text = tag;
                    }
                    sku.Tags.Add(tg);
                    unitOfWork.Save();

                }

                if (skuoptionvalue.EnglishTags == null)
                    skuoptionvalue.EnglishTags = new List<string>();

                foreach (var tag in skuoptionvalue.EnglishTags)
                {
                    Tag tg = unitOfWork.TagRepository.Get(c => c.LanguageId == (long)Langs.English && c.Text == tag).FirstOrDefault();
                    if (tg == null)
                    {
                        tg = new Tag();
                        tg.LanguageId = Utils.getLanguage(Langs.English);
                        tg.DateAdded = DateTime.Now;
                        tg.DateModefied = DateTime.Now;
                        tg.Text = tag;
                    }
                    sku.Tags.Add(tg);
                    unitOfWork.Save();
                }
            }
            if (productDto.ArabicProductTags == null)
                productDto.ArabicProductTags = new List<string>();
            foreach (var tag in productDto.ArabicProductTags)
            {
                Tag tg = unitOfWork.TagRepository.Get(c => c.LanguageId == (long)Langs.Arabic && c.Text == tag).FirstOrDefault();
                if (tg == null)
                {
                    tg = new Tag();
                    tg.LanguageId = Utils.getLanguage(Langs.Arabic);
                    tg.DateAdded = DateTime.Now;
                    tg.DateModefied = DateTime.Now;
                    tg.Text = tag;
                }

                foreach (var sku in unitOfWork.ProductRepository.GetByID(productId).ProductSKUs)
                {
                    if (sku.Tags == null)
                        sku.Tags = new List<Tag>();
                    sku.Tags.Add(tg);
                    unitOfWork.Save();
                }
            }
            if (productDto.EnglishProductTags == null)
                productDto.EnglishProductTags = new List<string>();
            foreach (var tag in productDto.EnglishProductTags)
            {
                Tag tg = unitOfWork.TagRepository.Get(c => c.LanguageId == (long)Langs.English && c.Text == tag).FirstOrDefault();
                if (tg == null)
                {
                    tg = new Tag();
                    tg.LanguageId = Utils.getLanguage(Langs.English);
                    tg.DateAdded = DateTime.Now;
                    tg.DateModefied = DateTime.Now;
                    tg.Text = tag;
                }
                foreach (var sku in unitOfWork.ProductRepository.GetByID(productId).ProductSKUs)
                {
                    if (sku.Tags == null)
                        sku.Tags = new List<Tag>();
                    sku.Tags.Add(tg);
                    unitOfWork.Save();
                }
            }

            // unitOfWork.Save();
            return new OperationDetails(true, "تمت عملية الإضافة بنجاح", "");
        }

        private void addOption(long optionId, long productId, bool isColor, List<long?> lst)
        {
            OptionHelper helper = unitOfWork.OptionHelperRepository.GetByID(optionId);
            Option option = new Option();
            option.IsColor = isColor;
            option.ProductId = productId;
            option.OptionId = optionId;
            option.OptionDescriptions = new List<OptionDescription>
            {
                new OptionDescription {
                LanguageId = (long)Langs.Arabic,
            Name = helper.ArabicName,
            ProductId = productId
            //OptionId = optionId
                },
                 new OptionDescription {
                LanguageId = (long)Langs.English,
            Name = helper.EnglishName,
            ProductId = productId
            //OptionId = optionId
                }
            };


            //OptionDescription arabicDesc = new OptionDescription();
            //arabicDesc.LanguageId = (long)Langs.Arabic;
            //arabicDesc.Name = helper.ArabicName;
            //arabicDesc.OptionId = optionId;
            //OptionDescription englishDesc = new OptionDescription();
            //englishDesc.LanguageId = (long)Langs.English;
            //englishDesc.Name = helper.EnglishName;
            //englishDesc.OptionId = optionId;

            unitOfWork.OptionRepository.Insert(option);
            unitOfWork.Save();
            //unitOfWork.OptionDescriptionRepository.Insert(arabicDesc);
            //unitOfWork.OptionDescriptionRepository.Insert(englishDesc);
            unitOfWork.Save();

            if (isColor)
            {
                List<ColorHelper> colors = unitOfWork.ColorHelperRepository.Get(c => c.OptionId == optionId && lst.Contains(c.Id)).ToList();
                foreach (var color in colors)
                {
                    OptionValue optionValue = new OptionValue();
                    optionValue.OptionId = optionId;
                    optionValue.ProductId = productId;
                    optionValue.Value = color.Value;
                    optionValue.ValueId = color.Id;
                    optionValue.ImageUrl = color.ImageUrl;
                    OptionValueDescription arabic = new OptionValueDescription();
                    arabic.LanguageId = (long)Langs.Arabic;
                    arabic.Name = color.ArabicName;
                    arabic.OptionId = optionId;
                    arabic.ValueId = color.Id;
                    arabic.ProductId = productId;
                    OptionValueDescription english = new OptionValueDescription();
                    english.LanguageId = (long)Langs.English;
                    english.Name = color.EngishName;
                    english.OptionId = optionId;
                    english.ValueId = color.Id;
                    english.ProductId = productId;
                    unitOfWork.OptionValueRepository.Insert(optionValue);
                    unitOfWork.Save();
                    unitOfWork.OptionValueDescriptionRepository.Insert(arabic);
                    unitOfWork.OptionValueDescriptionRepository.Insert(english);
                    unitOfWork.Save();
                }
            }
            else
            {
                List<SizeHelper> sizes = unitOfWork.SizeHelperRepository.Get(c => c.OptionId == optionId && lst.Contains(c.Id)).ToList();
                foreach (var size in sizes)
                {
                    OptionValue optionValue = new OptionValue();
                    optionValue.OptionId = optionId;
                    optionValue.ProductId = productId;
                    optionValue.Value = size.Value;
                    optionValue.ValueId = size.Id;
                    OptionValueDescription arabic = new OptionValueDescription();
                    arabic.LanguageId = (long)Langs.Arabic;
                    arabic.Name = size.ArabicName;
                    arabic.OptionId = optionId;
                    arabic.ValueId = size.Id;
                    arabic.ProductId = productId;
                    OptionValueDescription english = new OptionValueDescription();
                    english.LanguageId = (long)Langs.English;
                    english.Name = size.EngishName;
                    english.OptionId = optionId;
                    english.ValueId = size.Id;
                    english.ProductId = productId;
                    unitOfWork.OptionValueRepository.Insert(optionValue);
                    unitOfWork.Save();
                    unitOfWork.OptionValueDescriptionRepository.Insert(arabic);
                    unitOfWork.OptionValueDescriptionRepository.Insert(english);
                    unitOfWork.Save();
                }

            }

        }

        public AddProductDTO GetEditProduct(long productId, Langs l)
        {
            AddProductDTO productDTO = new AddProductDTO();
            Product product = unitOfWork.ProductRepository.GetByID(productId);
            productDTO.CategoryId = product.CategoryId;
            productDTO.DesignerId = product.DesignerId;
            productDTO.MinimumQty = product.Minimum;

            productDTO.Price = product.Price;
            productDTO.Status = product.Status;
            productDTO.Id = product.Id;
            productDTO.Comment = product.Comment;
            //get Produt Attributes
            List<ProductAttributeValue> productAttributes = unitOfWork.ProductAttributeValueRepository.Get
                (c => c.ProductId == productId).ToList();
            List<AttributeDTO> attributesDtos = new List<AttributeDTO>();
            foreach (var attr in productAttributes)
            {
                if (attr.LanguageId == (long)Langs.Arabic)
                {
                    AttributeDTO dto = new AttributeDTO();
                    dto.ArabicName = attr.Text;
                    dto.EnglishName = productAttributes.Where(c => c.LanguageId == (long)Langs.English && c.ProductId == attr.ProductId && c.AttributeId == attr.AttributeId).FirstOrDefault().Text;
                    dto.Id = attr.AttributeId;

                    dto.Name = unitOfWork.AttributeDescriptionRepository.Get(c => c.LanguageId == (long)l && c.AttributeId == attr.AttributeId).FirstOrDefault().Text;
                    attributesDtos.Add(dto);
                }
            }
            ///////////////////

            productDTO.Attributes = attributesDtos;
           
            //get necessary details for selecting Base Cat,Sub cat and the actual category for the product
            //long subCatId = product.Category.ParentId.Value;
            //productDTO.SubCategoryId = subCatId;
            //Category subCat = unitOfWork.CategoryRepository.GetByID(subCatId);
            //long baseCatId = subCat.ParentId.Value;
            //productDTO.BaseCategoryId = baseCatId;
            //List<Category> subCategories = unitOfWork.CategoryRepository.Get(c => c.ParentId == baseCatId).ToList();

            //List<CategoryDTO> subCategoriesDtos = new List<CategoryDTO>();
            //foreach (var sub in subCategories)
            //{
            //    CategoryDTO dto = new CategoryDTO();
            //    dto.Id = sub.Id;
            //    dto.Name = sub.CategoryDescriptions.Where(c => c.LanguageId == (long)l).FirstOrDefault().Name;
            //    subCategoriesDtos.Add(dto);
            //}

            //List<Category> productExpectedCategories = unitOfWork.CategoryRepository.Get(c => c.ParentId == subCatId).ToList();
            //List<CategoryDTO> ProductExpectedCategoriesDtos = new List<CategoryDTO>();
            //foreach (var sub in productExpectedCategories)
            //{
            //    CategoryDTO dto = new CategoryDTO();
            //    dto.Id = sub.Id;
            //    dto.Name = sub.CategoryDescriptions.Where(c => c.LanguageId == (long)l).FirstOrDefault().Name;
            //    ProductExpectedCategoriesDtos.Add(dto);
            //}

            ///////////////////////////////////////////////

            //productDTO.SubCategories = subCategoriesDtos;
            //productDTO.Categories = ProductExpectedCategoriesDtos;

            List<Image> images = unitOfWork.ImageRepository.Get(c => c.ProductId == productId).ToList();
            productDTO.ImagesUrls = new List<string>();
            if (images != null)
            {
                foreach (var image in images)
                {
                    productDTO.ImagesUrls.Add(image.ImageUrl);
                }
            }

            ProductDescription arabicProductDesc = unitOfWork.ProductDescriptionRepository.Get(c => c.ProductId == productId &&
                c.LanguageId == (long)Langs.Arabic).FirstOrDefault();
            ProductDescription englishProductDesc = unitOfWork.ProductDescriptionRepository.Get(c => c.ProductId == productId &&
                c.LanguageId == (long)Langs.English).FirstOrDefault();
            if (arabicProductDesc != null)
            {
                productDTO.ArabicDescription = arabicProductDesc.Text;
                productDTO.ArabicMetaTagDescription = arabicProductDesc.MetaDescriptions;
                productDTO.ArabicMetaTagKeywords = arabicProductDesc.MetaKeywords;
                productDTO.ArabicName = arabicProductDesc.Name;
            }

            if (englishProductDesc != null)
            {
                productDTO.EnglishDescription = englishProductDesc.Text;
                productDTO.EnglishMetaTagDescription = englishProductDesc.MetaDescriptions;
                productDTO.EnglishMetaTagKeywords = englishProductDesc.MetaKeywords;
                productDTO.EnglishName = englishProductDesc.Name;
            }

            productDTO.HasSize = false; productDTO.HasColor = false;
            if (unitOfWork.OptionRepository.Get(c => c.IsColor == true && c.ProductId == productId).Count() != 0)
                productDTO.HasColor = true;
            if (unitOfWork.OptionRepository.Get(c => c.IsColor == false && c.ProductId == productId).Count() != 0)
            {
                productDTO.HasSize = true;
                productDTO.SizeCategoryId = unitOfWork.OptionRepository.Get(c => c.IsColor == false && c.ProductId == productId).FirstOrDefault().OptionId;
            }

            if (productDTO.HasSize)
            {

                productDTO.SizeAttributes = new List<SizeAttributeValueDTO>();
                List<ProductSizeAttribute> sizeAttrs = unitOfWork.ProductSizeAttributeRepository.
                    Get(c => c.ProductId == productId).ToList();
                if (sizeAttrs != null)
                {
                    foreach (var sizeAttribute in sizeAttrs)
                    {
                        SizeAttributeValueDTO sizeAttr = new SizeAttributeValueDTO();
                        sizeAttr.SizeAttributeId = sizeAttribute.SizeAttributeId;
                        sizeAttr.SizeValueId = sizeAttribute.ValueId;
                        sizeAttr.Value = sizeAttribute.Value;

                        productDTO.SizeAttributes.Add(sizeAttr);
                    }
                }
            }

            if (!productDTO.HasColor && !productDTO.HasSize)
            {

                productDTO.Quantity = product.ProductSKUs.Sum(c => c.Quentity);
            }

            List<ProductSKU> productSKUs = unitOfWork.ProductSKURepository.Get(c => c.ProductId == productId).ToList();
            productDTO.ProductOptionValues = new List<ProductOptionValueDTO>();
            if (productSKUs != null)
            {
                foreach (var sku in productSKUs)
                {
                    ProductOptionValueDTO productOptionValueDTO = new ProductOptionValueDTO();
                    productOptionValueDTO.Price = Math.Abs(sku.Price);
                    productOptionValueDTO.PricePrefix = "+";
                    if (sku.Price < 0)
                        productOptionValueDTO.PricePrefix = "-";
                    productOptionValueDTO.Quantity = sku.Quentity;

                    if (productDTO.HasColor)
                    {
                        productOptionValueDTO.ColorValueId = unitOfWork.ProductSKUOptionValueRepository
                            .Get(c => c.ProductId == productId && c.SKUId == sku.SKUId && c.Option.IsColor == true)
                            .FirstOrDefault().ValueId;
                        long optionid = unitOfWork.ColorHelperRepository.
                                GetByID(productOptionValueDTO.ColorValueId).OptionId;
                        List<Image> skuImages = unitOfWork.ImageRepository
                            .Get(c => c.OptionValueProductId == productId &&
                                    c.OptionValueValueId == productOptionValueDTO.ColorValueId &&
                                    c.OptionValueOptionId == optionid&&
                                    c.AlternateText==sku.SKU).ToList();
                        productOptionValueDTO.ImagesUrls = new List<string>();
                        if (skuImages != null)
                        {
                            foreach (var image in skuImages)
                            {
                                productOptionValueDTO.ImagesUrls.Add(image.ImageUrl);
                            }
                        }
                    }
                    if (productDTO.HasSize)
                    {
                        productOptionValueDTO.SizeValueId = unitOfWork.ProductSKUOptionValueRepository
                            .Get(c => c.ProductId == productId && c.SKUId == sku.SKUId && c.Option.IsColor == false)
                            .FirstOrDefault().ValueId;
                    }

                    productOptionValueDTO.ArabicTags = new List<string>();
                    productOptionValueDTO.EnglishTags = new List<string>();
                    List<Tag> skuArabicTags = sku.Tags.Where(c => c.LanguageId == (long)Langs.Arabic).ToList();
                    List<Tag> skuEnglishTags = sku.Tags.Where(c => c.LanguageId == (long)Langs.English).ToList();
                    productDTO.ArabicProductTags = new List<string>();
                    productDTO.EnglishProductTags = new List<string>();

                    if (skuArabicTags != null)
                        foreach (var tag in skuArabicTags)
                        {
                            if (IsProductTag(tag.Text, productDTO.Id))
                            {

                                productDTO.ArabicProductTags.Add(tag.Text);
                            }
                            else
                            {
                                productOptionValueDTO.ArabicTags.Add(tag.Text);
                            }
                        }

                    if (skuEnglishTags != null)
                        foreach (var tag in skuEnglishTags)
                        {
                            if (IsProductTag(tag.Text, productDTO.Id))
                            {
                                productDTO.EnglishProductTags.Add(tag.Text);

                            }
                            else
                            {
                                productOptionValueDTO.EnglishTags.Add(tag.Text);
                            }
                        }
                    productDTO.ProductOptionValues.Add(productOptionValueDTO);
                }


            }




            return productDTO;
        }

        public List<ProductIndexManagerDTO> FilterProducts(string arabicName, string englishName, Double? minPrice, Double? maxPrice, Boolean? status, long id, Sorts s, Langs l, Currency cu,WebSites w)
        {
            List<long> ids = unitOfWork.ProductRepository.Get(c => true).AsEnumerable().Where(c=>_categoryService.getWebsite(c.CategoryId)==w).Select(c => c.Id).ToList();
            List<long> help;
            if (!String.IsNullOrEmpty(arabicName))
            {
                help = unitOfWork.ProductDescriptionRepository.
                    Get(c => c.LanguageId == (long)Langs.Arabic && c.Text.ToLower().Contains(arabicName.ToLower())).
                    Select(c => c.ProductId).Distinct().ToList();
                ids = ids.Intersect(help).ToList();
            }
            if (!String.IsNullOrEmpty(englishName))
            {
                help = unitOfWork.ProductDescriptionRepository.
                    Get(c => c.LanguageId == (long)Langs.English && c.Text.ToLower().Contains(englishName.ToLower())).
                    Select(c => c.ProductId).Distinct().ToList();
                ids = ids.Intersect(help).ToList();
            }
            if (minPrice == null || minPrice == 0)
                minPrice = -10000;
            if (maxPrice == null || maxPrice == 0)
                maxPrice = 100000;
            help = unitOfWork.ProductRepository.Get(c => c.Price <= maxPrice && c.Price >= minPrice).Select(c => c.Id).ToList();
            ids = ids.Intersect(help).ToList();
            if (status != null)
            {
                help = unitOfWork.ProductRepository.Get(c => c.Status == status).Select(c => c.Id).ToList();
                ids = ids.Intersect(help).ToList();
            }
            if (id != null && id != 0)
            {
                if (ids.Contains(id))
                {
                    ids.Clear();
                    ids.Add(id);
                }
                else
                    ids.Clear();
            }
            List<ProductIndexManagerDTO> dtos = new List<ProductIndexManagerDTO>();
            foreach (var productId in ids)
            {
                dtos.Add(GetProductByID(productId, l, cu));
            }
            sortProducts(ref dtos, s);
            return dtos;
        }

        private bool IsProductTag(string tag, long productId)
        {
            List<ProductSKU> skus = unitOfWork.ProductRepository.Get(c => c.Id == productId).FirstOrDefault().ProductSKUs.ToList();
            foreach (var sku in skus)
            {

                if (sku.Tags.Where(c => c.Text == tag).Count() == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void sortProducts(ref List<ProductIndexManagerDTO> products, Sorts s)
        {
            switch (s)
            {
                case Sorts.ArabicNameDown: products.Sort((a, b) => -1 * a.ArabicName.CompareTo(b.ArabicName));   //products.OrderBy(op => op.Name);
                    break;
                case Sorts.ArabicNameUp: products.Sort((a, b) => a.ArabicName.CompareTo(b.ArabicName));
                    break;
                case Sorts.EnglishNameDown: products.Sort((a, b) => -1 * a.EnglishName.CompareTo(b.EnglishName)); //products.OrderBy(op => op.Price);
                    break;
                case Sorts.EnglishNameUp: products.Sort((a, b) => a.EnglishName.CompareTo(b.EnglishName));
                    break;
                case Sorts.IdDown: products.Sort((a, b) => -1 * a.Id.CompareTo(b.Id));
                    break;
                case Sorts.IdUp: products.Sort((a, b) => a.Id.CompareTo(b.Id));
                    break;
                case Sorts.PriceDownUp: products.Sort((a, b) => a.Price.CompareTo(b.Price));
                    break;
                case Sorts.PriceUpDown: products.Sort((a, b) => -1 * a.Price.CompareTo(b.Price));
                    break;
                case Sorts.QuantityDown: products.Sort((a, b) => -1 * a.Quantity.CompareTo(b.Quantity));
                    break;
                case Sorts.QuantityUp: products.Sort((a, b) => a.Quantity.CompareTo(b.Quantity));
                    break;
            }

        }

        public ProductIndexManagerDTO GetProductByID(long id, Langs l, Currency cu)
        {
            Product product = unitOfWork.ProductRepository.GetByID(id);
            ProductIndexManagerDTO dto = new ProductIndexManagerDTO();
            dto.Id = id;
            if (product.Images != null && product.Images.FirstOrDefault() != null)
                dto.ImageUrl = product.Images.FirstOrDefault().ImageUrl;
            dto.Price = Utils.getCurrency(cu, l, product.Price).Item1;
            dto.Quantity = product.ProductSKUs.Sum(c => c.Quentity);
            dto.Status = product.Status;
            dto.CurrencyName = Utils.getCurrencyName(cu, l);
            dto.ArabicName = product.ProductDescriptions.Where(c => c.LanguageId == (long)Langs.Arabic).FirstOrDefault().Name;
            dto.EnglishName = product.ProductDescriptions.Where(c => c.LanguageId == (long)Langs.English).FirstOrDefault().Name;
            return dto;
        }


        public OperationDetails DeleteOption(long optionId)
        {
            OptionHelper optionHelper = unitOfWork.OptionHelperRepository.GetByID(optionId);
            int productCount = unitOfWork.OptionRepository.Get(c => c.OptionId == optionId).Count();
            if (productCount != 0)
            {
                return new OperationDetails(false, "هناك منتجات مرتبطة بهذه الواصفة", "");
            }
            if (optionId == 3)
            {
                return new OperationDetails(false, "لا يمكنك حذف هذه الواصفة", "");
            }
            else
            {
               var sizeAttributes =  unitOfWork.ProductSizeAttributeRepository.Get(c => c.SizeCategoryId == optionId).ToList();

                if(sizeAttributes.Any())
                    return new OperationDetails(false, "لا يمكنك حذف هذا الخيار،هناك واصفات قياسات مرتبطة بهذه الواصفة", "");

                int valueCount = unitOfWork.SizeHelperRepository.Get(c => c.OptionId == optionId).ToList().Count();
                if (valueCount != 0)
                {
                    return new OperationDetails(false, "لا يمكنك حذف هذا الخيار،هناك قيم مرتبطة بهذه الواصفة", "");
                }

                unitOfWork.OptionHelperRepository.Delete(optionHelper);
                unitOfWork.Save();
                return new OperationDetails(true, "تم الحذف بنجاح", "");
            }
        }

        public OperationDetails DeleteOptionValue(long optionId, long optionValueId)
        {
            if (optionId == 3)
            {
                ColorHelper value = unitOfWork.ColorHelperRepository.GetByID(optionValueId);
                if (value == null)
                    return new OperationDetails(false, "الخيار غير موجود", "");

                if (unitOfWork.OptionValueRepository.Get(c => c.ValueId == optionValueId && c.OptionId == optionId).Any())
                    return new OperationDetails(false, "لا يمكن حذف القيمة لوجود منتجات مرتبطة", "");

                

                unitOfWork.ColorHelperRepository.Delete(value);
                unitOfWork.Save();
                return new OperationDetails(true, "تمت عملية الحذف", "");
            }
            else
            {
                SizeHelper value = unitOfWork.SizeHelperRepository.GetByID(optionValueId);
                if (value == null)
                    return new OperationDetails(false, "القيمة غير موجودة", "");

                if (unitOfWork.OptionValueRepository.Get(c => c.ValueId == optionValueId && c.OptionId == optionId).Any())
                    return new OperationDetails(false, "لا يمكن حذف القيمة لوجود منتجات مرتبطة", "");



                unitOfWork.SizeHelperRepository.Delete(value);
                unitOfWork.Save();
                return new OperationDetails(true, "تمت عملية الحذف", "");
            }
        }



        public OperationDetails EditOptionValue(OptionValueManagerDTO optionValueDto)
        {

            if (optionValueDto.OptionId == 3)
            {
                ColorHelper optionValue = unitOfWork.ColorHelperRepository.Get(c => c.Id == optionValueDto.Id).FirstOrDefault();
                if (optionValue == null)
                    return new OperationDetails(false, "حدث خطأ في تعديل الخيار", "");
                optionValue.ArabicName = optionValueDto.ArabicName;
                optionValue.EngishName = optionValueDto.EnglishName;
                optionValue.Value = optionValueDto.Value;
                optionValue.ImageUrl = optionValueDto.ImageURL;
                unitOfWork.ColorHelperRepository.Update(optionValue);
                unitOfWork.Save();
                return new OperationDetails(true, "تم تعديل الخيار  بشكل صحيح", "");
            }
            else
            {
                SizeHelper optionValue = unitOfWork.SizeHelperRepository.Get(c => c.Id == optionValueDto.Id).FirstOrDefault();
                if (optionValue == null)
                    return new OperationDetails(false, "حدث خطأ في تعديل الخيار", "");
                optionValue.ArabicName = optionValueDto.ArabicName;
                optionValue.EngishName = optionValueDto.EnglishName;
                optionValue.Value = optionValueDto.Value;
                unitOfWork.SizeHelperRepository.Update(optionValue);
                unitOfWork.Save();
                return new OperationDetails(true, "تم تعديل الخيار  بشكل صحيح", "");
            }


        }

        public OperationDetails EditOption(OptionManagerDTO optionDto)
        {
            OptionHelper option = unitOfWork.OptionHelperRepository.Get(c => c.Id == optionDto.Id).FirstOrDefault();
            if (option == null)
                return new OperationDetails(false, "حدث خطأ في تعديل الخيار", "");
            option.ArabicName = optionDto.ArabicName;
            option.EnglishName = optionDto.EnglishName;
            unitOfWork.OptionHelperRepository.Update(option);
            unitOfWork.Save();
            return new OperationDetails(true, "تم تعديل الخيار  بشكل صحيح", "");

        }

        public List<OptionManagerDTO> GetAllOptions()
        {
            List<OptionManagerDTO> optionManagerDTOs = new List<OptionManagerDTO>();
            var optionIds = unitOfWork.OptionHelperRepository.Get(c => true).Select(c => c.Id);
            foreach (var optionId in optionIds)
            {
                optionManagerDTOs.Add(GetOptionById(optionId));
            }
            return optionManagerDTOs;
        }
        public List<OptionManagerDTO> GetAllSizeOptions()
        {
            List<OptionManagerDTO> optionManagerDTOs = new List<OptionManagerDTO>();
            var optionIds = unitOfWork.OptionHelperRepository.Get(c => c.Id != 3).Select(c => c.Id);
            foreach (var optionId in optionIds)
            {
                optionManagerDTOs.Add(GetOptionById(optionId));
            }
            return optionManagerDTOs;
        }
        public OptionManagerDTO GetOptionById(long id)
        {
            var option = unitOfWork.OptionHelperRepository.Get(c => c.Id == id).FirstOrDefault();
            return new OptionManagerDTO()
            {
                Id = option.Id,
                ArabicName = option.ArabicName,
                EnglishName = option.EnglishName,
                OptionValues = GetOptionValuesByOptionId(id)
            };
        }

        public OperationDetails AddOption(string arabicName, string englishName)
        {
            OptionHelper option = new OptionHelper();
            option.ArabicName = arabicName;
            option.EnglishName = englishName;
            option = unitOfWork.OptionHelperRepository.Insert(option);
            unitOfWork.Save();
            return new OperationDetails(true, "تمت الإضافة بنجاح", option.Id.ToString());
        }
        public OperationDetails AddOptionValue(OptionValueManagerDTO optionValue)
        {
            if (optionValue.OptionId == 3)
            {
                ColorHelper helper = new ColorHelper();
                helper.OptionId = optionValue.OptionId;
                helper.ArabicName = optionValue.ArabicName;
                helper.EngishName = optionValue.EnglishName;
                helper.Value = optionValue.Value;
                helper.ImageUrl = optionValue.ImageURL;
                unitOfWork.ColorHelperRepository.Insert(helper);
                unitOfWork.Save();
                return new OperationDetails(true, "تمت إضافة القيمة بنجاح", helper.Id.ToString());
            }
            else
            {
                SizeHelper helper = new SizeHelper();
                OptionManagerDTO op = GetOptionById(optionValue.OptionId);
                helper.OptionId = op.Id;
                helper.ArabicSizeCategoryName = op.ArabicName;
                helper.EnglishSizeCategoryName = op.EnglishName;
                helper.ArabicName = optionValue.ArabicName;
                helper.EngishName = optionValue.EnglishName;
                helper.Value = optionValue.Value;
                unitOfWork.SizeHelperRepository.Insert(helper);
                unitOfWork.Save();
                return new OperationDetails(true, "تمت إضافة القيمة بنجاح", helper.Id.ToString());
            }

        }

        public List<OptionValueManagerDTO> GetOptionValuesByOptionId(long optionId)
        {
            List<OptionValueManagerDTO> optionValueManagerDTOs = new List<OptionValueManagerDTO>();
            if (optionId == 3)
            {
                List<long> ids = unitOfWork.ColorHelperRepository.Get(c => c.OptionId == optionId).Select(c => c.Id).ToList();
                foreach (var id in ids)
                {
                    optionValueManagerDTOs.Add(GetColorValueById(id));
                }
            }
            else
            {
                List<long> ids = unitOfWork.SizeHelperRepository.Get(c => c.OptionId == optionId).Select(c => c.Id).ToList();
                foreach (var id in ids)
                {
                    optionValueManagerDTOs.Add(GetSizeValueById(id));
                }
            }
            return optionValueManagerDTOs;
        }
        public OptionValueManagerDTO GetColorValueById(long id)
        {
            ColorHelper helper = unitOfWork.ColorHelperRepository.GetByID(id);
            OptionValueManagerDTO optionValueManagerDTO = new OptionValueManagerDTO();
            optionValueManagerDTO.ArabicName = helper.ArabicName;
            optionValueManagerDTO.EnglishName = helper.EngishName;
            optionValueManagerDTO.Id = helper.Id;
            optionValueManagerDTO.OptionId = helper.OptionId;
            optionValueManagerDTO.Value = helper.Value;
            optionValueManagerDTO.ImageURL = helper.ImageUrl;

            return optionValueManagerDTO;
        }
        public OptionValueManagerDTO GetSizeValueById(long id)
        {
            SizeHelper helper = unitOfWork.SizeHelperRepository.GetByID(id);
            OptionValueManagerDTO optionValueManagerDTO = new OptionValueManagerDTO();
            optionValueManagerDTO.ArabicName = helper.ArabicName;
            optionValueManagerDTO.EnglishName = helper.EngishName;
            optionValueManagerDTO.Id = helper.Id;
            optionValueManagerDTO.OptionId = helper.OptionId;
            optionValueManagerDTO.Value = helper.Value;
            return optionValueManagerDTO;
        }

        public List<OptionManagerDTO> Filter(String arabicName, String englishName, long optionId, Sorts s)
        {
            List<long> optionIds = unitOfWork.OptionHelperRepository.Get(c => true).Select(c => c.Id).ToList();

            if (!String.IsNullOrEmpty(arabicName))
            {
                List<long> ids = unitOfWork.OptionHelperRepository.Get(c => c.ArabicName.Contains(arabicName)).Select(c => c.Id).ToList();
                optionIds = optionIds.Intersect(ids).ToList();
            }
            if (!String.IsNullOrEmpty(englishName))
            {
                List<long> ids = unitOfWork.OptionHelperRepository.Get(c => c.EnglishName.ToLower().Contains(englishName.ToLower())).Select(c => c.Id).ToList();
                optionIds = optionIds.Intersect(ids).ToList();
            }
            if (optionId != null && optionId != 0)
            {
                List<long> id = new List<long>();
                id.Add(optionId);
                optionIds = optionIds.Intersect(id).ToList();
            }

            List<OptionManagerDTO> options = new List<OptionManagerDTO>();
            foreach (var id in optionIds)
            {
                if (id == 3)
                {
                    OptionManagerDTO op = GetOptionById(id);
                    op.ArabicName = "اللون";
                    op.EnglishName = "Color";
                    options.Add(op);
                }
                else
                {
                    options.Add(GetOptionById(id));
                }
            }

            sortOptions(ref options, s);
            return options;
        }
        private void sortOptions(ref List<OptionManagerDTO> options, Sorts s)
        {
            switch (s)
            {
                case Sorts.ArabicNameDown: options.Sort((a, b) => -1 * a.ArabicName.CompareTo(b.ArabicName));
                    break;
                case Sorts.ArabicNameUp: options.Sort((a, b) => a.ArabicName.CompareTo(b.ArabicName));
                    break;
                case Sorts.EnglishNameDown: options.Sort((a, b) => -1 * a.EnglishName.CompareTo(b.EnglishName));
                    break;
                case Sorts.EnglishNameUp: options.Sort((a, b) => a.EnglishName.CompareTo(b.EnglishName));
                    break;
                case Sorts.IdDown: options.Sort((a, b) => -1 * a.Id.CompareTo(b.Id));
                    break;
                case Sorts.IdUp: options.Sort((a, b) => a.Id.CompareTo(b.Id));
                    break;
            }

        }

        public List<CategoryDTO> GetSubCategories(long parentId, Langs l)
        {
            List<Category> categories = unitOfWork.CategoryRepository.Get(c => c.ParentId == parentId).ToList();
            List<CategoryDTO> categoriesDTOs = new List<CategoryDTO>();
            long lang = Utils.getLanguage(l);
            foreach (var category in categories)
            {
                CategoryDTO dto = new CategoryDTO();
                dto.Id = category.Id;
                dto.Name = category.CategoryDescriptions.Where(c => c.LanguageId == lang).Select(g => g.Name).FirstOrDefault();
                categoriesDTOs.Add(dto);
            }
            return categoriesDTOs;
        }

        public OperationDetails EditProduct(long productId, AddProductDTO productDto)
        {
            Product product = unitOfWork.ProductRepository.GetByID(productId);
            product.CategoryId = productDto.CategoryId;
            product.DesignerId = productDto.DesignerId;
            product.Minimum = productDto.MinimumQty;
            product.Price = productDto.Price;
            product.Status = productDto.Status;
            product.DateModefied = DateTime.Now;
            product.Comment = productDto.Comment;
            unitOfWork.ProductRepository.Update(product);
            unitOfWork.Save();

            long arabicLang = (long)Langs.Arabic;
            long englishLang = (long)Langs.English;

            foreach (var attribute in productDto.Attributes)
            {
                ProductAttributeValue arabicDesc = unitOfWork.ProductAttributeValueRepository
                      .Get(c => c.LanguageId == arabicLang && c.ProductId == productId && c.AttributeId == attribute.Id).FirstOrDefault();
                ProductAttributeValue englishDesc = unitOfWork.ProductAttributeValueRepository
                    .Get(c => c.LanguageId == englishLang && c.ProductId == productId && c.AttributeId == attribute.Id).FirstOrDefault();
                if (arabicDesc == null)
                {
                    arabicDesc = new ProductAttributeValue();
                    englishDesc = new ProductAttributeValue();

                    arabicDesc.LanguageId = arabicLang;
                    arabicDesc.ProductId = product.Id;
                    arabicDesc.Text = attribute.ArabicName;
                    arabicDesc.AttributeId = attribute.Id;

                    englishDesc.LanguageId = englishLang;
                    englishDesc.ProductId = product.Id;
                    englishDesc.Text = attribute.EnglishName;
                    englishDesc.AttributeId = attribute.Id;

                    unitOfWork.ProductAttributeValueRepository.Insert(arabicDesc);
                    unitOfWork.ProductAttributeValueRepository.Insert(englishDesc);
                    unitOfWork.Save();
                }
                else
                {

                    arabicDesc.Text = attribute.ArabicName;

                    englishDesc.Text = attribute.EnglishName;

                    unitOfWork.ProductAttributeValueRepository.Update(arabicDesc);
                    unitOfWork.ProductAttributeValueRepository.Update(englishDesc);
                    unitOfWork.Save();
                }
            }

          var productAttributes =   unitOfWork.ProductAttributeValueRepository.Get(c => c.ProductId == productDto.Id).ToList();

          var dtoAttributesIds = productDto.Attributes.Select(c => c.Id).ToList();

          if (productAttributes == null)
              productAttributes = new List<ProductAttributeValue>();

          if (dtoAttributesIds == null)
              dtoAttributesIds = new List<long>();

          foreach (var attribute in productAttributes)
          {
              if(!dtoAttributesIds.Contains(attribute.Id))
              {
                  unitOfWork.ProductAttributeValueRepository.Delete(attribute);
                  unitOfWork.Save();
              }
          }

            List<string> productImages = unitOfWork.ImageRepository.Get(c => c.ProductId == productId).Select(c => c.ImageUrl).ToList();
            if (productImages == null)
                productImages = new List<string>();
            if (productDto.ImagesUrls == null)
                productDto.ImagesUrls = new List<string>();

            foreach (var image in productDto.ImagesUrls)
            {
                if (!productImages.Contains(image))
                {
                    Image img = new Image();
                    img.ProductId = productId;
                    img.ImageUrl = image;
                    unitOfWork.ImageRepository.Insert(img);
                    unitOfWork.Save();
                }
            }
            foreach (var image in productImages)
            {
                if (!productDto.ImagesUrls.Contains(image))
                {
                    Image img = unitOfWork.ImageRepository.Get(c => c.ImageUrl == image && c.ProductId == productId).FirstOrDefault();
                    unitOfWork.ImageRepository.Delete(img);
                    unitOfWork.Save();
                }
            }

            ProductDescription arabicDescription = unitOfWork.ProductDescriptionRepository
                .Get(c => c.ProductId == productId && c.LanguageId == (long)Langs.Arabic).FirstOrDefault();
            ProductDescription englishDescription = unitOfWork.ProductDescriptionRepository
                .Get(c => c.ProductId == productId && c.LanguageId == (long)Langs.English).FirstOrDefault();


            arabicDescription.MetaDescriptions = productDto.ArabicMetaTagDescription;
            arabicDescription.MetaKeywords = productDto.ArabicMetaTagKeywords;
            arabicDescription.Name = productDto.ArabicName;
            arabicDescription.Text = productDto.ArabicDescription;
            arabicDescription.DateModefied = DateTime.Now;

            englishDescription.MetaDescriptions = productDto.EnglishMetaTagDescription;
            englishDescription.MetaKeywords = productDto.EnglishMetaTagKeywords;
            englishDescription.Name = productDto.EnglishName;
            englishDescription.Text = productDto.EnglishDescription;
            englishDescription.DateModefied = DateTime.Now;

            unitOfWork.ProductDescriptionRepository.Update(arabicDescription);
            unitOfWork.ProductDescriptionRepository.Update(englishDescription);
            unitOfWork.Save();
            List<string> newSKU = new List<string>();
            foreach (var skuoptionvalue in productDto.ProductOptionValues)
            {
                string SKU = "P" + productId;
                if (productDto.HasColor) SKU += "C" + skuoptionvalue.ColorValueId;
                if (productDto.HasSize) SKU += "S" + skuoptionvalue.SizeValueId;
                newSKU.Add(SKU);
            }
            List<string> skuStrings = unitOfWork.ProductSKURepository.Get(c => c.ProductId == productId).Select(c => c.SKU).ToList();
            if(productDto.HasColor || productDto.HasSize)
            foreach (var sku in skuStrings)
            {
                if (!newSKU.Contains(sku))
                {
                    deleteSKU(sku);
                }
            }
            if (!unitOfWork.ProductSKURepository.Get(c => c.ProductId == productId).Any())
            {
                long max;
                if (!productDto.HasColor && !productDto.HasSize)
                {
                    max = 1;
                    var list = unitOfWork.ProductSKURepository.Get(c => true).ToList();

                    if (list.Any())
                        max = list.Max(c => c.SKUId) + 1;
                    string SKU = "P" + productId;
                    ProductSKU sku = new ProductSKU();
                    sku.Price = 0;
                    sku.ProductId = productId;
                    sku.Quentity = productDto.Quantity;
                    sku.SKUId = max;
                    sku.SKU = SKU;
                    unitOfWork.ProductSKURepository.Insert(sku);
                    unitOfWork.Save();
                }
            }

            if (productDto.HasColor)
            {
                long? colorValueId = productDto.ProductOptionValues.FirstOrDefault().ColorValueId;
                long optionId = unitOfWork.ColorHelperRepository.GetByID(colorValueId).OptionId;
                List<long?> colors = productDto.ProductOptionValues.Select(c => c.ColorValueId).Distinct().ToList();
               editOption(optionId, productId, true, colors);
            }
            if (productDto.HasSize)
            {
                long? sizeValueId = productDto.ProductOptionValues.FirstOrDefault().SizeValueId;
                long optionId = unitOfWork.SizeHelperRepository.GetByID(sizeValueId.Value).OptionId;
                List<long?> sizes = productDto.ProductOptionValues.Select(c => c.SizeValueId).Distinct().ToList();
                editOption(optionId, productId, false, sizes);



                foreach (var sizeAttribute in productDto.SizeAttributes)
                {

                    ProductSizeAttribute sizeAttr = unitOfWork.ProductSizeAttributeRepository
                        .Get(c => c.ProductId == productId && c.SizeAttributeId == sizeAttribute.SizeAttributeId && c.ValueId == sizeAttribute.SizeValueId).FirstOrDefault();
                    sizeAttr.DateModified = DateTime.Now;
                    //sizeAttr.ValueId = sizeAttribute.SizeValueId;
                    sizeAttr.Value = sizeAttribute.Value??0;
                    unitOfWork.ProductSizeAttributeRepository.Update(sizeAttr);
                    unitOfWork.Save();
                }
            }

            if (!productDto.HasColor && !productDto.HasSize)
            {

                ProductSKU sku = unitOfWork.ProductSKURepository.Get(c => c.ProductId == productId).FirstOrDefault();
                sku.Quentity = productDto.Quantity;
                unitOfWork.ProductSKURepository.Update(sku);
                unitOfWork.Save();
            }



            foreach (var skuoptionvalue in productDto.ProductOptionValues)
            {
                string SKU = "P" + productId;
                if (productDto.HasColor) SKU += "C" + skuoptionvalue.ColorValueId;
                if (productDto.HasSize) SKU += "S" + skuoptionvalue.SizeValueId;
                ProductSKU sku = unitOfWork.ProductSKURepository.Get(c => c.SKU == SKU).FirstOrDefault();

                if (sku == null)
                {
                    SKU = "P"+productId;
                    sku = new ProductSKU();
                    sku.Price = (int)skuoptionvalue.Price;
                    if (skuoptionvalue.PricePrefix == "-")
                        sku.Price *= -1;
                    sku.ProductId = productId;
                    sku.Quentity = skuoptionvalue.Quantity;
                    sku.SKUId = unitOfWork.ProductSKURepository.Get(c => true).Max(c => c.SKUId) + 1;
                    ProductSKUOptionValue productSkuOptionValue1 = new ProductSKUOptionValue();
                    ProductSKUOptionValue productSkuOptionValue2 = new ProductSKUOptionValue();
                    if (productDto.HasColor)
                    {
                        SKU += "C" + skuoptionvalue.ColorValueId;
                        productSkuOptionValue1.ProductId = productId;
                        productSkuOptionValue1.ValueId = skuoptionvalue.ColorValueId.Value;
                        productSkuOptionValue1.SKUId = sku.SKUId;
                        productSkuOptionValue1.OptionId = unitOfWork.ColorHelperRepository.
                            GetByID(skuoptionvalue.ColorValueId).OptionId;

                    }
                    if (productDto.HasSize)
                    {
                        SKU += "S" + skuoptionvalue.SizeValueId;
                        productSkuOptionValue2.ProductId = productId;
                        productSkuOptionValue2.ValueId = skuoptionvalue.SizeValueId.Value;
                        productSkuOptionValue2.SKUId = sku.SKUId;
                        productSkuOptionValue2.OptionId = unitOfWork.SizeHelperRepository.
                            GetByID(skuoptionvalue.SizeValueId).OptionId;

                    }
                    sku.SKU = SKU;
                    sku = unitOfWork.ProductSKURepository.Insert(sku);
                    unitOfWork.Save();
                    if (productDto.HasColor)
                    {
                        unitOfWork.ProductSKUOptionValueRepository.Insert(productSkuOptionValue1);
                        unitOfWork.Save();
                    }
                    if (productDto.HasSize)
                    {
                        unitOfWork.ProductSKUOptionValueRepository.Insert(productSkuOptionValue2);
                        unitOfWork.Save();
                    }

                    if (skuoptionvalue.ImagesUrls == null)
                        skuoptionvalue.ImagesUrls = new List<string>();

                    foreach (var image in skuoptionvalue.ImagesUrls)
                    {
                        Image img = new Image();
                        img.OptionValueProductId = productId;
                        img.OptionValueOptionId = unitOfWork.ColorHelperRepository.
                            GetByID(skuoptionvalue.ColorValueId).OptionId;
                        img.OptionValueValueId = skuoptionvalue.ColorValueId;
                        img.AlternateText = SKU;
                        img.ImageUrl = image;
                        unitOfWork.ImageRepository.Insert(img);
                        unitOfWork.Save();
                    }
                    sku.Tags = new List<Tag>();

                    if (skuoptionvalue.ArabicTags == null)
                        skuoptionvalue.ArabicTags = new List<string>();

                    foreach (var tag in skuoptionvalue.ArabicTags)
                    {
                        Tag tg = unitOfWork.TagRepository
                            .Get(c => c.LanguageId == (long)Langs.Arabic && c.Text == tag).FirstOrDefault();
                        if (tg == null)
                        {
                            tg = new Tag();
                            tg.LanguageId = Utils.getLanguage(Langs.Arabic);
                            tg.DateAdded = DateTime.Now;
                            tg.DateModefied = DateTime.Now;
                            tg.Text = tag;
                        }
                        sku.Tags.Add(tg);
                        unitOfWork.Save();

                    }

                    if (skuoptionvalue.EnglishTags == null)
                        skuoptionvalue.EnglishTags = new List<string>();

                    foreach (var tag in skuoptionvalue.EnglishTags)
                    {
                        Tag tg = unitOfWork.TagRepository.Get(c => c.LanguageId == (long)Langs.English && c.Text == tag).FirstOrDefault();
                        if (tg == null)
                        {
                            tg = new Tag();
                            tg.LanguageId = Utils.getLanguage(Langs.English);
                            tg.DateAdded = DateTime.Now;
                            tg.DateModefied = DateTime.Now;
                            tg.Text = tag;
                        }
                        sku.Tags.Add(tg);
                        unitOfWork.Save();
                    }
                }
                else
                {
                    sku.Price = (int)skuoptionvalue.Price;
                    if (skuoptionvalue.PricePrefix == "-")
                        sku.Price *= -1;
                    sku.Quentity = skuoptionvalue.Quantity;
                    unitOfWork.ProductSKURepository.Update(sku);
                    unitOfWork.Save();

                    if (skuoptionvalue.ImagesUrls == null)
                        skuoptionvalue.ImagesUrls = new List<string>();
                    if (productDto.HasColor) { 
                    long optionid = unitOfWork.ColorHelperRepository.
                            GetByID(skuoptionvalue.ColorValueId).OptionId;
                    List<string> skuImages = unitOfWork.ImageRepository
                        .Get(c => c.OptionValueProductId == productId && c.OptionValueOptionId == optionid &&
                            c.OptionValueValueId == skuoptionvalue.ColorValueId&&c.AlternateText==sku.SKU)
                        .Select(c => c.ImageUrl).ToList();
                    if (skuImages == null) skuImages = new List<string>();
                    if (skuoptionvalue.ImagesUrls == null) skuoptionvalue.ImagesUrls = new List<string>();
                    foreach (var image in skuoptionvalue.ImagesUrls)
                    {
                        if (!skuImages.Contains(image))
                        {
                            Image img = new Image();
                            img.OptionValueProductId = productId;
                            img.OptionValueOptionId = unitOfWork.ColorHelperRepository.
                                GetByID(skuoptionvalue.ColorValueId).OptionId;
                            img.OptionValueValueId = skuoptionvalue.ColorValueId;
                            img.AlternateText = SKU;
                            img.ImageUrl = image;
                            unitOfWork.ImageRepository.Insert(img);
                            unitOfWork.Save();
                        }
                    }
                    foreach (var image in skuImages)
                    {
                        if (!skuoptionvalue.ImagesUrls.Contains(image))
                        {
                            Image img = unitOfWork.ImageRepository
                                .Get(c => c.OptionValueProductId == productId && c.OptionValueOptionId == optionid
                                    && c.OptionValueValueId == skuoptionvalue.ColorValueId
                                    && c.ImageUrl == image&& c.AlternateText==sku.SKU)
                                .FirstOrDefault();
                            unitOfWork.ImageRepository.Delete(img);
                            unitOfWork.Save();
                        }
                    }
                    }


                    sku.Tags = new List<Tag>();

                    if (skuoptionvalue.ArabicTags == null)
                        skuoptionvalue.ArabicTags = new List<string>();

                    foreach (var tag in skuoptionvalue.ArabicTags)
                    {
                        Tag tg = unitOfWork.TagRepository
                            .Get(c => c.LanguageId == (long)Langs.Arabic && c.Text == tag).FirstOrDefault();
                        if (tg == null)
                        {
                            tg = new Tag();
                            tg.LanguageId = Utils.getLanguage(Langs.Arabic);
                            tg.DateAdded = DateTime.Now;
                            tg.DateModefied = DateTime.Now;
                            tg.Text = tag;
                        }
                        if (!sku.Tags.Contains(tg))
                            sku.Tags.Add(tg);
                        unitOfWork.Save();
                    }
                    //To get over the null reference Exception
                    if (skuoptionvalue.ArabicTags == null)
                        skuoptionvalue.ArabicTags = new List<string>();
                    if (skuoptionvalue.EnglishTags == null)
                        skuoptionvalue.EnglishTags = new List<string>();
                    if (productDto.ArabicProductTags == null)
                        productDto.ArabicProductTags = new List<string>();
                    if (productDto.EnglishProductTags == null)
                        productDto.EnglishProductTags = new List<string>();

                    foreach (var tag in sku.Tags.ToList())
                    {
                        if (!skuoptionvalue.ArabicTags.Contains(tag.Text) && !skuoptionvalue.EnglishTags.Contains(tag.Text) && !productDto.ArabicProductTags.Contains(tag.Text) && !productDto.EnglishProductTags.Contains(tag.Text))
                        {
                            sku.Tags.Remove(tag);
                            // unitOfWork.ProductSKURepository.Delete(tag);
                            unitOfWork.Save();
                        }
                    }

                    if (skuoptionvalue.EnglishTags == null)
                        skuoptionvalue.EnglishTags = new List<string>();

                    foreach (var tag in skuoptionvalue.EnglishTags)
                    {
                        Tag tg = unitOfWork.TagRepository.Get(c => c.LanguageId == (long)Langs.English && c.Text == tag).FirstOrDefault();
                        if (tg == null)
                        {
                            tg = new Tag();
                            tg.LanguageId = Utils.getLanguage(Langs.English);
                            tg.DateAdded = DateTime.Now;
                            tg.DateModefied = DateTime.Now;
                            tg.Text = tag;
                        }
                        if (!sku.Tags.Contains(tg))
                            sku.Tags.Add(tg);
                        unitOfWork.Save();
                    }
                    //foreach (var tag in sku.Tags)
                    //{
                    //    if (!skuoptionvalue.EnglishTags.Contains(tag.Text))
                    //    {
                    //        unitOfWork.ProductSKURepository.Delete(tag);
                    //        unitOfWork.Save();
                    //    }
                    //}
                }
            }
            if (productDto.ArabicProductTags == null)
                productDto.ArabicProductTags = new List<string>();
            foreach (var tag in productDto.ArabicProductTags)
            {
                Tag tg = unitOfWork.TagRepository.Get(c => c.LanguageId == (long)Langs.Arabic && c.Text == tag).FirstOrDefault();
                if (tg == null)
                {
                    tg = new Tag();
                    tg.LanguageId = Utils.getLanguage(Langs.Arabic);
                    tg.DateAdded = DateTime.Now;
                    tg.DateModefied = DateTime.Now;
                    tg.Text = tag;
                }

                foreach (var sku in unitOfWork.ProductRepository.GetByID(productId).ProductSKUs)
                {
                    if (sku.Tags == null)
                        sku.Tags = new List<Tag>();
                    if (!sku.Tags.Contains(tg))
                        sku.Tags.Add(tg);
                    unitOfWork.Save();
                }
            }
            if (productDto.EnglishProductTags == null)
                productDto.EnglishProductTags = new List<string>();
            foreach (var tag in productDto.EnglishProductTags)
            {
                Tag tg = unitOfWork.TagRepository.Get(c => c.LanguageId == (long)Langs.English && c.Text == tag).FirstOrDefault();
                if (tg == null)
                {
                    tg = new Tag();
                    tg.LanguageId = Utils.getLanguage(Langs.English);
                    tg.DateAdded = DateTime.Now;
                    tg.DateModefied = DateTime.Now;
                    tg.Text = tag;
                }
                foreach (var sku in unitOfWork.ProductRepository.GetByID(productId).ProductSKUs)
                {
                    if (sku.Tags == null)
                        sku.Tags = new List<Tag>();
                    if (!sku.Tags.Contains(tg))
                        sku.Tags.Add(tg);
                    unitOfWork.Save();
                }
            }

            // unitOfWork.Save();
            return new OperationDetails(true, "تمت عملية التعديل بنجاح", "");

        }

        private void deleteSKU(string skuString)
        {
            ProductSKU sku = unitOfWork.ProductSKURepository.Get(c => c.SKU == skuString).FirstOrDefault();
            long skuid = sku.SKUId;
            foreach (var tag in sku.Tags.ToList())
            {
                sku.Tags.Remove(tag);
                unitOfWork.Save();
            }

            List<ProductSKUOptionValue> links = sku.ProductSKUOptionValues.ToList();
            links.Clear();
            unitOfWork.Save();
            //foreach (var link  in links)
            //{
            //    links.Remove(link);
            //    unitOfWork.Save();    
            //}
            List<ProductDiscount> discounts = sku.ProductSkuDiscounts.ToList();
            discounts.Clear();
            unitOfWork.Save();

            List<ShoppingCart> carts = sku.ShoppingCarts.ToList();
            carts.Clear();
            unitOfWork.Save();

            unitOfWork.ProductSKURepository.Delete(sku);
            unitOfWork.Save();




        }
        private void editOption(long optionId, long productId, bool isColor, List<long?> lst)
        {
            OptionHelper helper = unitOfWork.OptionHelperRepository.GetByID(optionId);
            Option option = unitOfWork.OptionRepository
                .Get(c => c.ProductId == productId && c.OptionId == optionId).FirstOrDefault();


            if (isColor)
            {
                List<ColorHelper> colors = unitOfWork.ColorHelperRepository.Get(c => c.OptionId == optionId && lst.Contains(c.Id)).ToList();
                foreach (var color in colors)
                {
                    OptionValue optionValue = unitOfWork.OptionValueRepository
                        .Get(c => c.ProductId == productId && c.OptionId == optionId && c.ValueId == color.Id).FirstOrDefault();
                    if (optionValue == null)
                    {
                        optionValue = new OptionValue();
                        optionValue.OptionId = optionId;
                        optionValue.ProductId = productId;
                        optionValue.Value = color.Value;
                        optionValue.ValueId = color.Id;
                        optionValue.ImageUrl = color.ImageUrl;
                        OptionValueDescription arabic = new OptionValueDescription();
                        arabic.LanguageId = (long)Langs.Arabic;
                        arabic.Name = color.ArabicName;
                        arabic.OptionId = optionId;
                        arabic.ValueId = color.Id;
                        arabic.ProductId = productId;
                        OptionValueDescription english = new OptionValueDescription();
                        english.LanguageId = (long)Langs.English;
                        english.Name = color.EngishName;
                        english.OptionId = optionId;
                        english.ValueId = color.Id;
                        english.ProductId = productId;
                        unitOfWork.OptionValueRepository.Insert(optionValue);
                        unitOfWork.Save();
                        unitOfWork.OptionValueDescriptionRepository.Insert(arabic);
                        unitOfWork.OptionValueDescriptionRepository.Insert(english);
                        unitOfWork.Save();
                    }
                }
                List<OptionValue> prodColors = unitOfWork.OptionValueRepository
                    .Get(c => c.ProductId == productId && c.OptionId == optionId).ToList();
                foreach (var color in prodColors)
                {
                    if (!colors.Select(c => c.Id).Contains(color.ValueId))
                    {
                        var arabicDescriptions = unitOfWork.OptionValueDescriptionRepository.Get(c => c.ValueId == color.ValueId && c.ProductId == productId && c.OptionId == optionId && c.LanguageId == (long)Langs.Arabic).ToList();
                        var englishDescriptions = unitOfWork.OptionValueDescriptionRepository.Get(c => c.ValueId == color.ValueId && c.ProductId == productId && c.OptionId == optionId && c.LanguageId == (long)Langs.English).ToList();
                        foreach (var desc in arabicDescriptions)
                        {
                            unitOfWork.OptionValueDescriptionRepository.Delete(desc);
                            unitOfWork.Save();
                        }
                        foreach (var desc in englishDescriptions)
                        {
                            unitOfWork.OptionValueDescriptionRepository.Delete(desc);
                            unitOfWork.Save();
                        }

                        List<Image> images = color.Images.ToList();
                        images.Clear();
                        unitOfWork.Save();

                        unitOfWork.OptionValueRepository.Delete(color);
                        unitOfWork.Save();
                    }
                }

            }
            else
            {
                List<SizeHelper> sizes = unitOfWork.SizeHelperRepository.Get(c => c.OptionId == optionId && lst.Contains(c.Id)).ToList();
                foreach (var size in sizes)
                {
                    OptionValue optionValue = unitOfWork.OptionValueRepository
                        .Get(c => c.ProductId == productId && c.OptionId == optionId && c.ValueId == size.Id).FirstOrDefault();
                    if (optionValue == null)
                    {
                        optionValue = new OptionValue();
                        optionValue.OptionId = optionId;
                        optionValue.ProductId = productId;
                        optionValue.Value = size.Value;
                        optionValue.ValueId = size.Id;
                        OptionValueDescription arabic = new OptionValueDescription();
                        arabic.LanguageId = (long)Langs.Arabic;
                        arabic.Name = size.ArabicName;
                        arabic.OptionId = optionId;
                        arabic.ValueId = size.Id;
                        arabic.ProductId = productId;
                        OptionValueDescription english = new OptionValueDescription();
                        english.LanguageId = (long)Langs.English;
                        english.Name = size.EngishName;
                        english.OptionId = optionId;
                        english.ValueId = size.Id;
                        english.ProductId = productId;
                        unitOfWork.OptionValueRepository.Insert(optionValue);
                        unitOfWork.Save();
                        unitOfWork.OptionValueDescriptionRepository.Insert(arabic);
                        unitOfWork.OptionValueDescriptionRepository.Insert(english);
                        unitOfWork.Save();
                    }
                }
                List<OptionValue> prodSizes = unitOfWork.OptionValueRepository
                    .Get(c => c.ProductId == productId && c.OptionId == optionId).ToList();
                foreach (var size in prodSizes)
                {
                    if (!sizes.Select(c => c.Id).Contains(size.ValueId))
                    {
                        var arabicDescriptions = unitOfWork.OptionValueDescriptionRepository.Get(c => c.ValueId == size.ValueId && c.ProductId == productId && c.OptionId == optionId && c.LanguageId == (long)Langs.Arabic).ToList();
                        var englishDescriptions = unitOfWork.OptionValueDescriptionRepository.Get(c => c.ValueId == size.ValueId && c.ProductId == productId && c.OptionId == optionId && c.LanguageId == (long)Langs.English).ToList();
                        foreach (var desc in arabicDescriptions)
                        {
                            unitOfWork.OptionValueDescriptionRepository.Delete(desc);
                            unitOfWork.Save();
                        }
                        foreach (var desc in englishDescriptions)
                        {
                            unitOfWork.OptionValueDescriptionRepository.Delete(desc);
                            unitOfWork.Save();
                        }
                        unitOfWork.OptionValueRepository.Delete(size);
                        unitOfWork.Save();
                    }
                }

            }

        }

    }
}
