using AutoMapper;
using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using EShop.BLL.Services;
using EShop.Common;
using EShop.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace EShop.WEB.Controllers
{
    [Authorize(Roles = "AdminRole")]
    public class ManageOptionController : BaseController
    {
        private IProductManagerService _productManagerService;
        public ManageOptionController(IProductManagerService productManagerService)
        {
            _productManagerService = productManagerService;
        }
        //
        // GET: /ManageOption/
        public ActionResult Index(String an, String en, int PageSize = PAGE_SIZE, int page = 1, Sorts sb = Sorts.IdUp, Int32 Id = 0)
        {
           //List<OptionManagerDTO> optionsDTOs = _productManagerService.GetAllOptions();
            List<OptionManagerDTO> optionsDTOs = _productManagerService.Filter(an, en, Id, sb);
            OptionIndexManagerVM options = new OptionIndexManagerVM
            {
                Options = optionsDTOs.ToPagedList(page, PageSize),
                filters = new OptionFiltersDTO
                {
                    ArabicName = an,
                    EnglishName = en,
                    PageNum = page,
                    PageSize = PageSize,
                    Id = Id,
                    SortBy = sb
                }
            };
           /* Mapper.Initialize(c => c.CreateMap<OptionValueManagerDTO, OptionValueManagerVM>());
            Mapper.Initialize(c => c.CreateMap<OptionManagerDTO, OptionManagerVM>().ForMember(dest => dest.OptionValues, opt =>
           opt.MapFrom(src => Mapper.Map<List<OptionValueManagerDTO>, List<OptionValueManagerVM>>(src.OptionValues))));

            List<OptionManagerVM> options = Mapper.Map<List<OptionManagerDTO>, List<OptionManagerVM>>(optionsDTOs);*/

            return View(options);
        }

        [HttpPost]
        public ActionResult Index(String en, String an, int PageSize = PAGE_SIZE, Int32 Id = 0, int page = 1, Sorts sb = Sorts.IdUp)
        {
            //List<OptionManagerDTO> optionsDTOs = _productManagerService.GetAllOptions();
            List<OptionManagerDTO> optionsDTOs = _productManagerService.Filter(an, en, Id, sb);
            OptionIndexManagerVM options = new OptionIndexManagerVM
            {
                Options = optionsDTOs.ToPagedList(page, PageSize),
                filters = new OptionFiltersDTO
                {
                    ArabicName = an,
                    EnglishName = en,
                    PageNum = page,
                    PageSize = PageSize,
                    Id = Id,
                    SortBy = sb
                }
            };
            /* Mapper.Initialize(c => c.CreateMap<OptionValueManagerDTO, OptionValueManagerVM>());
             Mapper.Initialize(c => c.CreateMap<OptionManagerDTO, OptionManagerVM>().ForMember(dest => dest.OptionValues, opt =>
            opt.MapFrom(src => Mapper.Map<List<OptionValueManagerDTO>, List<OptionValueManagerVM>>(src.OptionValues))));

             List<OptionManagerVM> options = Mapper.Map<List<OptionManagerDTO>, List<OptionManagerVM>>(optionsDTOs);*/

            return View(options);
        }
        public ActionResult Add()
        {
            OptionManagerVM option = new OptionManagerVM();
            return View(option);
        }

        [HttpPost]
        public ActionResult Add(OptionManagerVM option)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(c => c.CreateMap<OptionManagerVM, OptionManagerDTO>());
                OptionManagerDTO optionDto = Mapper.Map<OptionManagerVM, OptionManagerDTO>(option);
                OperationDetails op = _productManagerService.AddOption(option.ArabicName, option.EnglishName);

                return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });
            }
            else
                return View(option);
        }

        [HttpPost]
        public ActionResult AddOptionValue(OptionValueManagerVM optionValue)
        {
            Mapper.Initialize(c => c.CreateMap<OptionValueManagerVM, OptionValueManagerDTO>());
            OptionValueManagerDTO optionValueDto = Mapper.Map<OptionValueManagerVM, OptionValueManagerDTO>(optionValue);

           OperationDetails op =_productManagerService.AddOptionValue(optionValueDto);

            List<OptionValueManagerDTO> resultDtos = _productManagerService.GetOptionValuesByOptionId(optionValue.OptionId);

            Mapper.Initialize(c => c.CreateMap<OptionValueManagerDTO, OptionValueManagerVM>());

            List<OptionValueManagerVM> optionValueVMs = Mapper.Map<List<OptionValueManagerDTO>, List<OptionValueManagerVM>>(resultDtos);

            return PartialView("_AddOptionValue", optionValueVMs);
        }

        public ActionResult Edit(long Id)
        {
            OptionManagerDTO optionDto = _productManagerService.GetOptionById(Id);
            Mapper.Initialize(c => c.CreateMap<OptionManagerDTO, OptionManagerVM>());
            OptionManagerVM option = Mapper.Map<OptionManagerDTO, OptionManagerVM>(optionDto);
            return View(option);
        }

        [HttpPost]
        public ActionResult Edit(OptionManagerVM option)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(c => c.CreateMap<OptionManagerVM, OptionManagerDTO>());
                OptionManagerDTO optionDto = Mapper.Map<OptionManagerVM, OptionManagerDTO>(option);
                OperationDetails op = _productManagerService.EditOption(optionDto);

                return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });
            }
            else
                return View(option);
        }

        [HttpPost]
        public ActionResult EditOptionValue(OptionValueManagerVM optionValue)
        {

            Mapper.Initialize(c => c.CreateMap<OptionValueManagerVM, OptionValueManagerDTO>());

            OptionValueManagerDTO optionValueDto = Mapper.Map<OptionValueManagerVM, OptionValueManagerDTO>(optionValue);

            OperationDetails op =  _productManagerService.EditOptionValue(optionValueDto);

            return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });
           
        }
        public ActionResult Delete(long Id)
        {
            OperationDetails op = _productManagerService.DeleteOption(Id);
            return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });
        }
        [HttpPost]
        public ActionResult DeleteOptionValue(long Id,long optionValueId)
        {
            OperationDetails op = _productManagerService.DeleteOptionValue(Id,optionValueId);
            return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });
        }
	}
}