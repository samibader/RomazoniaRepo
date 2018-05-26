using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.BLL.DTO;
using EShop.BLL.Interfaces;
using EShop.Common;
using EShop.DAL.Interfaces;
using EShop.BLL.Infrastructure;
using EShop.DAL.Entities;

namespace EShop.BLL.Services
{
    public class DesignerFilterService : IDesignerFilterService
    {
        private IUnitOfWork unitOfWork { get; set; }
        private IProductFilterService _productFilterService;
        private ICategoryService _categoryService;
        public DesignerFilterService(IUnitOfWork uow, IProductFilterService productFilterService,ICategoryService catService)
        {
            unitOfWork = uow;
            _productFilterService = productFilterService;
            _categoryService=catService;
        }
        public OperationDetails DeleteDesigner(long designerID)
        {
            Designer designer = unitOfWork.DesignerRepository.GetByID(designerID);
            if (designer == null)
                return new OperationDetails(false, "المصمم غير موجود", "");
            if(!designer.Products.Any())
                return new OperationDetails(false, "لا يمكن حذف المصمم لوجود منتجات مرتبطة", "");

            if (designer.Descriptions.Any())
            {
                List<DesignerDescription> descs = unitOfWork.DesignerDescriptionRepository.Get(c => c.DesignerId == designerID).ToList();
                foreach(var desc in descs)
                {
                    unitOfWork.DesignerDescriptionRepository.Delete(desc);
                    unitOfWork.Save();
                }
            }
            unitOfWork.DesignerRepository.Delete(designer);
            unitOfWork.Save();
            return new OperationDetails(true, "تمت عملية الحذف", "");

        }
        private WebSites getWebsite(long designerId)
        {
            long categoryId = unitOfWork.DesignerRepository.GetByID(designerId).Products.FirstOrDefault().CategoryId;
            return _categoryService.getWebsite(categoryId);
        }
        public List<DesignerDTO> GetAllDesigners(WebSites w)
        {
            List<DesignerDTO> designerDtos = new List<DesignerDTO>();
            var designers = unitOfWork.DesignerRepository.Get(c => c.Products.Any()).AsEnumerable().Where(c=>(long)getWebsite(c.Id)==(long)w).ToList();
            foreach (var designer in designers)
            {
                designerDtos.Add(GetDesigner(designer.Descriptions.FirstOrDefault(d=>d.LanguageId==(long)Langs.English).Text, Langs.English));
            }
            return designerDtos;
        }

        public DesignerDTO GetDesigner(string designerName,Langs l)
        {
            long designerId = unitOfWork.DesignerDescriptionRepository
                .Get(c => c.LanguageId == (long)Langs.English && c.Text == designerName).Select(c => c.DesignerId).FirstOrDefault();
            var designer = unitOfWork.DesignerRepository.GetByID(designerId);
            var dto = new DesignerDTO()
            {
                DesignerId = designer.Id,
                DesignerName = designer.Descriptions.FirstOrDefault(c=>c.LanguageId==(long)l).Text,
                ImageUrl = designer.ImageUrl??DefaultImages.Designer,
                ImageThumb = designer.ImageThumb ?? DefaultImages.Designer
            };
            return dto;
        }

        public FilterMenuDTO FilterFilters(String designerName, String[] subCategories, String[] colorNames,
            String[] sizeNames, String[] tags, double lowPrice, double highPrice, Sorts s, Currency currency, Langs l)
        {
            return _productFilterService.FilterFilters("", new[] {designerName}, subCategories, colorNames, sizeNames,
                tags, lowPrice, highPrice, s, currency, l);
        }

    }
}
