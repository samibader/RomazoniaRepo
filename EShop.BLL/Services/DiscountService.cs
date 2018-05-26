using AutoMapper;
using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using EShop.Common;
using EShop.DAL.Entities;
using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Services
{
    public class DiscountService : IDiscount
    {
        private readonly IUnitOfWork _uitOfWork;
        private readonly IProductService _productService;

        public DiscountService(IUnitOfWork _uitOfWork, IProductService _productService)
        {
            this._uitOfWork = _uitOfWork;
            this._productService = _productService;
        }

        public OperationDetails AddDiscount(DiscountDTO dto)
        {
            Discount discount = new Discount();
            discount.DateAdded = DateTime.Now;
            discount.DateModefied = DateTime.Now;

            if (!String.IsNullOrWhiteSpace(dto.StartDate))
                discount.DateStart = DateTime.ParseExact(dto.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            else
                discount.DateStart = DateTime.Now;

            if (!String.IsNullOrWhiteSpace(dto.EndDate))
                discount.DateEnd = DateTime.ParseExact(dto.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            else
                discount.DateEnd = DateTime.Now;

            discount.IsPercentage = dto.IsPercentage;
            discount.Value = dto.Value;

            long arabicLang = (long)Langs.Arabic;
            long englishLang = (long)Langs.English;

            discount.Descriptions = new List<DiscountDescription>
            {
                new DiscountDescription
                {
                    DateAdded = DateTime.Now,
                    DateModefied = DateTime.Now,
                    LanguageId = arabicLang,
                    Name = dto.ArabicName
                },
                new DiscountDescription
                {
                    DateAdded = DateTime.Now,
                    DateModefied = DateTime.Now,
                    LanguageId = englishLang,
                    Name = dto.EnglishName
                }
            };

            discount = _uitOfWork.DiscountRepository.Insert(discount);
            _uitOfWork.Save();

            return new OperationDetails(true, "تمت عملية الإضافة بنجاح", discount.Id.ToString());

        }

        public List<ProductDTO> GetProductSKUs(long ProductId, Langs l, Currency cu)
        {
            List<long> ids = _uitOfWork.ProductSKURepository.Get(c => c.ProductId == ProductId).Select(g => g.SKUId).ToList();
            List<ProductDTO> skus = new List<ProductDTO>();
            foreach (var id in ids)
            {
                ProductDTO sku = _productService.GetProductSKU(id, l, cu);
                skus.Add(sku);
            }
            return skus;
        }

        public OperationDetails AddDiscountToSkus(List<long> SkusIds, long discountId)
        {

            if (SkusIds == null || SkusIds.Count == 0)
            {
                return new OperationDetails(false, "الرجاء اختيار منتجات ليتم تطبيق التخفيض عليها", "");

            }
            foreach (var id in SkusIds)
            {
                if (IsSkuInThisDiscount(id,discountId))
                    return new OperationDetails(false, "أحد المنتجات المختارة موجود مسبقاً ضمن التخفيض لذلك لم تتم إضافته مرة أخرى", "");

                if (IsSKuInDiscount(id))
                    return new OperationDetails(false, "أحد المنتجات موجود ضمن تخفيض جاري،يرجى إالغاء التخفيض الجاري عن المنتج أو إالغاء المنتج من التخخفيض الجديد", "");

                ProductDiscount productDiscount = new ProductDiscount();
                long prodictId = _uitOfWork.ProductSKURepository.Get(c => c.SKUId == id).Select(g => g.ProductId).FirstOrDefault();
                productDiscount.ProductId = prodictId;
                productDiscount.SKUId = id;
                productDiscount.DiscountId = discountId;
                productDiscount.DateAdded = DateTime.Now;
                productDiscount.DateModefied = DateTime.Now;
                _uitOfWork.ProductDiscountRepository.Insert(productDiscount);
                _uitOfWork.Save();
            }

            return new OperationDetails(true, "تمت عملية الإضافة بنجاح", "");

        }
        private bool IsSkuInThisDiscount(long skuId,long discountId)
        {
           var skuDiscount = _uitOfWork.ProductDiscountRepository.Get(c => c.SKUId == skuId && c.DiscountId == discountId).ToList();
           if (skuDiscount.Any())
               return true;
           return false;
        }
        private bool IsSKuInDiscount(long skuId)
        {
            //bool result = false;
            ProductSKU sku = _uitOfWork.ProductSKURepository.Get(c => c.SKUId == skuId).FirstOrDefault();
            if (sku.ProductSkuDiscounts != null && sku.ProductSkuDiscounts.Count > 0)
            {
                List<ProductDiscount> discounts = sku.ProductSkuDiscounts.ToList();
                foreach (var discount in discounts)
                {
                    if (discount.Discount.DateEnd > DateTime.Now)
                        return true;
                }
            }

            return false;
        }

        public List<ProductSKUDTO> GetDiscountSkus(long Id, Langs l, Currency cu)
        {
            var discount = _uitOfWork.DiscountRepository.GetByID(Id);
            var discountSkus = discount.DiscountProducts.Select(g => g.ProductSKU).ToList();

            var skusDtos = new List<ProductSKUDTO>();
            foreach (var sku in discountSkus)
            {
                var skuDto = new ProductSKUDTO();
                ProductDTO dto = _productService.GetProductSKU(sku.SKUId, l, cu);
                Mapper.Initialize(c => c.CreateMap<ProductDTO, ProductSKUDTO>());
                skuDto = Mapper.Map<ProductDTO, ProductSKUDTO>(dto);
                skuDto.Id = sku.SKUId;
                if (discount.IsPercentage)
                {
                    skuDto.DiscountPrice = skuDto.OrginalPrice - (skuDto.OrginalPrice * (discount.Value * 0.01));
                }
                else
                {
                    skuDto.DiscountPrice = skuDto.OrginalPrice - discount.Value;
                }
                skusDtos.Add(skuDto);
            }
            return skusDtos;
        }

        public DiscountDTO GetDiscount(long Id, Langs l, Currency cu)
        {
            var discount = _uitOfWork.DiscountRepository.GetByID(Id);
            var discountDTO = new DiscountDTO();

            discountDTO.ArabicName = discount.Descriptions.Where(c => c.LanguageId == (long)Langs.Arabic).FirstOrDefault().Name;
            discountDTO.EnglishName = discount.Descriptions.Where(c => c.LanguageId == (long)Langs.English).FirstOrDefault().Name;
            discountDTO.StartDate = discount.DateStart.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            discountDTO.EndDate = discount.DateEnd.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            discountDTO.Id = discount.Id;
            discountDTO.IsPercentage = discount.IsPercentage;
            discountDTO.Value = discount.Value;
            discountDTO.DiscountSkus = GetDiscountSkus(Id, l, cu);

            return discountDTO;
        }

        public OperationDetails EditDiscount(DiscountDTO dto)
        {
            Discount discount = _uitOfWork.DiscountRepository.GetByID(dto.Id);

            discount.DateModefied = DateTime.Now;

            if (!String.IsNullOrWhiteSpace(dto.StartDate))
                discount.DateStart = DateTime.ParseExact(dto.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            else
                discount.DateStart = DateTime.Now;

            if (!String.IsNullOrWhiteSpace(dto.EndDate))
                discount.DateEnd = DateTime.ParseExact(dto.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            else
                discount.DateEnd = DateTime.Now;

            discount.IsPercentage = dto.IsPercentage;
            discount.Value = dto.Value;
            _uitOfWork.DiscountRepository.Update(discount);

            long arabicLang = (long)Langs.Arabic;
            long englishLang = (long)Langs.English;

            DiscountDescription arabicDescreption = discount.Descriptions.Where(c => c.LanguageId == arabicLang).FirstOrDefault();
            DiscountDescription englsihDescreption = discount.Descriptions.Where(c => c.LanguageId == englishLang).FirstOrDefault();


            arabicDescreption.DateModefied = DateTime.Now;
            arabicDescreption.Name = dto.ArabicName;

            englsihDescreption.DateModefied = DateTime.Now;
            englsihDescreption.Name = dto.EnglishName;
            _uitOfWork.DiscountDescriptionRepository.Update(arabicDescreption);
            _uitOfWork.DiscountDescriptionRepository.Update(englsihDescreption);

            _uitOfWork.Save();

            return new OperationDetails(true, "تمت عملية التعديل بنجاح", discount.Id.ToString());

        }

        public OperationDetails DeleteSkuFromDiscount(long skuId, long discountId)
        {
            var discountSku = _uitOfWork.ProductDiscountRepository.Get(c => c.SKUId == skuId && c.DiscountId == discountId).FirstOrDefault();
            if (discountSku == null)
                return new OperationDetails(false, "حدث خطأ بعملية الحذف", "");


            _uitOfWork.ProductDiscountRepository.Delete(discountSku);
            _uitOfWork.Save();

            return new OperationDetails(true, "تمت عملية الحذف بنجاح", "");
        }

        public List<DiscountDTO> Filter(String arabicName, String englishName, bool? IsPercentage, long discountId, Sorts s, Langs l, Currency cu)
        {
            List<long> discountIds = _uitOfWork.DiscountRepository.Get(c => true).Select(c => c.Id).ToList();

            if (!String.IsNullOrEmpty(arabicName))
            {
                List<long> ids = _uitOfWork.DiscountDescriptionRepository.Get(c => c.Name.Contains(arabicName) && c.LanguageId == (long)Langs.Arabic).Select(c => c.DiscountId).ToList();
                discountIds = discountIds.Intersect(ids).ToList();
            }
            if (!String.IsNullOrEmpty(englishName))
            {
                List<long> ids = _uitOfWork.DiscountDescriptionRepository.Get(c => c.Name.Contains(englishName) && c.LanguageId == (long)Langs.English).Select(c => c.DiscountId).ToList();
                discountIds = discountIds.Intersect(ids).ToList();
            }
            if (discountId != null && discountId != 0)
            {
                List<long> id = new List<long>();
                id.Add(discountId);
                discountIds = discountIds.Intersect(id).ToList();
            }

            if (IsPercentage != null)
            {
                List<long> ids = _uitOfWork.DiscountRepository.Get(c => c.IsPercentage==IsPercentage).Select(c => c.Id).ToList();
                discountIds = discountIds.Intersect(ids).ToList();
            }

            List<DiscountDTO> discounts = new List<DiscountDTO>();
            foreach (var id in discountIds)
            {
                DiscountDTO dto = GetDiscount(id, l, cu);
                discounts.Add(dto);
            }

            sortDiscounts(ref discounts, s);
            return discounts;
        }
        private void sortDiscounts(ref List<DiscountDTO> options, Sorts s)
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
                case Sorts.StatusDown: options.Sort((a, b) => -1 * a.IsPercentage.CompareTo(b.IsPercentage));
                    break;
                case Sorts.StatusUp: options.Sort((a, b) => a.IsPercentage.CompareTo(b.IsPercentage));
                    break;
                case Sorts.ValueDown: options.Sort((a, b) => -1 * a.Value.CompareTo(b.Value));
                    break;
                case Sorts.ValueUp: options.Sort((a, b) => a.Value.CompareTo(b.Value));
                    break;
            }

        }

        public OperationDetails DeleteDiscount(long Id)
        {
            var discount = _uitOfWork.DiscountRepository.GetByID(Id);
            if (discount == null)
                return new OperationDetails(false, "حدث خطأ بعملية الحذف", "");

            var discountSkus = discount.DiscountProducts.ToList();
            if (discountSkus.Count > 0)
                return new OperationDetails(false, "يرجى إلغاء التخفيض عن جميع المنتجات المرتبطة قبل القيام بحذف التخفيض", "");

            DiscountDescription arabicDesc = discount.Descriptions.Where(c => c.LanguageId == (long)Langs.Arabic).FirstOrDefault();
            DiscountDescription englishDesc = discount.Descriptions.Where(c => c.LanguageId == (long)Langs.English).FirstOrDefault();

            _uitOfWork.DiscountDescriptionRepository.Delete(arabicDesc);
            _uitOfWork.DiscountDescriptionRepository.Delete(englishDesc);

            _uitOfWork.DiscountRepository.Delete(discount);
            _uitOfWork.Save();

            return new OperationDetails(true, "تمت عملية الحذف بنجاح", "");
        }
    }
}
