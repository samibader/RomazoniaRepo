using EShop.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using EShop.BLL.Interfaces;
using AutoMapper;
using EShop.BLL.DTO;
using EShop.Common;
using EShop.BLL.Infrastructure;

namespace EShop.WEB.Controllers
{
    [Authorize(Roles = "AdminRole")]
    public class ManageCategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public ManageCategoryController(ICategoryService _categoryService)
        {
            this._categoryService = _categoryService;
        }
        //
        // GET: /ManageCategory/
        public ActionResult Index(String an, String en, Boolean? s, int PageSize = PAGE_SIZE, int page = 1, Sorts sb = Sorts.IdUp, Int32 Id = 0, DateTime? dc = null, DateTime? dm = null)
        {


            page = page > 1 ? page : 1;
            PageSize = PageSize > 0 ? PageSize : PAGE_SIZE;
            //ViewBag.CurrentWebsite = CurrentWebsite;
            List<CategoryManegerDTO> categoriesFull = _categoryService.Filter(an, en, s,CurrentWebsite, Id, dc, dm, sb);
            

            Mapper.Initialize(c => c.CreateMap<CategoryManegerDTO, CategoryDetailsVm>());
            List<CategoryDetailsVm> categories = Mapper.Map<List<CategoryManegerDTO>, List<CategoryDetailsVm>>(categoriesFull);
            ManageCategoryVM manageCategoryVM = new ManageCategoryVM
            {
                categories = categories.ToPagedList(page, PageSize),
                filters = new CategoryManageFilter
                {

                    PageNum = page,
                    PageSize = PageSize,
                    ArabicName = an,
                    EnglishName = en,
                    Status = s,
                    DateCreation = null,
                    DateModified = null,
                    SortBy = sb,
                    Id = Id

                }
            };



            return View(manageCategoryVM);
        }

