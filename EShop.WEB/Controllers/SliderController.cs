using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using EShop.WEB.Models;
using AutoMapper;
using EShop.BLL.DTO;

namespace EShop.WEB.Controllers
{
    public class SliderController : BaseController
    {

        private ISliderService _sliderService;

        public SliderController(ISliderService _sliderService)
        {
            this._sliderService = _sliderService;
        }
        //
        // GET: /Slider/
        public ActionResult Index(int page = 1, int PageSize = PAGE_SIZE)
        {
            var slidesDtos = _sliderService.GetSlidesByWebSite(CurrentWebsite);
            ManageSlidesVM manageSlidesVM = new ManageSlidesVM();

            Mapper.Initialize(c => c.CreateMap<SlideDTO, SlideVM>());
            List<SlideVM> slides = Mapper.Map<List<SlideDTO>, List<SlideVM>>(slidesDtos);
            manageSlidesVM.filters = new SlideFilter
            {
                PageNum = page,
                PageSize = PageSize,
            };

            manageSlidesVM.Slides = slides.ToPagedList(page, PageSize);
            return View(manageSlidesVM);
        }

        public ActionResult Add()
        {
            var vm = new SlideVM();
            vm.WebSite = CurrentWebsite;

            return View(vm);
        }

        [HttpPost]
        public ActionResult Add(SlideVM vm)
        {

            if (ModelState.IsValid)
            {
                Mapper.Initialize(c => c.CreateMap<SlideVM, SlideDTO>());
                var slideDto = Mapper.Map<SlideVM, SlideDTO>(vm);
               OperationDetails op = _sliderService.AddSlide(slideDto);
               return Json(new { Succedeed = op.Succedeed, message = op.Message });
            }
            return View(vm);
        }

        public ActionResult Edit(long Id)
        {
            var slideDTO = _sliderService.GetSlideById(Id);
            Mapper.Initialize(c => c.CreateMap<SlideDTO, SlideVM>());
            var slideVM = Mapper.Map<SlideDTO, SlideVM>(slideDTO);
            slideVM.WebSite = CurrentWebsite;
            return View(slideVM);
        }

        [HttpPost]
        public ActionResult Edit(SlideVM vm)
        {

            if (ModelState.IsValid)
            {
                Mapper.Initialize(c => c.CreateMap<SlideVM, SlideDTO>());
                var slideDto = Mapper.Map<SlideVM, SlideDTO>(vm);
                OperationDetails op = _sliderService.EditSlide(slideDto);
                return Json(new { Succedeed = op.Succedeed, message = op.Message });
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult Delete(long Id)
        {
            OperationDetails op = _sliderService.DeleteSlide(Id);
            return Json(new { Succedeed = op.Succedeed, message = op.Message });
        }
    }
}