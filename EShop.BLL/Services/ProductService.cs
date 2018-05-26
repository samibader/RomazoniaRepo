using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using EShop.Common;
using EShop.DAL.Entities;
using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EShop.DAL.Migrations;

namespace EShop.BLL.Services
{
    public class ProductService : IProductService
    {
        
        IUnitOfWork unitOfWork { get; set; }
        private ICategoryService _categoryService;
        public ProductService(IUnitOfWork uow, ICategoryService categoryService)
        {
            unitOfWork = uow;
            _categoryService = categoryService;

        }
       

        public OperationDetails DeleteOption(long optionId)
        {
            OptionHelper option = unitOfWork.OptionHelperRepository.GetByID(optionId);
            if (option == null)
                return new OperationDetails(false, "الخيار غير موجودة", "");
            if (!option.SizeAttributes.Any())
                return new OperationDetails(false, "لا يمكن حذف الخيار لوجود واصفات قياس مرتبطة", "");
            if(unitOfWork.OptionRepository.Get(c=>c.OptionId==optionId).Any())
                return new OperationDetails(false, "لا يمكن حذف الخيار لوجود منتجات مرتبطة", "");

            if (unitOfWork.ColorHelperRepository.Get(c => c.OptionId == optionId).Any())
                return new OperationDetails(false, "لا يمكن حذف الخيار لوجود ألوان مرتبطة", "");

            if (unitOfWork.SizeHelperRepository.Get(c => c.OptionId == optionId).Any())
                return new OperationDetails(false, "لا يمكن حذف الخيار لوجود قياسات مرتبطة", "");

            
            unitOfWork.OptionRepository.Delete(option);
            unitOfWork.Save();
            return new OperationDetails(true, "تمت عملية الحذف", "");
        }

        public OperationDetails DeleteOptionValue(long optionId,long valueId)
        {
            if (optionId == 3)
            {
                ColorHelper value = unitOfWork.ColorHelperRepository.GetByID(valueId);
                if (value == null)
                    return new OperationDetails(false, "الخيار غير موجود", "");

                if (unitOfWork.OptionValueRepository.Get(c => c.ValueId == valueId&&c.OptionId==optionId).Any())
                    return new OperationDetails(false, "لا يمكن حذف القيمة لوجود منتجات مرتبطة", "");



                unitOfWork.ColorHelperRepository.Delete(value);
                unitOfWork.Save();
                return new OperationDetails(true, "تمت عملية الحذف", "");
            }
            else
            {
                SizeHelper value = unitOfWork.SizeHelperRepository.GetByID(valueId);
                if (value == null)
                    return new OperationDetails(false, "القيمة غير موجودة", "");

                if (unitOfWork.OptionValueRepository.Get(c => c.ValueId == valueId&&c.OptionId==optionId).Any())
                    return new OperationDetails(false, "لا يمكن حذف القيمة لوجود منتجات مرتبطة", "");



                unitOfWork.SizeHelperRepository.Delete(value);
                unitOfWork.Save();
                return new OperationDetails(true, "تمت عملية الحذف", "");
            }
           
        }

      

