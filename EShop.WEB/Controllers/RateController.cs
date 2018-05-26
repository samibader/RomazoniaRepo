using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using EShop.WEB.Models;
using EShop.WEB.Util;
using Microsoft.AspNet.Identity;

namespace EShop.WEB.Controllers
{
    public class RateController : BaseController
    {
        private IProductService _productService;
        public RateController(IProductService productService)
        {
            _productService = productService;
        }

        public PartialViewResult AddRate(long id)
        {
            ViewBag.Id = id;
            return PartialView("_AddRate");
        }
        //
        // POST: /Rate/AddRate
        
        [HttpPost]
        [AjaxAuthorize]
        public ActionResult AddRate(ProductRateVM vm)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ProductRateVM, ProductRateDTO>());
                ProductRateDTO pr = Mapper.Map<ProductRateVM, ProductRateDTO>(vm);
                pr.UserId = User.Identity.GetUserId();
                var res = _productService.AddProductRate(pr);
                return Json(res);
            }

            return Json(new OperationDetails(false,"حدث خطأ أثناء التقييم",""));

        }

        public PartialViewResult ProductRates(long id)
        {
            var vm = _productService.GetProductRates(id);
            return PartialView("_ProductRates", vm);
        }

        public ActionResult GetRate(long id)
        {
            int rate = _productService.GetProductRate(id);
            return PartialView("_Stars",rate);
        }
    }
}