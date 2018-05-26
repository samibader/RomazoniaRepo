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
    public class ManageAttributeGroupController : BaseController
    {
        private IManageAttributeGroup _ManageAttributeGroup;
        public ManageAttributeGroupController(IManageAttributeGroup _ManageAttributeGroup)
        {
            this._ManageAttributeGroup = _ManageAttributeGroup;
        }
        public ActionResult Index(string en, string an, Sorts sb = Sorts.IdUp, int page = 1, int PageSize = PAGE_SIZE, long Id = 0)
        {
            List<AttributeGroupDTO> groupsDtos = _ManageAttributeGroup.Filter(an, en, Id, sb);
            Mapper.Initialize(c => c.CreateMap<AttributeGroupDTO, AttributeGroupVM>());
            List<AttributeGroupVM> groups = Mapper.Map<List<AttributeGroupDTO>, List<AttributeGroupVM>>(groupsDtos);
            AttributeGroupIndexVM groupsVM = new AttributeGroupIndexVM();
            groupsVM.AttributeGroups = groups.ToPagedList(page, PageSize);
            groupsVM.filters = new AttributeGroupFilter
            {
                PageNum = page,
                PageSize = PageSize,
                ArabicName = an,
                EnglishName = en,
                //Status = s,
                SortBy = sb,
                Id = Id
            };
            return View(groupsVM);
        }
        [HttpPost]
        public ActionResult Index(string en, string an, long Id = 0, Sorts sb = Sorts.IdUp, int PageSize = PAGE_SIZE, int page = 1)
        {
            List<AttributeGroupDTO> groupsDtos = _ManageAttributeGroup.Filter(an, en, Id, sb);
            Mapper.Initialize(c => c.CreateMap<AttributeGroupDTO, AttributeGroupVM>());
            List<AttributeGroupVM> groups = Mapper.Map<List<AttributeGroupDTO>, List<AttributeGroupVM>>(groupsDtos);
            AttributeGroupIndexVM groupsVM = new AttributeGroupIndexVM();
            groupsVM.AttributeGroups = groups.ToPagedList(page, PageSize);
            groupsVM.filters = new AttributeGroupFilter
            {
                PageNum = page,
                PageSize = PageSize,
                ArabicName = an,
                EnglishName = en,
                //Status = s,
                SortBy = sb,
                Id = Id
            };
            return View(groupsVM);
        }

        public ActionResult Add()
        {
            AttributeGroupVM attributeGroup = new AttributeGroupVM();
            return View(attributeGroup);
        }
        [HttpPost]
        public ActionResult Add(AttributeGroupVM group)
        {
            if (ModelState.IsValid) { 

            Mapper.Initialize(c => c.CreateMap<AttributeGroupVM,AttributeGroupDTO>());
            AttributeGroupDTO dto = Mapper.Map<AttributeGroupVM, AttributeGroupDTO>(group);
            OperationDetails op = _ManageAttributeGroup.AddAttributeGroup(dto); 
           
            return Json(new { Succedeed = op.Succedeed, message = op.Message });
            }
            else
            {
                return View(group);
            }

        }
        public ActionResult Edit(long Id)
        {
            AttributeGroupDTO attributeGroupDTO = _ManageAttributeGroup.GetAttributeGroupById(Id);
            Mapper.Initialize(c => c.CreateMap<AttributeGroupDTO, AttributeGroupVM>());
            AttributeGroupVM attributeGroup = Mapper.Map<AttributeGroupDTO, AttributeGroupVM>(attributeGroupDTO);
            return View(attributeGroup);
        }
        [HttpPost]
        public ActionResult Edit(AttributeGroupVM group)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(c => c.CreateMap<AttributeGroupVM, AttributeGroupDTO>());
                AttributeGroupDTO dto = Mapper.Map<AttributeGroupVM, AttributeGroupDTO>(group);
                OperationDetails op = _ManageAttributeGroup.EditAttributeGroup(dto);

                return Json(new { Succedeed = op.Succedeed, message = op.Message });
            }
            else
                return View(group);
        }

        public ActionResult Delete(long Id)
        {

            OperationDetails op = _ManageAttributeGroup.DeleteAttribGroup(Id);
            return Json(new { Succedeed = op.Succedeed, message = op.Message });

        }
       
	}
}