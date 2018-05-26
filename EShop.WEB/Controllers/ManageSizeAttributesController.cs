using AutoMapper;
using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
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
    public class ManageSizeAttributesController : BaseController
    {
        private IManageSizeAttributes _manageSizeAttributeService;
        private IProductManagerService _manageProductService;
        public ManageSizeAttributesController(IManageSizeAttributes _manageSizeAttributeService, IProductManagerService _manageProductService)
        {
            this._manageSizeAttributeService = _manageSizeAttributeService;
            this._manageProductService = _manageProductService;
        }

        //
        // GET: /ManageSizeAttributes/

        public ActionResult Index(String an, String en, int PageSize = PAGE_SIZE, int page = 1, Sorts sb = Sorts.IdUp, long Id = 0)
        {
            List<OptionManagerDTO> optionsDTOs = _manageProductService.GetAllOptions();
            List<SizeAttributeDTO> AttributesDTOs = _manageSizeAttributeService.Filter(an, en, Id, sb);
            //List<SizeAttributeDTO> AttributesDTOs = new List<SizeAttributeDTO>();
            SizeAttributeIndexManagerVM sizeAttributes = new SizeAttributeIndexManagerVM
            {

                SizeAttributes = AttributesDTOs.ToPagedList(page, PageSize),
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

            return View(sizeAttributes);
        }

        [HttpPost]
        public ActionResult Index(String an, String en, int PageSize = PAGE_SIZE,  long Id = 0,int page = 1, Sorts sb = Sorts.IdUp)
        {
            List<OptionManagerDTO> optionsDTOs = _manageProductService.GetAllOptions();
            List<SizeAttributeDTO> AttributesDTOs = _manageSizeAttributeService.Filter(an, en, Id, sb);
           // List<SizeAttributeDTO> AttributesDTOs = new List<SizeAttributeDTO>();
            SizeAttributeIndexManagerVM sizeAttributes = new SizeAttributeIndexManagerVM
            {

                SizeAttributes = AttributesDTOs.ToPagedList(page, PageSize),
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

            return View(sizeAttributes);
        }

        [HttpPost]
        public JsonResult GetSizesForSizeCategory(long Id)
        {
            List<OptionValueManagerDTO> dto = _manageProductService.GetOptionValuesByOptionId(Id);
            SelectList sizes = new SelectList(dto, "Id", "ArabicName", 0);
            return Json(sizes);
        }

        public ActionResult Add()
        {

            AddSizeAttributeDTO addSizeAttributeDto = _manageSizeAttributeService.GetAllSizesToAddSizeAttribute();

            Mapper.Initialize(c => c.CreateMap<AddSizeAttributeDTO, AddSizeAttributeVM>());

            AddSizeAttributeVM vm = Mapper.Map<AddSizeAttributeDTO, AddSizeAttributeVM>(addSizeAttributeDto);
            //AddSizeAttributeVM vm = new AddSizeAttributeVM();
            return View(vm);
            
        }

        [HttpPost]
        public ActionResult Add(string arabicName, string englishName, long SizeCategoryId)
        {
        //    Mapper.Initialize(c => c.CreateMap<SizeAttributeValueVM, SizeAttributeValueDTO>());

        //    SizeAttributeValueDTO addSizeAttributeDto = Mapper.Map<SizeAttributeValueVM, SizeAttributeValueDTO>(sizeAttribute);
            OperationDetails op = _manageSizeAttributeService.AddSizeAttribute(arabicName, englishName, SizeCategoryId);
            return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });
            //return Json(new { Succedeed = "", message = "", prop = "" });
        }

        [HttpPost]
        public ActionResult AddSizeAttributeValue(SizeAttributeValueVM optionValue)
        {
            //Mapper.Initialize(c => c.CreateMap<SizeAttributeValueVM, SizeAttributeValueDTO>());
            //SizeAttributeValueDTO optionValueDto = Mapper.Map<SizeAttributeValueVM, SizeAttributeValueDTO>(optionValue);

            //OperationDetails op = _manageSizeAttributeService.AddSizeAttributeValue(optionValueDto);

            //List<SizeAttributeValueDTO> resultDtos = _manageSizeAttributeService.GetSizeAttributeValuesBySizeAttributeId(optionValue.SizeAttributeId);

            //AddSizeAttributeDTO addSizeAttributeDto = _manageSizeAttributeService.GetAllSizesToAddSizeAttribute();

            //Mapper.Initialize(c => c.CreateMap<AddSizeAttributeDTO, AddSizeAttributeVM>());

            //AddSizeAttributeVM vm = Mapper.Map<AddSizeAttributeDTO, AddSizeAttributeVM>(addSizeAttributeDto);

            //vm.SizeAttributeValues = resultDtos;

            //return PartialView("_AddSizeAttributeValue", vm);
            return View();
        }

        public ActionResult Edit(long Id)
        {

            AddSizeAttributeDTO addSizeAttributeDto = _manageSizeAttributeService.GetSizeAttributeById(Id);

            Mapper.Initialize(c => c.CreateMap<AddSizeAttributeDTO, AddSizeAttributeVM>());

            AddSizeAttributeVM vm = Mapper.Map<AddSizeAttributeDTO, AddSizeAttributeVM>(addSizeAttributeDto);
          
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(string arabicName, string englishName, long Id, long SizeCategoryId)
        {
            //Mapper.Initialize(c => c.CreateMap<SizeAttributeValueVM, SizeAttributeValueDTO>());

            //SizeAttributeValueDTO addSizeAttributeDto = Mapper.Map<SizeAttributeValueVM, SizeAttributeValueDTO>(sizeAttribute);
            OperationDetails op = _manageSizeAttributeService.EditSizeAttribute(arabicName, englishName, Id, SizeCategoryId);
            return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });

        }

        [HttpPost]
        public ActionResult EditSizeAttributeValue(SizeAttributeValueVM optionValue)
        {
            Mapper.Initialize(c => c.CreateMap<SizeAttributeValueVM, SizeAttributeValueDTO>());
            SizeAttributeValueDTO optionValueDto = Mapper.Map<SizeAttributeValueVM, SizeAttributeValueDTO>(optionValue);

            //OperationDetails op = _manageSizeAttributeService.EditSizeAttributeValue(optionValueDto);

            //return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });
            return Json(new { Succedeed = "", message = "", prop = "" });

        }
        [HttpPost]
        public ActionResult Delete(long Id)
        {
            OperationDetails op = _manageSizeAttributeService.DeleteSizeAttribute(Id);
            return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });
        }
    }
}