        [HttpPost]
        public ActionResult Index(String an, String en, DateTime? dc, Boolean? s, DateTime? dm, Sorts sb = Sorts.IdUp, int page = 1, int PageSize = PAGE_SIZE, Int32 Id = 0)
        {
            //send values of filters to service

            List<CategoryManegerDTO> categoriesFull = _categoryService.Filter(an, en, s,CurrentWebsite, Id, dc, dm, sb);
            ManageCategoryVM manageCategoryVM = new ManageCategoryVM();

            Mapper.Initialize(c => c.CreateMap<CategoryManegerDTO, CategoryDetailsVm>());
            List<CategoryDetailsVm> categories = Mapper.Map<List<CategoryManegerDTO>, List<CategoryDetailsVm>>(categoriesFull);
            manageCategoryVM.filters = new CategoryManageFilter
            {
                PageNum = page,
                PageSize = PageSize,
                ArabicName = an,
                EnglishName = en,
                Status = s,
                DateCreation = dc,
                DateModified = dm,
                SortBy = sb,
                Id = Id
            };

            manageCategoryVM.categories = categories.ToPagedList(page, PageSize);
            return View(manageCategoryVM);
            

        }
        public ActionResult Add()
        {
           List<CategoryPathDTO> tempParentCategories = _categoryService.GetAllPaths(CurrentLanguage, CurrentWebsite);

           List<CategoryPathDTO> parentCategoriesDTO = new List<CategoryPathDTO>();

           foreach (var cat in tempParentCategories)
           {
               if (cat.Path.Count < 4)
               {
                   parentCategoriesDTO.Add(cat);
               }
           }

           Mapper.Initialize(c => c.CreateMap<CategoryPathDTO,AddCategoryPathVM>());

           List<AddCategoryPathVM> parentCategoriesVM = Mapper.Map<List<CategoryPathDTO>, List<AddCategoryPathVM>>(parentCategoriesDTO);

           AddCategoryVM model = new AddCategoryVM();
           model.Category = new CategoryDetailsVm();
           model.ParentCategories = parentCategoriesVM;

            return View("Addd",model);
        }
        [HttpPost]
        public ActionResult Add(AddCategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                vm.Category.DateCreation = DateTime.Now;
                vm.Category.DateModefied = DateTime.Now;

                CategoryManegerDTO categoryDTO = new CategoryManegerDTO();
                Mapper.Initialize(c =>c.CreateMap<CategoryDetailsVm, CategoryManegerDTO>());
                categoryDTO = Mapper.Map<CategoryDetailsVm, CategoryManegerDTO>(vm.Category);
                OperationDetails op = _categoryService.AddCategory(categoryDTO);
                return Json(new { Succedeed = op.Succedeed, message = op.Message });
            }
            else
            {
                List<CategoryPathDTO> tempParentCategories = _categoryService.GetAllPaths(CurrentLanguage, CurrentWebsite);

                List<CategoryPathDTO> parentCategoriesDTO = new List<CategoryPathDTO>();

                foreach (var cat in tempParentCategories)
                {
                    if (cat.Path.Count < 3)
                    {
                        parentCategoriesDTO.Add(cat);
                    }
                }

                Mapper.Initialize(c => c.CreateMap<CategoryPathDTO, AddCategoryPathVM>());

                List<AddCategoryPathVM> parentCategoriesVM = Mapper.Map<List<CategoryPathDTO>, List<AddCategoryPathVM>>(parentCategoriesDTO);
                vm.ParentCategories = parentCategoriesVM;
                return View("Addd",vm);
            }
        
        }
        public ActionResult Edit(long Id)
        {
            CategoryManegerDTO categoryDTO = _categoryService.GetCategoryManagerById(Id);

            List<CategoryPathDTO> tempParentCategories = _categoryService.GetAllPaths(CurrentLanguage, CurrentWebsite);
            List<CategoryPathDTO> parentCategoriesDTO = new List<CategoryPathDTO>();

            foreach (var cat in tempParentCategories)
            {
                
                if (cat.Path.Count < 4)
                {
                    cat.CategoryId = cat.Path[cat.Path.Count - 1].Item1;
                    parentCategoriesDTO.Add(cat);
                }
            }

            Mapper.Initialize(c => c.CreateMap<CategoryPathDTO, AddCategoryPathVM>());
            
            List<AddCategoryPathVM> parentCategoriesVM = Mapper.Map<List<CategoryPathDTO>, List<AddCategoryPathVM>>(parentCategoriesDTO);
            SelectList list = new SelectList(parentCategoriesVM, "CategoryId", "PathStr", parentCategoriesVM[0]);
            Mapper.Initialize(c => c.CreateMap<CategoryManegerDTO, CategoryDetailsVm>());
            CategoryDetailsVm categoryVM = Mapper.Map<CategoryManegerDTO, CategoryDetailsVm>(categoryDTO);

            AddCategoryVM model = new AddCategoryVM();
            model.Category = categoryVM;
            model.ParentCategories = parentCategoriesVM;

            
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AddCategoryVM vm)
        {
            if (ModelState.IsValid)
            {

                CategoryManegerDTO categoryDTO = new CategoryManegerDTO();
                Mapper.Initialize(c =>c.CreateMap<CategoryDetailsVm, CategoryManegerDTO>());
                categoryDTO = Mapper.Map<CategoryDetailsVm, CategoryManegerDTO>(vm.Category);
                OperationDetails op = _categoryService.EditCategoryManager(categoryDTO);
                return Json(new { Succedeed = op.Succedeed, message = op.Message });
            }
            else
            {
                
                List<CategoryPathDTO> tempParentCategories = _categoryService.GetAllPaths(CurrentLanguage, Common.WebSites.Fashion);

                List<CategoryPathDTO> parentCategoriesDTO = new List<CategoryPathDTO>();

                foreach (var cat in tempParentCategories)
                {
                    if (cat.Path.Count < 3)
                    {
                        parentCategoriesDTO.Add(cat);
                    }
                }

                Mapper.Initialize(c => c.CreateMap<CategoryPathDTO, AddCategoryPathVM>());

                List<AddCategoryPathVM> parentCategoriesVM = Mapper.Map<List<CategoryPathDTO>, List<AddCategoryPathVM>>(parentCategoriesDTO);
                vm.ParentCategories = parentCategoriesVM;
                return View(vm);
            }
        }
        [HttpPost]
        public ActionResult Delete(long Id)
        {
            OperationDetails op = _categoryService.DeleteCategory(Id);
            return Json(new { Succedeed = op.Succedeed, message = op.Message });
        }

       


    }
}