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
    public class ManageSizeAttributes : IManageSizeAttributes
    {
        private IUnitOfWork _unitOfWork;
        private IProductManagerService _productManagerService;
        public ManageSizeAttributes(IUnitOfWork _unitOfWork, IProductManagerService _productManagerService)
        {
            this._unitOfWork = _unitOfWork;
            this._productManagerService = _productManagerService;
        }

        public OperationDetails DeleteSizeAttribute(long Id)
        {
            var sizeAttribute = _unitOfWork.SizeAttributeRepository.GetByID(Id);
            if (sizeAttribute == null)
                return new OperationDetails(false, "هذه الواصفة غير موجودة", "");

            var sizeAttributeProducts = _unitOfWork.ProductSizeAttributeRepository.Get(c => c.SizeAttributeId == Id).ToList();

            if(sizeAttributeProducts.Any())
                return new OperationDetails(false, "لايمكن حذف الواصفة لوجود منتجات مرتبطة بها", "");


            var descriptions = _unitOfWork.SizeAttributeDescriptionRepository.Get(c => c.SizeAttributeId == Id).ToList();
            foreach (var desc in descriptions)
            {
                _unitOfWork.SizeAttributeDescriptionRepository.Delete(desc);
                _unitOfWork.Save();
            }
            _unitOfWork.SizeAttributeRepository.Delete(sizeAttribute);
            _unitOfWork.Save();
            return new OperationDetails(true, "تمت عملية الحذف بنجاح", "");
        }
        public OperationDetails AddSizeAttribute(string arabicName, string englishName, long SizeCategoryId)
        {
            SizeAttribute option = new SizeAttribute();
            option.DateAdded = DateTime.Now;
            option.DateModefied = DateTime.Now;
            OptionHelper sizeCat = _unitOfWork.OptionHelperRepository.Get(c => c.Id == SizeCategoryId).FirstOrDefault();
            option.SizeCatId = SizeCategoryId;

            option = _unitOfWork.SizeAttributeRepository.Insert(option);

            SizeAttributeDescription arabicDesciption = new SizeAttributeDescription();
            SizeAttributeDescription englishDesciption = new SizeAttributeDescription();

            arabicDesciption.Name = arabicName;
            arabicDesciption.SizeAttributeId = option.Id;
            arabicDesciption.DateAdded = DateTime.Now;
            arabicDesciption.DateModefied = DateTime.Now;
            arabicDesciption.LanguageId = (long)Langs.Arabic;

            englishDesciption.Name = englishName;
            englishDesciption.SizeAttributeId = option.Id;
            englishDesciption.DateAdded = DateTime.Now;
            englishDesciption.DateModefied = DateTime.Now;
            englishDesciption.LanguageId = (long)Langs.English;

            _unitOfWork.SizeAttributeDescriptionRepository.Insert(arabicDesciption);
            _unitOfWork.SizeAttributeDescriptionRepository.Insert(englishDesciption);

            _unitOfWork.Save();
            return new OperationDetails(true, "تمت الإضافة بنجاح", option.Id.ToString());
        }

        //public OperationDetails AddSizeAttributeValue(SizeAttributeValueDTO sizeAttributeValue)
        //{
        //    SizeAttributeMedian median = new SizeAttributeMedian();
        //    median.SizeAttributeId = sizeAttributeValue.SizeAttributeId;
        //    median.SizeValueId = sizeAttributeValue.SizeValueId;
        //    median.Value = sizeAttributeValue.Value;
        //    median.DateAdded = DateTime.Now;
        //    median.DateModefied = DateTime.Now;
        //    _unitOfWork.SizeAttributeMedianRepository.Insert(median);
        //    _unitOfWork.Save();
        //    return new OperationDetails(true, "تمت إضافة القيمة بنجاح", median.Id.ToString());


        //}

        public AddSizeAttributeDTO GetAllSizesToAddSizeAttribute()
        {
            AddSizeAttributeDTO sizeAttributeDto = new AddSizeAttributeDTO();
            List<OptionManagerDTO> allSizeOptions = _productManagerService.GetAllSizeOptions();
            sizeAttributeDto.SizesCategories = allSizeOptions;
            sizeAttributeDto.SizeAttributeValues = new List<SizeAttributeValueDTO>();
            return sizeAttributeDto;
        }

        //public List<SizeAttributeValueDTO> GetSizeAttributeValuesBySizeAttributeId(long Id)
        //{
        //    List<SizeAttributeValueDTO> dtos = new List<SizeAttributeValueDTO>();

        //    List<SizeAttributeMedian> sizeAttributesValues = _unitOfWork.SizeAttributeMedianRepository.Get(c => c.SizeAttributeId == Id).ToList();
        //    foreach (var AttributeValue in sizeAttributesValues)
        //    {
        //        SizeAttributeValueDTO dto = new SizeAttributeValueDTO();
        //        dto.SizeAttributeId = AttributeValue.SizeAttributeId;
        //        dto.Id = AttributeValue.Id;
        //        dto.DateModified = AttributeValue.DateModefied ?? AttributeValue.DateModefied.Value;
        //        dto.SizeValueId = AttributeValue.SizeValueId;

        //        dto.SizeValueName = _unitOfWork.SizeHelperRepository.Get(c => c.Id == AttributeValue.SizeValueId).Select(g => g.ArabicName).FirstOrDefault();

        //        dto.SizeCategoryName = _unitOfWork.SizeHelperRepository.Get(c => c.Id == AttributeValue.SizeValueId).Select(g => g.ArabicSizeCategoryName).FirstOrDefault();
        //        dto.Value = AttributeValue.Value;

        //        dtos.Add(dto);
        //    }
        //    return dtos;
        //}

        public AddSizeAttributeDTO GetSizeAttributeById(long Id)
        {
            AddSizeAttributeDTO addDTO = new AddSizeAttributeDTO();

            SizeAttribute sizeAttribute = _unitOfWork.SizeAttributeRepository.Get(c => c.Id == Id).FirstOrDefault();

            long arabicLang = (long)Langs.Arabic;
            long englishLang = (long)Langs.English;

            addDTO.SizeAttribute = new SizeAttributeDTO
            {
                ArabicName = _unitOfWork.SizeAttributeDescriptionRepository.Get(c => c.SizeAttributeId == Id && c.LanguageId == arabicLang).Select(g => g.Name).FirstOrDefault(),
                EnglishName = _unitOfWork.SizeAttributeDescriptionRepository.Get(c => c.SizeAttributeId == Id && c.LanguageId == englishLang).Select(g => g.Name).FirstOrDefault(),
                Id = Id,
                SizeCategoryId = sizeAttribute.SizeCatId,
                DateModified = sizeAttribute.DateModefied ?? sizeAttribute.DateModefied.Value
            };
            //addDTO.SizeAttributeValues = GetSizeAttributeValuesBySizeAttributeId(Id);
            addDTO.SizeAttributeValues = new List<SizeAttributeValueDTO>();
            addDTO.SizesCategories = _productManagerService.GetAllSizeOptions();

            return addDTO;
        }


        public OperationDetails EditSizeAttribute(string arabicName, string englishName, long Id, long SizeCatId)
        {
            SizeAttribute option = _unitOfWork.SizeAttributeRepository.Get(c => c.Id == Id).FirstOrDefault();
            option.SizeCatId = SizeCatId;
            option.DateModefied = DateTime.Now;
            _unitOfWork.SizeAttributeRepository.Update(option);

            long arabicLang = (long)Langs.Arabic;
            long englishLang = (long)Langs.English;


            SizeAttributeDescription arabicDesciption = option.SizeAttributeDescriptions.Where(c => c.LanguageId == arabicLang).FirstOrDefault();
            SizeAttributeDescription englishDesciption = option.SizeAttributeDescriptions.Where(c => c.LanguageId == englishLang).FirstOrDefault();

            arabicDesciption.Name = arabicName;
            arabicDesciption.DateModefied = DateTime.Now;


            englishDesciption.Name = englishName;
            englishDesciption.DateModefied = DateTime.Now;


            _unitOfWork.SizeAttributeDescriptionRepository.Update(arabicDesciption);
            _unitOfWork.SizeAttributeDescriptionRepository.Update(englishDesciption);

            _unitOfWork.Save();
            return new OperationDetails(true, "تم التعديل بنجاح", option.Id.ToString());
        }

        //public OperationDetails EditSizeAttributeValue(SizeAttributeValueDTO sizeAttributeValue)
        //{
        //    SizeAttributeMedian median = _unitOfWork.SizeAttributeMedianRepository.Get(c => c.Id == sizeAttributeValue.Id).FirstOrDefault();

        //    median.Value = sizeAttributeValue.Value;
        //    median.DateModefied = DateTime.Now;
        //    _unitOfWork.SizeAttributeMedianRepository.Update(median);
        //    _unitOfWork.Save();
        //    return new OperationDetails(true, "تم التعديل بنجاح", median.Id.ToString());


        //}

        public List<SizeAttributeDTO> Filter(String arabicName, String englishName, long optionId, Sorts s)
        {
            List<long> attributeIds = _unitOfWork.SizeAttributeRepository.Get(c => true).Select(c => c.Id).ToList();

            long arabicLang = (long)Langs.Arabic;
            long englishLang = (long)Langs.English;


            if (!String.IsNullOrEmpty(arabicName))
            {
                List<long> ids = _unitOfWork.SizeAttributeDescriptionRepository.Get(c => c.Name.ToLower().Contains(arabicName.ToLower()) && c.LanguageId == arabicLang).Select(c => c.SizeAttributeId).ToList();
                attributeIds = attributeIds.Intersect(ids).ToList();
            }
            if (!String.IsNullOrEmpty(englishName))
            {
                List<long> ids = _unitOfWork.SizeAttributeDescriptionRepository.Get(c => c.Name.ToLower().Contains(englishName.ToLower()) && c.LanguageId == englishLang).Select(c => c.SizeAttributeId).ToList();
                attributeIds = attributeIds.Intersect(ids).ToList();
            }
            if (optionId != null && optionId != 0)
            {
                List<long> id = new List<long>();
                id.Add(optionId);
                attributeIds = attributeIds.Intersect(id).ToList();
            }

            List<SizeAttributeDTO> sizeAttributes = new List<SizeAttributeDTO>();
            foreach (var id in attributeIds)
            {
                sizeAttributes.Add(GetSizeAttributeById(id).SizeAttribute);
            }

            sortSizeAttributes(ref sizeAttributes, s);
            return sizeAttributes;
        }
        private void sortSizeAttributes(ref List<SizeAttributeDTO> options, Sorts s)
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

        public List<SizeAttributeDTO> GetAllSizeAttributes(Langs l)
        {
            List<SizeAttribute> sizeAttributes = _unitOfWork.SizeAttributeRepository.Get(c => true).ToList();
            List<SizeAttributeDTO> sizeAttributesdtos = new List<SizeAttributeDTO>();
            foreach (var sizeAttribute in sizeAttributes)
            {
                SizeAttributeDTO dto = new SizeAttributeDTO();
                long lang = Utils.getLanguage(l);
                dto.Name = sizeAttribute.SizeAttributeDescriptions.Where(c => c.LanguageId == lang).Select(g => g.Name).FirstOrDefault();
                dto.SizeCategoryId = sizeAttribute.SizeCatId;
                dto.Id = sizeAttribute.Id;
                sizeAttributesdtos.Add(dto);
            }
            return sizeAttributesdtos;
        }

        public List<SizeAttributeDTO> GetSizeAttributesBySizeCaategoryId(long SizecatId, Langs l)
        {
            List<SizeAttribute> sizeAttributes = _unitOfWork.SizeAttributeRepository.Get(c => c.SizeCatId == SizecatId).ToList();
            List<SizeAttributeDTO> sizeAttributesdtos = new List<SizeAttributeDTO>();
            foreach (var sizeAttribute in sizeAttributes)
            {
                SizeAttributeDTO dto = new SizeAttributeDTO();
                long lang = Utils.getLanguage(l);
                dto.Name = sizeAttribute.SizeAttributeDescriptions.Where(c => c.LanguageId == lang).Select(g => g.Name).FirstOrDefault();
                dto.SizeCategoryId = sizeAttribute.SizeCatId;
                dto.Id = sizeAttribute.Id;
                sizeAttributesdtos.Add(dto);
            }
            return sizeAttributesdtos;

        }
    }
}
