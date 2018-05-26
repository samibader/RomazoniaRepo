using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using AutoMapper;
using EShop.BLL.DTO;
using EShop.BLL.Interfaces;
using EShop.WEB.Models;
using EShop.WEB.Simulation_Data;

namespace EShop.WEB.Controllers
{
    public class SearchController : BaseController
    {
        private ICategoryService _categoryService;
        private IProductFilterService _productFilterService;

        public SearchController(ISampleService serv, ICategoryService categoryService, IProductFilterService productFilterService)
        {
            _productFilterService = productFilterService;
            _categoryService = categoryService;
        }
        //
        // GET: /Search/
        public ActionResult Results(SearchVM vm)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<SearchVM, SearchDTO>());

            return View("Blank");
        }

        public PartialViewResult SearchByCategory()
        {
            CategoryMenuDTO menuDto = _categoryService.GetAll(CurrentLanguage, CurrentWebsite);

            Mapper.Initialize(cfg => cfg.CreateMap<CategoryMenuDTO, CategoryMenuVM>());

            CategoryMenuVM vm = Mapper.Map<CategoryMenuDTO, CategoryMenuVM>(menuDto);
            SearchVM svm = new SearchVM()
            {
                CategoryId = Int32.Parse(Request.QueryString["CategoryId"] ?? "-1"),
                SearchText = Request.QueryString["SearchText"] ?? "",
                Categories = vm
            };
            return PartialView("_SearchPartialView", svm);
        }

        public JsonResult Search(string SearchText, long categoryId)
        {
            List<SearchJsonResultVM> vm = new List<SearchJsonResultVM>();
            var products = _productFilterService.FilterByName(SearchText, categoryId, CurrentLanguage, CurrentCurrency,CurrentWebsite);
            foreach (var productDto in products)
            {
                vm.Add(new SearchJsonResultVM()
                {
                    ImageUrl = productDto.Images[0],
                    Link = Url.Action("Browse","Product",new {id=productDto.Id}),
                    Name = productDto.Name,
                    Id = productDto.Id.ToString()
                });
            }
            return Json(vm,JsonRequestBehavior.AllowGet);
        }
    }


}