        public OperationDetails DeleteProduct(long productId)
        {
            Product product = unitOfWork.ProductRepository.GetByID(productId);

            List<ProductDescription> productDescriptions= product.ProductDescriptions.ToList();
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

            List<ProductSizeAttribute> productSizeAttributes= product.SizeAttributes.ToList();
            foreach (var sizeattr in productSizeAttributes)
            {
                unitOfWork.ProductSizeAttributeRepository.Delete(sizeattr);
                unitOfWork.Save();
            }

            List<ProductAttributeValue> productAtteibuteValues= product.ProductAttributeValues.ToList();
            foreach (var attrVal in productAtteibuteValues)
            {
                unitOfWork.ProductAttributeValueRepository.Delete(attrVal);
                unitOfWork.Save();
            }

            List<Option> productOptions= product.Options.ToList();
            foreach(var option in productOptions)
            {
                List<OptionDescription> optionDescriptions = option.OptionDescriptions.ToList();
                foreach (var desc in optionDescriptions)
                {
                    unitOfWork.OptionDescriptionRepository.Delete(desc);
                    unitOfWork.Save();
                }

                List<OptionValue> optionValues = option.OptionValues.ToList();
                foreach(var value in optionValues)
                {
                    List<OptionValueDescription> valueDescriptions = value.OptionValueDescriptions.ToList();
                    foreach (var desc in valueDescriptions)
                    {
                        unitOfWork.OptionValueDescriptionRepository.Delete(desc);
                        unitOfWork.Save();
                    }

                    List<Image> valueImages= value.Images.ToList();
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
                List<ProductDiscount> skuDiscounts=  sku.ProductSkuDiscounts.ToList();
                foreach (var discount in skuDiscounts)
                {
                    unitOfWork.DiscountRepository.Delete(discount);
                    unitOfWork.Save();
                }

               List<Tag> skuTags= sku.Tags.ToList();
               foreach (var tag in skuTags)
               {
                   unitOfWork.TagRepository.Delete(tag);
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
        public List<TagDTO> GetAllTags()
        {
            List<Tag> tags = unitOfWork.TagRepository.Get(c => true).ToList();
            List<TagDTO> tagDTOs = new List<TagDTO>();
            foreach (var tag in tags)
            {
                tagDTOs.Add(new TagDTO()
                {
                    Name = tag.Text
                });
            }
            return tagDTOs;
            //return new List<TagDTO>();
        }

        public List<TagDTO> GetProductTags(long productId, Langs l)
        {
            List<Tag> tags = unitOfWork.ProductRepository.GetByID(productId).Tags.ToList();
            
            List<TagDTO> names = new List<TagDTO>();
            foreach (var tag in tags)
            {
                names.Add(new TagDTO()
                {
                    Name = tag.Text,
                });
            }

            return names;
            //return new List<TagDTO>();
        }
        public List<TagDTO> GetProductTagsBySkuId(long skuId, Langs l)
        {
            List<Tag> tags = unitOfWork.ProductSKURepository.Get(c=>c.SKUId==skuId).FirstOrDefault().Tags.Where(t=>t.LanguageId==(long)l).ToList();

            List<TagDTO> names = new List<TagDTO>();
            foreach (var tag in tags)
            {
                
                names.Add(new TagDTO()
                {
                    Name = tag.Text,
                });
            }

            return names;
            //return new List<TagDTO>();
        }
        public OperationDetails AddProductRate(ProductRateDTO productRate)
        {
            ProductRate rate = new ProductRate();
            rate.CreationDate = DateTime.Now;
            rate.Note = productRate.Note;
            rate.ProductId = productRate.productId;
            rate.PriceRate = productRate.Rate1;
            rate.QualityRate = productRate.Rate2;
            rate.ThirdRate = productRate.Rate3;
            rate.UserId = productRate.UserId;
            unitOfWork.ProductRateRepository.Insert(rate);
            unitOfWork.Save();
            return new OperationDetails(true,"تمت الإضافة بنجاح","");
        }
        public List<ProductRateDTO> GetProductRates(long productId)
        {
            List<long> rateIds = unitOfWork.ProductRateRepository.Get(c => c.ProductId == productId).Select(c => c.Id).ToList();
            List<ProductRateDTO> rates = new List<ProductRateDTO>();
            foreach (var id in rateIds)
            {
                rates.Add(GetRateById(id));
            }
            return rates;

        }

        public int GetProductRate(long productId)
        {
            var rates = GetProductRates(productId);
            var res = 0;
            foreach (var rate in rates)
            {
                res += rate.Rate;
            }
            res = (int) Math.Ceiling(res/(double)rates.Count);
            return res;
        }
        public ProductRateDTO GetRateById(long rateId)
        {
            ProductRateDTO rateDTO = new ProductRateDTO();
            ProductRate rate=unitOfWork.ProductRateRepository.GetByID(rateId);
            rateDTO.Note = rate.Note;
            rateDTO.Rate1 = rate.PriceRate;
            rateDTO.Rate2 = rate.QualityRate;
            rateDTO.Rate3 = rate.ThirdRate;
            rateDTO.Rate =(int)Math.Ceiling((rate.PriceRate + rate.QualityRate + rate.ThirdRate)/3.0);
            rateDTO.Id = rate.Id;
            rateDTO.date = rate.CreationDate.Value;
            rateDTO.productId = rate.ProductId;
            return rateDTO;
        }

        public OperationDetails EditProductDescription(ProductDTO product, Langs l)
        {
            long lan = Utils.getLanguage(l);
            ProductDescription description = unitOfWork.ProductDescriptionRepository.Get(c => c.ProductId == product.Id & c.LanguageId == lan).FirstOrDefault();
            if (description == null)
                return new OperationDetails(false, "error editing product descriptipn", "null");
            description.Text = product.Text;
            unitOfWork.ProductDescriptionRepository.Update(description);
            unitOfWork.Save();
            return new OperationDetails(true, "product description changed successfully", "null");

        }
        public long GetProductIdFromSKU(string sku)
        {
            return unitOfWork.ProductSKURepository.Get(c => c.SKU == sku).FirstOrDefault().ProductId;
        }
        //.......................
        private List<ProductDTO> GetRandomFive(List<ProductDTO> products)
        {
            Random random = new Random();
            int seed = random.Next();
            products = products.OrderBy(s => (~(s.Id & seed)) & (s.Id | seed)).ToList();
            return products.Take(Math.Min(products.Count, 5)).ToList();
        }
        public MainPageProductsDTO GetMainPageProducts(Langs l, Currency currency,WebSites w)
        {
            MainPageProductsDTO mainPageProductsDTO = new MainPageProductsDTO();
            mainPageProductsDTO.MainCategories = _categoryService.GetRandomCategories(l, w).AsEnumerable()
                .Where(c => GetRandomFive(GetCategoryProducts(c.Id, l, currency)).Count >= 5).Take(3).ToList();


            foreach (var categoryMenuDto in mainPageProductsDTO.MainCategories)
            {

                var products = GetRandomFive(GetCategoryProducts(categoryMenuDto.Id, l, currency));
                mainPageProductsDTO.Products.Add(products);
            }
            //mainPageProductsDTO.Women = GetRandomFive(GetCategoryProducts((long)BaseCategories.Women, l, currency));
            //mainPageProductsDTO.Men = GetRandomFive(GetCategoryProducts((long)BaseCategories.Men, l, currency));
            //mainPageProductsDTO.Babe = GetRandomFive(GetCategoryProducts((long)BaseCategories.Babe, l, currency));
            return mainPageProductsDTO;
        }
        //......
        private long GetDefaultProductSKU(long productId)
        {
            return unitOfWork.ProductRepository.GetByID(productId).ProductSKUs.FirstOrDefault().SKUId;
        }
        private IEnumerable<long> GetSKUIdBySizeName(long productId, string sizeValue, long languageId)
        {
            return unitOfWork.ProductSKUOptionValueRepository.Get(op =>
                                            op.ProductId == productId &&
                                            op.Option.IsColor == false &&
                                            op.OptionValue.OptionValueDescriptions.Where(c =>
                                                c.LanguageId == languageId).FirstOrDefault().Name == sizeValue
                                            ).Select(op => op.SKUId);
        }
        private IEnumerable<long> GetSKUIdByColorName(long productId, string colorValue, long languageId)
        {
            return unitOfWork.ProductSKUOptionValueRepository.Get(op =>
                                            op.ProductId == productId &&
                                            op.Option.IsColor == true &&
                                            op.OptionValue.OptionValueDescriptions.Where(c =>
                                                c.LanguageId == languageId).FirstOrDefault().Name == colorValue
                                            ).Select(op => op.SKUId);
        }
        private string GetOptionNameBySKUId(long skuId, long languageId)
        {
            return unitOfWork.ProductSKURepository.Get(s => s.SKUId == skuId).FirstOrDefault().
                                            ProductSKUOptionValues.FirstOrDefault().
                                            OptionValue.OptionValueDescriptions.Where(op =>
                                            op.LanguageId == languageId).FirstOrDefault().Name;
        }
        //Just Color
        private string GetSizeNameBySKUId(long skuId, long languageId)
        {
            return unitOfWork.ProductSKURepository.Get(s => s.SKUId == skuId).FirstOrDefault().
                                            ProductSKUOptionValues.Where(c => c.OptionValue.Option.IsColor == false).FirstOrDefault().
                                            OptionValue.OptionValueDescriptions.Where(op =>
                                            op.LanguageId == languageId).FirstOrDefault().Name;
        }
        private string GetColorNameBySKUId(long skuId, long languageId)
        {
            return unitOfWork.ProductSKURepository.Get(s => s.SKUId == skuId).FirstOrDefault().
                                            ProductSKUOptionValues.Where(c => c.OptionValue.Option.IsColor == true).FirstOrDefault().
                                            OptionValue.OptionValueDescriptions.Where(op =>
                                            op.LanguageId == languageId).FirstOrDefault().Name;
        }
        public void Fix(long productId, ref long? skuId, ref string colorName, ref string sizeName)
        {
            long languageId = Utils.getLanguage(Langs.English);
            int numOfColors = GetProductColors(productId, Langs.English).Count,
                numOfSizes = GetProductSizes(productId, Langs.English).Count;
            if (skuId == null)
            {
                if (numOfColors == 0)
                {
                    if (numOfSizes != 0)
                    {
                        if (sizeName == null)
                        {
                            skuId = GetDefaultProductSKU(productId);
                            sizeName = GetOptionNameBySKUId(skuId.Value, languageId);
                        }
                        else
                        {
                            string sizeValue = sizeName;
                            skuId = GetSKUIdBySizeName(productId, sizeValue, languageId).FirstOrDefault();
                        }
                    }
                    else
                    {
                        skuId = GetDefaultProductSKU(productId);
                    }
                }
                else
                {
                    if (numOfSizes == 0)
                    {
                        if (colorName == null)
                        {
                            skuId = GetDefaultProductSKU(productId);
                            colorName = GetOptionNameBySKUId(skuId.Value, languageId);
                        }
                        else
                        {
                            string colorValue = colorName;
                            skuId = GetSKUIdByColorName(productId, colorValue, languageId).FirstOrDefault();
                        }
                    }
                    else
                    {
                        if (sizeName == null && colorName == null)
                        {
                            skuId = GetDefaultProductSKU(productId);
                            colorName = GetColorNameBySKUId(skuId.Value, languageId);
                            sizeName = GetSizeNameBySKUId(skuId.Value, languageId);
                        }
                        else if (sizeName != null && colorName == null)
                        {
                            string sizeValue = sizeName;
                            skuId = GetSKUIdBySizeName(productId, sizeValue, languageId).FirstOrDefault();
                            if (skuId == null)
                                skuId = GetDefaultProductSKU(productId);
                            sizeName = GetSizeNameBySKUId(skuId.Value, languageId);
                            colorName = GetColorNameBySKUId(skuId.Value, languageId);
                        }
                        else if (sizeName == null && colorName != null)
                        {
                            string colorValue = colorName;
                            skuId = GetSKUIdByColorName(productId, colorValue, languageId).FirstOrDefault();
                            if (skuId == null)
                                skuId = GetDefaultProductSKU(productId);
                            colorName = GetColorNameBySKUId(skuId.Value, languageId);
                            sizeName = GetSizeNameBySKUId(skuId.Value, languageId);
                        }
                        else
                        {
                            // Product have colors and sizes

                            //string sizeValue = sizeName;
                            //string colorValue = colorName;
                            var skuId1 = GetSKUIdByColorName(productId, colorName, languageId);
                            if (skuId1.Count() == 0)
                            {
                                // Validate and set defaut color if error
                                skuId = GetDefaultProductSKU(productId);
                                colorName = GetColorNameBySKUId(skuId.Value, languageId);
                                skuId1 = GetSKUIdByColorName(productId, colorName, languageId);
                            }
                            var skuId2 = GetSKUIdBySizeName(productId, sizeName, languageId);
                            if (skuId2.Count() == 0)
                            {
                                // Validate and set defaut size if error
                                skuId = GetDefaultProductSKU(productId);
                                sizeName = GetColorNameBySKUId(skuId.Value, languageId);
                                skuId2 = GetSKUIdBySizeName(productId, sizeName, languageId);
                            }

                            skuId = skuId1.Intersect(skuId2).FirstOrDefault();
                            if(skuId==0)
                            {
                                // no compination found !!
                                skuId = GetSKUIdByColorName(productId, colorName, languageId).FirstOrDefault();
                                sizeName = GetSizeNameBySKUId(skuId.Value, languageId);
                            }

                        }
                    }

                }



            }
        }

        //.......................
        public ProductDTO GetProductSKU(long skuId, Langs l, Currency c)
        {
            ProductDTO productDTO = GetSKUInformation(skuId, l, c);
            productDTO.SizeAttributes = new List<SizeAttributeViewDTO>();

            productDTO.Sizes = GetProductSizes(productDTO.Id, l);
            if (productDTO.Sizes.Count == 0)
            {
                productDTO.SelectedSize = -1;
                productDTO.Sizes = null;
            }
            else
            {
                var sizeOption = unitOfWork.ProductSKURepository.Get(s => s.SKUId == skuId).FirstOrDefault().
                                ProductSKUOptionValues.Select(op => op.OptionValue).
                                Where(op => op.Option.IsColor == false).FirstOrDefault();
                if (sizeOption != null)
                {
                    long sizeId = sizeOption.ValueId;
                    for (int i = 0; i < productDTO.Sizes.Count; i++)
                    {
                        if (productDTO.Sizes[i].ValueId == sizeId)
                            productDTO.SelectedSize = i;
                    }
                }
                else
                {
                    long sizeId = unitOfWork.ProductSKURepository.Get(s => s.SKUId == skuId).FirstOrDefault().ProductSKUOptionValues.FirstOrDefault().ValueId;
                    for (int i = 0; i < productDTO.Sizes.Count; i++)
                    {
                        if (productDTO.Sizes[i].ValueId == sizeId)
                            productDTO.SelectedSize = i;
                    }
                }
            }

            productDTO.Colors = GetProductColors(productDTO.Id, l);
            if (productDTO.Colors.Count == 0)
            {
                productDTO.SelectedColor = -1;
                productDTO.Colors = null;
            }
            else
            {
                long colorId = unitOfWork.ProductSKURepository.Get(op => op.SKUId == skuId).FirstOrDefault().
                                ProductSKUOptionValues.Select(op => op.OptionValue).
                                Where(op => op.Option.IsColor == true).FirstOrDefault().ValueId;

                for (int i = 0; i < productDTO.Colors.Count; i++)
                {
                    if (productDTO.Colors[i].ValueId == colorId)
                        productDTO.SelectedColor = i;
                }
                var colorOptionId = productDTO.Colors[productDTO.SelectedColor].OptionId;
                var colorImages = unitOfWork.OptionValueRepository.Get(op => op.ProductId == productDTO.Id
                                                                                     && op.OptionId == colorOptionId
                                                                                     && op.ValueId == colorId)
                                                                               .FirstOrDefault().Images;
                if (colorImages.Count>0)
                    productDTO.Images = colorImages.Select(img => img.ImageUrl).ToList();
                else
                {
                    productDTO.Images.Add ( DefaultImages.Product);
                }

                if (productDTO.SelectedSize >= 0)
                    for (int i = 0; i < productDTO.Sizes.Count; i++)
                    {
                        long curSize = productDTO.Sizes[i].ValueId, curColor = productDTO.Colors[productDTO.SelectedColor].ValueId,
                                curProduct = productDTO.Id, curColorOption, curSizeOption;
                        ProductSKU psku = unitOfWork.ProductSKURepository.Get(s => s.SKUId == productDTO.SKUId).FirstOrDefault();

                        //curColorOption=psku.
                        //                ProductSKUOptionValues.Select(op => op.OptionValue).
                        //                Where(op=>op.ValueId==curColor).FirstOrDefault().Option.OptionId;
                        curColorOption = productDTO.Colors[productDTO.SelectedColor].OptionId;
                        //curSizeOption = psku.
                        //                ProductSKUOptionValues.Select(op => op.OptionValue).
                        //                Where(op => op.ValueId == curSize).FirstOrDefault().Option.OptionId;
                        curSizeOption = productDTO.Sizes[i].OptionId;
                        var v1 = unitOfWork.ProductSKUOptionValueRepository.Get(op =>
                                            op.ProductId == curProduct &&
                                            op.ValueId == curColor &&
                                            op.OptionId == curColorOption).Select(op => op.SKUId);
                        var v2 = unitOfWork.ProductSKUOptionValueRepository.Get(op =>
                                            op.ProductId == curProduct &&
                                            op.ValueId == curSize &&
                                            op.OptionId == curSizeOption).Select(op => op.SKUId);
                        var v = v1.Intersect(v2);

                        if (v != null && v.ToList().Count > 0)
                            productDTO.Sizes[i].Available = true;
                        else
                            productDTO.Sizes[i].Available = false;
                    }

                if (productDTO.SelectedSize >= 0)
                {
                    var sizeDto = productDTO.Sizes[productDTO.SelectedSize];
                    var sizAttributes = unitOfWork.ProductSizeAttributeRepository.Get(ps => ps.ValueId == sizeDto.ValueId && ps.ProductId == productDTO.Id ).ToList();
                    
                    foreach (var productSizeAttribute in sizAttributes)
                    {
                        SizeAttributeViewDTO dto = new SizeAttributeViewDTO();
                        var sizeAttribute =
                            unitOfWork.SizeAttributeRepository.GetByID(productSizeAttribute.SizeAttributeId);
                        var option =
                            unitOfWork.SizeHelperRepository.Get(d => d.Id == sizeAttribute.Id).FirstOrDefault().OptionId;
                        dto.SizeAttributeName =
                            sizeAttribute.SizeAttributeDescriptions.Where(d => d.LanguageId == (long) l)
                                .FirstOrDefault()
                                .Name;
                        dto.SizeAttributeValueCm = productSizeAttribute.Value;
                        dto.SizeAttributeValueInch = productSizeAttribute.Value*2.5;
                        productDTO.SizeAttributes.Add(dto);
                    }
                }

            }

            return productDTO;
        }
        private ProductDTO GetSKUInformation(long skuId, Langs l, Currency c)
        {
            long languageId = Utils.getLanguage(l);
            ProductDTO productDTO = new ProductDTO();

            productDTO.SKUId = skuId;
            ProductSKU SKU = unitOfWork.ProductSKURepository.Get(s => s.SKUId == skuId).FirstOrDefault();
            productDTO.Id = SKU.ProductId;
            Product product = unitOfWork.ProductRepository.GetByID(productDTO.Id);
            productDTO.Designer = new DesignerDTO();
            productDTO.Designer.DesignerId = product.DesignerId;
            productDTO.Designer.DesignerName = 
                product.Designer.Descriptions.FirstOrDefault(cc=>cc.LanguageId==(long)l).Text;

            productDTO.Name = product.ProductDescriptions.Where(cc=>cc.LanguageId==(long)l).FirstOrDefault().Name;
            productDTO.IsNew = (DateTime.Now.Year * 365 + DateTime.Now.Month * 30 + DateTime.Now.Day
                               - product.DateAdded.Value.Year * 365 - product.DateAdded.Value.Month * 30 - product.DateAdded.Value.Day)
                               <= 7;
            productDTO.Quantity = SKU.Quentity;
            productDTO.IsAvailable = (SKU.Quentity != 0);
            productDTO.CategoryId = product.CategoryId;

            productDTO.Price = Utils.getCurrency(c, l, product.Price).Item1;
            Tuple<double, String> tuple = Utils.getCurrency(c, l, SKU.Price);
            double SKUPrice = tuple.Item1;
            productDTO.CurrencyName = tuple.Item2;

            productDTO.rates = GetProductRates(productDTO.Id);
            if (productDTO.rates.Count != 0)
                productDTO.TotalRate = (int)Math.Ceiling(productDTO.rates.Sum(cc => cc.Rate) / productDTO.rates.Count + 0.0);
            else productDTO.TotalRate = 0;


            productDTO.DateAdded = product.DateAdded.Value;
            productDTO.OrginalPrice = (productDTO.Price + SKUPrice);
            productDTO.TotalPrice = productDTO.OrginalPrice;
            //if (SKU.Discounts.Count > 0)
            //{
            //    if (SKU.Discounts.FirstOrDefault().IsPercentage)
            //        productDTO.TotalPrice = productDTO.OrginalPrice * (100 - SKU.Discounts.FirstOrDefault().Value) / 100;
            //    else
            //        productDTO.TotalPrice = productDTO.OrginalPrice - SKU.Discounts.FirstOrDefault().Value;
            //    productDTO.IsDiscounted = (SKU.Discounts.FirstOrDefault().Value != 0);

            //}



            ProductDescription productDescription = product.ProductDescriptions.FirstOrDefault(op => op.LanguageId == languageId);
            productDTO.MetaDescriptions = productDescription.MetaDescriptions;
            productDTO.Text = productDescription.Text;
            var images = unitOfWork.ImageRepository.Get(img => img.ProductId == productDTO.Id)
                    .Select(img => img.ImageUrl)
                    .ToList();
            if (images.Count > 0)
                productDTO.Images = images;
            else
            {
               productDTO.Images = new List<string>();
                productDTO.Images .Add(DefaultImages.Product);
            }
                
                

            productDTO.Tags = GetProductTagsBySkuId(productDTO.SKUId, l);
            return productDTO;
        }
        private List<ColorValuesDTO> GetProductColors(long productId, Langs l)
        {
            long languageId = Utils.getLanguage(l);
            List<OptionValue> colorOptionValues = unitOfWork.OptionValueRepository.Get(op =>
                                        op.Option.IsColor == true &&
                                        op.ProductId == productId).ToList();

            List<ColorValuesDTO> colorValueDTOs = new List<ColorValuesDTO>();
            foreach (var colorOptionValue in colorOptionValues)
            {
                ColorValuesDTO colorValueDTO = new ColorValuesDTO();
                colorValueDTO.ColorName = colorOptionValue.OptionValueDescriptions.
                    Where(op => op.LanguageId == languageId).FirstOrDefault().Name;
                colorValueDTO.ColorNameEnglish = colorOptionValue.OptionValueDescriptions.
                    Where(op => op.LanguageId == (long)Langs.English).FirstOrDefault().Name;
                colorValueDTO.IsImages = (colorOptionValue.Value == null);
                colorValueDTO.Image = colorOptionValue.Value??colorOptionValue.ImageUrl;
                colorValueDTO.ValueId = colorOptionValue.ValueId;
                colorValueDTO.OptionId = colorOptionValue.OptionId;
                if(colorOptionValue.Images.FirstOrDefault()!=null)
                    colorValueDTO.ProductImage = colorOptionValue.Images.FirstOrDefault().ImageUrl;
                else
                {
                    colorValueDTO.ProductImage = DefaultImages.Product;
                }
                colorValueDTOs.Add(colorValueDTO);

            }
            return colorValueDTOs;
        }
        private List<SizeValueDTO> GetProductSizes(long productId, Langs l)
        {
            long languageId = Utils.getLanguage(l);
            List<OptionValue> sizeOptionValues = unitOfWork.OptionValueRepository.Get(op =>
                                        op.Option.IsColor == false &&
                                        op.ProductId == productId).ToList();

            List<SizeValueDTO> sizeValueDTOs = new List<SizeValueDTO>();
            foreach (var sizeOptionValue in sizeOptionValues)
            {
                SizeValueDTO sizeValueDTO = new SizeValueDTO();
                sizeValueDTO.ValueId = sizeOptionValue.ValueId;
                sizeValueDTO.Available = true;
                sizeValueDTO.Name = sizeOptionValue.OptionValueDescriptions.Where(op =>
                                        op.LanguageId == languageId).FirstOrDefault().Name;
                sizeValueDTO.EnglishName = sizeOptionValue.OptionValueDescriptions.Where(op =>
                                        op.LanguageId == (long)Langs.English).FirstOrDefault().Name;
                sizeValueDTO.OptionId = sizeOptionValue.OptionId;

                sizeValueDTOs.Add(sizeValueDTO);
            }
            return sizeValueDTOs;

        }
        //.......................
        public List<ProductDTO> GetCategoryTreeProducts(string categoryName, Langs l, Currency c)
        {
            long categoryId = GetCategoryIdByName(categoryName, l);
            return GetCategoryProducts(categoryId, l, c);
        }
        public long GetCategoryIdByName(string categoryName, Langs l)
        {
            long languageId = Utils.getLanguage(l);
            CategoryDescription categoryDescription = unitOfWork.CategoryDescriptionRepository.Get(op =>
                                        op.LanguageId == (long)Langs.English).AsEnumerable().Where(op => (op.Name == categoryName || Utils.GenerateSlug(op.Name) == categoryName)).FirstOrDefault();
            return categoryDescription.CategoryId;

        }


        private List<ProductDTO> GetCategoryProducts(long categoryId, Langs l, Currency c)
        {
            List<ProductDTO> productDTOs = new List<ProductDTO>();

            var subCategories = unitOfWork.CategoryRepository.Get(op => op.ParentId == categoryId);
            if (subCategories.ToList().Count == 0)
            {
                var products = unitOfWork.ProductRepository.Get(op =>op.Status==true&& op.CategoryId == categoryId);
                foreach (var product in products)
                {

                    productDTOs.Add(GetProduct(product.Id, l, c));
                }
                return productDTOs;
            }
            foreach (var category in subCategories)
            {
                productDTOs.AddRange(GetCategoryProducts(category.Id, l, c));
            }
            return productDTOs;
        }
        public ProductDTO GetProduct(long productId, Langs l, Currency c)
        {
            Product product = unitOfWork.ProductRepository.GetByID(productId);
            if (product.ProductSKUs.Count == 0)
                return null;
            return GetProductSKU(product.ProductSKUs.FirstOrDefault().SKUId, l, c);

        }
        public ProductDTO GetProduct(long productId, Langs l, Currency c, string color)
        {
            var product = GetProduct(productId,l,c);
            if (product == null)
                return null;
            if(color!="")
            {
                if(product.Colors!=null)
                {
                    for (int i = 0; i < product.Colors.Count; i++)
                    {
                        if(product.Colors[i].ColorNameEnglish==color)
                        {
                            product.SelectedColor = i;
                        }
                    }
                }
            }

            return product;

        }
        public List<ProductDTO> GetRelatedProducts(long productId, Langs l, Currency c)
        {
            var category = unitOfWork.ProductRepository.GetByID(productId);
            if (category == null) return null;
            List<long> productIds = unitOfWork.CategoryRepository.GetByID(category.CategoryId).Products.Select(cc => cc.Id).ToList() ;
            List<ProductDTO> products = new List<ProductDTO>();
            for (int i = 0; i < Math.Min(productIds.Count, 8); i++)
            {
                products.Add(GetProduct(productIds[i],l,c));
            }
            return products;
        }
        public List<ProductDTO> GetLatestProducts(Langs l, Currency c, WebSites w)
        {
            List<long> productIds = unitOfWork.ProductRepository.Get(cc => true).AsEnumerable()
                .Where(cc => (long)_categoryService.getWebsite(cc.CategoryId) == (long)w)
                .OrderByDescending(cc => cc.DateAdded).Select(cc => cc.Id).ToList();
            List<ProductDTO> products = new List<ProductDTO>();
            for (int i = 0; i < Math.Min(productIds.Count, 8); i++)
            {
                products.Add(GetProduct(productIds[i], l, c));
            }
            return products;
        }

        public List<ProductDTO> GetMostPopularProducts(Langs l, Currency c, WebSites w)
        {
            List<long> productIds = unitOfWork.ProductRepository.Get(cc => true).AsEnumerable()
                .Where(cc => (long)_categoryService.getWebsite(cc.CategoryId) == (long)w)
                .OrderByDescending(cc => cc.DateAdded).Select(cc => cc.Id).ToList();
            Random random = new Random();
            int seed = random.Next();
            productIds = productIds.OrderBy(s => (~(s & seed)) & (s | seed)).ToList();
            List<ProductDTO> products = new List<ProductDTO>();
            for (int i = 0; i < Math.Min(productIds.Count, 8); i++)
            {
                products.Add(GetProduct(productIds[i], l, c));
            }
            return products;
        }
        //.........................
        public void Dispose()
        {
            unitOfWork.Dispose();
        }
        //..........................

    }
}
