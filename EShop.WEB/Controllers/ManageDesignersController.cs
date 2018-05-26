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
    public class ManageDesignersController : BaseController
    {
        private IManageDesigner _manageDesignerService;
        public ManageDesignersController(IManageDesigner _manageDesignerService)
        {
            this._manageDesignerService = _manageDesignerService;
        }


        //
        // GET: /ManageDesigners/
        public ActionResult Index(string en, string an, Sorts sb = Sorts.IdUp, int page = 1, int PageSize = PAGE_SIZE, long Id = 0)
        {
            List<ManageDesignerDTO> designersDtos = _manageDesignerService.Filter(an, en, Id, sb);
            Mapper.Initialize(c => c.CreateMap<ManageDesignerDTO, DesignerVM>());
            List<DesignerVM> designers = Mapper.Map<List<ManageDesignerDTO>, List<DesignerVM>>(designersDtos);
            DesignerIndexVM designerVM = new DesignerIndexVM();
            designerVM.Designers = designers.ToPagedList(page, PageSize);
            designerVM.filters = new DesignerFilter
            {
                PageNum = page,
                PageSize = PageSize,
                ArabicName = an,
                EnglishName = en,
                //Status = s,
                SortBy = sb,
                Id = Id
            };
            return View(designerVM);
        }
        [HttpPost]
        public ActionResult Index(string en, string an, long Id = 0, Sorts sb = Sorts.IdUp, int PageSize = PAGE_SIZE, int page = 1)
        {
            List<ManageDesignerDTO> designersDtos = _manageDesignerService.Filter(an, en, Id, sb);
            Mapper.Initialize(c => c.CreateMap<ManageDesignerDTO, DesignerVM>());
            List<DesignerVM> designers = Mapper.Map<List<ManageDesignerDTO>, List<DesignerVM>>(designersDtos);
            DesignerIndexVM designerVM = new DesignerIndexVM();
            designerVM.Designers = designers.ToPagedList(page, PageSize);
            designerVM.filters = new DesignerFilter
            {
                PageNum = page,
                PageSize = PageSize,
                ArabicName = an,
                EnglishName = en,
                //Status = s,
                SortBy = sb,
                Id = Id
            };
            return View(designerVM);
        }


        public ActionResult Add()
        {
            DesignerVM designer = new DesignerVM();

            return View(designer);
        }

        [HttpPost]
        public ActionResult Add(DesignerVM designerVM)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(c => c.CreateMap<DesignerVM, ManageDesignerDTO>());
                ManageDesignerDTO dto = Mapper.Map<DesignerVM, ManageDesignerDTO>(designerVM);
                OperationDetails op = _manageDesignerService.AddDesigner(dto);
                return Json(new { Succedeed = op.Succedeed, message = op.Message });
            }
            else
                return View(designerVM);
        }

        public ActionResult Edit(long Id)
        {
            ManageDesignerDTO dto = _manageDesignerService.GetDesignerById(Id);
            Mapper.Initialize(c => c.CreateMap<ManageDesignerDTO, DesignerVM>());
            DesignerVM designer = Mapper.Map<ManageDesignerDTO, DesignerVM>(dto);
           
            return View(designer);
        }

        [HttpPost]
        public ActionResult Edit(DesignerVM designerVM)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(c => c.CreateMap<DesignerVM, ManageDesignerDTO>());
                ManageDesignerDTO dto = Mapper.Map<DesignerVM, ManageDesignerDTO>(designerVM);
                OperationDetails op = _manageDesignerService.EditDesigner(dto);
                return Json(new { Succedeed = op.Succedeed, message = op.Message });
            }
            else
                return View(designerVM);

        }

        [HttpPost]
        public ActionResult Delete(long Id)
        {
            OperationDetails op = _manageDesignerService.DeleteDesigner(Id);
            return Json(new { Succedeed = op.Succedeed, message = op.Message });

        }
	}
}