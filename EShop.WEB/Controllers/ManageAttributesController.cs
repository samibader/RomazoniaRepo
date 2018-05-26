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
    public class ManageAttributesController : BaseController
    {
        private IManageAttributeGroup _ManageAttributeGroup;
        public ManageAttributesController(IManageAttributeGroup _ManageAttributeGroup)
        {
            this._ManageAttributeGroup = _ManageAttributeGroup;
        }
        //
        // GET: /ManageAtributes/
        public ActionResult Index(string en, string an,string gn, Sorts sb = Sorts.IdUp, int page = 1, int PageSize = PAGE_SIZE, long Id = 0,long GroupId=0)
        {
            List<AttributeDTO> attributesDtos = _ManageAttributeGroup.FilterAtrributes(an, GroupId, en, Id,gn, sb);
            Mapper.Initialize(c => c.CreateMap<AttributeDTO, AttributeVM>());
            List<AttributeVM> attributes = Mapper.Map<List<AttributeDTO>, List<AttributeVM>>(attributesDtos);
            AttributeIndexVM AttributesVM = new AttributeIndexVM();
            AttributesVM.Attributes = attributes.ToPagedList(page, PageSize);
            AttributesVM.filters = new AttributeFilter
            {
                PageNum = page,
                PageSize = PageSize,
                ArabicName = an,
                EnglishName = en,
                GroupName = gn,
                //Status = s,
                SortBy = sb,
                GroupId =GroupId,
                Id = Id
            };
            return View(AttributesVM);
        }
        [HttpPost]
        public ActionResult Index(string en, string an,string gn, Sorts sb = Sorts.IdUp, int page = 1, long GroupId = 0, int PageSize = PAGE_SIZE, long Id = 0)
        {
            List<AttributeDTO> attributesDtos = _ManageAttributeGroup.FilterAtrributes(an, GroupId, en, Id,gn, sb);
            Mapper.Initialize(c => c.CreateMap<AttributeDTO, AttributeVM>());
            List<AttributeVM> attributes = Mapper.Map<List<AttributeDTO>, List<AttributeVM>>(attributesDtos);
            AttributeIndexVM AttributesVM = new AttributeIndexVM();
            AttributesVM.Attributes = attributes.ToPagedList(page, PageSize);
            AttributesVM.filters = new AttributeFilter
            {
                PageNum = page,
                PageSize = PageSize,
                ArabicName = an,
                EnglishName = en,
                GroupName = gn,
                //Status = s,
                SortBy = sb,
                GroupId = GroupId,
                Id = Id
            };
            return View(AttributesVM);
        }
        
        public ActionResult Add()
        {
           
            //List<AttributeGroupDTO> groupsDTO = _ManageAttributeGroup.GetAllAttributeGroups();
            //List<SelectListItem> groups = groupsDTO.ConvertAll(a =>
            //{
            //    return new SelectListItem()
            //    {
            //        Text = a.ArabicName,
            //        Value = a.Id.ToString(),
            //        Selected = false
            //    };
            //});
            //ViewBag.Groups = groups;
            AttributeVM attribute = new AttributeVM();
            attribute.Groups = _ManageAttributeGroup.GetAllAttributeGroups(); 
            return View(attribute);
        }
        
        [HttpPost]
        public ActionResult Add(AttributeVM attribute)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(c => c.CreateMap<AttributeVM, AttributeDTO>());
                AttributeDTO dto = Mapper.Map<AttributeVM, AttributeDTO>(attribute);
                OperationDetails op = _ManageAttributeGroup.AddAttribute(dto);

                return Json(new { Succedeed = op.Succedeed, message = op.Message });
            }
            else
                return View(attribute);
        }

        public ActionResult Edit(long Id)
        {
            AttributeDTO attributeDTO = _ManageAttributeGroup.GetAttributeById(Id);
            Mapper.Initialize(c => c.CreateMap<AttributeDTO, AttributeVM>());
            AttributeVM attribute = Mapper.Map<AttributeDTO, AttributeVM>(attributeDTO);
            return View(attribute);
        }
        [HttpPost]
        public ActionResult Edit(AttributeVM attribute)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(c => c.CreateMap<AttributeVM, AttributeDTO>());
                AttributeDTO dto = Mapper.Map<AttributeVM, AttributeDTO>(attribute);
                OperationDetails op = _ManageAttributeGroup.EditAttribute(dto);

                return Json(new { Succedeed = op.Succedeed, message = op.Message });
            }
            else
                return View(attribute);
        }

        [HttpPost]
        public ActionResult Delete(long Id)
        {

            OperationDetails op = _ManageAttributeGroup.DeleteAttrib(Id);
            return Json(new { Succedeed = op.Succedeed, message = op.Message });

        }
	}
}