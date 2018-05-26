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
    public class SliderService : ISliderService
    {
        IUnitOfWork _unitOfWork { get; set; }

        public SliderService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
            
        }

        public OperationDetails AddSlide(SlideDTO dto)
        {
            Slide slide = new Slide();
            slide.ImageUrl = dto.ImageUrl;
            slide.Url = dto.Url;
            slide.WebSite = (long)dto.WebSite;

            _unitOfWork.SlideRepository.Insert(slide);
            _unitOfWork.Save();

            return new OperationDetails(true,"تمت عملية الإضافة بنجاح","");
        }

        public OperationDetails EditSlide(SlideDTO dto)
        {
            var slide = _unitOfWork.SlideRepository.GetByID(dto.Id);

            if(slide == null)
                return new OperationDetails(false, "حدث خطأ أثناء عملية التعديل", "");


            slide.ImageUrl = dto.ImageUrl;
            slide.Url = dto.Url;
            slide.WebSite = (long)dto.WebSite;

            _unitOfWork.SlideRepository.Update(slide);
            _unitOfWork.Save();

            return new OperationDetails(true, "تمت عملية التعديل بنجاح", "");
        }

        public OperationDetails DeleteSlide(long Id)
        {
            var slide = _unitOfWork.SlideRepository.GetByID(Id);

            if (slide == null)
                return new OperationDetails(false, "هذا السلايد غير موجود", "");

            _unitOfWork.SlideRepository.Delete(slide);
            _unitOfWork.Save();

            return new OperationDetails(true, "تمت عملية الحذف بنجاح", "");
        }

        public List<SlideDTO> GetSlidesByWebSite(WebSites website)
        {
            var slides = _unitOfWork.SlideRepository.Get(c => c.WebSite == (long)website).ToList();

            var slidesDtos = new List<SlideDTO>();

            foreach (var slide in slides)
            {
                var slideDto = new SlideDTO();
                slideDto.Id = slide.Id;
                slideDto.ImageUrl = slide.ImageUrl;
                slideDto.Url = slide.Url;
                slideDto.WebSite = website;
                slidesDtos.Add(slideDto);
            }

            return slidesDtos;
        }

         public SlideDTO GetSlideById(long Id)
        {
            var slide = _unitOfWork.SlideRepository.GetByID(Id);
            var dto = new SlideDTO();
            dto.Id = slide.Id;
            dto.Url = slide.Url;
            dto.ImageUrl = slide.ImageUrl;
            return dto;
        }
    }
}
