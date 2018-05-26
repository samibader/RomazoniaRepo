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
    public class ManageDesignerService : IManageDesigner
    {
        private IUnitOfWork _unitOfWork;
        public ManageDesignerService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public OperationDetails AddDesigner(ManageDesignerDTO dto)
        {
            Designer designer = new Designer();
            designer.ImageUrl = dto.ImageUrl;
            designer.DateAdded = DateTime.Now;
            designer.DateModefied = DateTime.Now;
            designer = _unitOfWork.DesignerRepository.Insert(designer);

            DesignerDescription arabicDescription = new DesignerDescription();
            DesignerDescription englishDescription = new DesignerDescription();

            arabicDescription.DesignerId = designer.Id;
            arabicDescription.Text = dto.ArabicName;
            arabicDescription.LanguageId = (long)Langs.Arabic;
            arabicDescription.DateAdded = DateTime.Now;
            arabicDescription.DateModefied = DateTime.Now;

            englishDescription.DesignerId = designer.Id;
            englishDescription.Text = dto.EnglishName;
            englishDescription.LanguageId = (long)Langs.English;
            englishDescription.DateAdded = DateTime.Now;
            englishDescription.DateModefied = DateTime.Now;
            _unitOfWork.DesignerDescriptionRepository.Insert(arabicDescription);
            _unitOfWork.DesignerDescriptionRepository.Insert(englishDescription);

            _unitOfWork.Save();
            return new OperationDetails(true, "تمت إضافة المصمم بنجاح", "");
        }

        public OperationDetails EditDesigner(ManageDesignerDTO dto)
        {
            Designer designer = _unitOfWork.DesignerRepository.Get(c => c.Id == dto.Id).FirstOrDefault();
            designer.ImageUrl = dto.ImageUrl;
            designer.DateAdded = DateTime.Now;
            designer.DateModefied = DateTime.Now;
            _unitOfWork.DesignerRepository.Update(designer);

            long arabicLang = (long)Langs.Arabic;
            long englishLang = (long)Langs.English;

            DesignerDescription arabicDescription = _unitOfWork.DesignerDescriptionRepository.Get(c => c.DesignerId == dto.Id && c.LanguageId == arabicLang).FirstOrDefault();

            DesignerDescription englishDescription = _unitOfWork.DesignerDescriptionRepository.Get(c => c.DesignerId == dto.Id && c.LanguageId == englishLang).FirstOrDefault();


            arabicDescription.Text = dto.ArabicName;
            arabicDescription.LanguageId = (long)Langs.Arabic;
            arabicDescription.DateModefied = DateTime.Now;


            englishDescription.Text = dto.EnglishName;
            englishDescription.DateModefied = DateTime.Now;

            _unitOfWork.DesignerDescriptionRepository.Update(arabicDescription);
            _unitOfWork.DesignerDescriptionRepository.Update(englishDescription);

            _unitOfWork.Save();
            return new OperationDetails(true, "تم تعديل المصمم بنجاح", "");
        }

        public ManageDesignerDTO GetDesignerById(long Id)
        {
            Designer designer = _unitOfWork.DesignerRepository.Get(c => c.Id == Id).FirstOrDefault();

            long arabicLang = (long)Langs.Arabic;
            long englishLang = (long)Langs.English;

            ManageDesignerDTO designerDto = new ManageDesignerDTO();
            designerDto.Id = designer.Id;
            designerDto.ArabicName = designer.Descriptions.Where(c => c.LanguageId == arabicLang).Select(n => n.Text).FirstOrDefault();
            designerDto.EnglishName = designer.Descriptions.Where(c => c.LanguageId == englishLang).Select(n => n.Text).FirstOrDefault();
            designerDto.ImageUrl = designer.ImageUrl;
            return designerDto;

        }

        public List<ManageDesignerDTO> Filter(string arabicName, string englishName, long id, Sorts s)
        {

            List<long> designersIds = _unitOfWork.DesignerRepository.Get(c => true).Select(c => c.Id).ToList();

            if (!String.IsNullOrEmpty(arabicName))
                FilterName(ref designersIds, arabicName, Langs.Arabic);
            if (!String.IsNullOrEmpty(englishName))
                FilterName(ref designersIds, englishName, Langs.English);
            //if (status != null)
            //    FilterStatus(ref categoryIds, status.Value);
            if (id != null && id != 0)
                FilterId(ref designersIds, id);

            List<ManageDesignerDTO> designers = new List<ManageDesignerDTO>();

            for (int i = 0; i < designersIds.Count; i++)
            {
                designers.Add(GetDesignerById(designersIds[i]));
            }
            sortDesigners(ref designers, s);
            return designers;
        }

        private void FilterName(ref List<long> designersIds, string name, Langs l)
        {
            long s = (long)Utils.getLanguage(l);
            List<long> ids = _unitOfWork.DesignerDescriptionRepository.
                                        Get(c => c.Text.Contains(name) && c.LanguageId == s).
                                            Select(op => op.DesignerId).Distinct().ToList();

            designersIds = designersIds.Intersect(ids).ToList();
        }

        private void FilterId(ref List<long> designersIds, long id)
        {
            List<long> ids = _unitOfWork.DesignerDescriptionRepository.
                                        Get(c => c.Id == id).
                                            Select(op => op.Id).Distinct().ToList();
            designersIds = designersIds.Intersect(ids).ToList();
        }

        private void sortDesigners(ref List<ManageDesignerDTO> groups, Sorts s)
        {
            switch (s)
            {
                case Sorts.ArabicNameDown: groups.Sort((a, b) => -1 * a.ArabicName.CompareTo(b.ArabicName));   //products.OrderBy(op => op.Name);
                    break;
                case Sorts.ArabicNameUp: groups.Sort((a, b) => a.ArabicName.CompareTo(b.ArabicName));
                    break;
                case Sorts.EnglishNameDown: groups.Sort((a, b) => -1 * a.EnglishName.CompareTo(b.EnglishName)); //products.OrderBy(op => op.Price);
                    break;
                case Sorts.EnglishNameUp: groups.Sort((a, b) => a.EnglishName.CompareTo(b.EnglishName));
                    break;
                case Sorts.IdDown: groups.Sort((a, b) => -1 * a.Id.CompareTo(b.Id));
                    break;
                case Sorts.IdUp: groups.Sort((a, b) => a.Id.CompareTo(b.Id));
                    break;
                //case Sorts.StatusDown: groups.Sort((a, b) => -1 * a.Status.CompareTo(b.Status));
                //    break;
                //case Sorts.StatusUp: groups.Sort((a, b) => a.Status.CompareTo(b.Status));
                //    break;
            }

        }

        public List<DesignerDTO> GetAllDesigners(Langs l)
        {
            List<Designer> designers = _unitOfWork.DesignerRepository.Get(c => true).ToList();
            List<DesignerDTO> designersDtos = new List<DesignerDTO>();

            long lang = Utils.getLanguage(l);

            foreach (var designer in designers)
            {
                DesignerDTO dto = new DesignerDTO();
                dto.DesignerId = designer.Id;
                dto.DesignerName = designer.Descriptions.Where(c => c.LanguageId == lang).Select(g => g.Text).FirstOrDefault();
                designersDtos.Add(dto);
            }
            return designersDtos;
        }

        public OperationDetails DeleteDesigner(long Id)
        {
            var designer = _unitOfWork.DesignerRepository.GetByID(Id);
            if (designer == null)
                return new OperationDetails(false, "هذا المصمم غير موجود", "");

            var designerProducts = _unitOfWork.ProductRepository.Get(c => c.DesignerId == Id).ToList();

            if (designerProducts.Any())
                return new OperationDetails(false, "لا يمكن حذف هذا المصمم لوجود منتجات مرتبطة به", "");

            var descriptions = _unitOfWork.DesignerDescriptionRepository.Get(c => c.DesignerId == Id).ToList();

            if (descriptions.Any())
            {
                foreach (var desc in descriptions)
                {
                    _unitOfWork.DesignerDescriptionRepository.Delete(desc);
                    _unitOfWork.Save();
                }
            }

            _unitOfWork.DesignerRepository.Delete(designer);
            _unitOfWork.Save();
            return new OperationDetails(true, "تمت عملية الحذف بنجاح", "");

        }
    }
}
