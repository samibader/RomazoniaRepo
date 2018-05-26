using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using AutoMapper;

using EShop.BLL.Interfaces;
using EShop.WEB.Models;
using EShop.BLL.DTO;
using EShop.Common;

namespace EShop.WEB.Controllers
{
    public class ProductController : BaseController
    {
        //
        // GET: /Product/
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        

        // c represents Color Name
        public ActionResult Browse(long? id, long? SkuId, String c, String s)
        {
            // ProductDTO p = _productService.GetProduct(productId,CurrentLanguage);
            ProductDTO p;
            //if (String.IsNullOrWhiteSpace(c) && String.IsNullOrWhiteSpace(s))
            //    p = new Simulation_Data.ProductDTO(id);
            //else if (String.IsNullOrWhiteSpace(c))
            //    p = new ProductDTO(id,s,-1);
            //else if (String.IsNullOrWhiteSpace(s))
            //    p = new ProductDTO(id,c);
            //else 
            //    p=new ProductDTO(id,c,s);
            if (SkuId != null)
            {
                p = _productService.GetProductSKU(SkuId.Value, CurrentLanguage, Currency.Dollar);
                return View(p);
            }

            long? skuId = null;
            String fixedColor = c, fixedSize = s;
            _productService.Fix(id.Value, ref skuId, ref fixedColor, ref fixedSize);
            //if(fixedColor!=null && fixedSize !=null)
            if (fixedSize == s && fixedColor == c)
            {
                p = _productService.GetProductSKU(skuId.Value, CurrentLanguage, CurrentCurrency);
                return View(p);
            }

            return RedirectToAction("Browse", new { id = id, c = fixedColor, s = fixedSize });
        }
        public ActionResult GetRelatedProducts(long id)
        {
            ViewBag.Title = "منتجات مشابهة";
            var related = _productService.GetRelatedProducts(id, CurrentLanguage, CurrentCurrency);
            return PartialView("_RelatedProducts", related);
        }
        public ActionResult GetTopProducts(long id, bool main)
        {
            ViewBag.Title = "المنتجات الأكثر مبيعاً";

            var related = _productService.GetRelatedProducts(id, CurrentLanguage, CurrentCurrency);
            if (related == null)
                return PartialView("_BlankPartial");
            string partialName = main ? "_TopProducts" : "_RelatedProducts";
            return PartialView(partialName, related);
        }
        public ActionResult QuickView(long id,string color)
        {
            long? skuId = null;
            string sizeName = null;
            _productService.Fix(id, ref skuId, ref color, ref sizeName);

            var dto = _productService.GetProductSKU(skuId.Value, CurrentLanguage, CurrentCurrency);
            return PartialView("_QuickView", dto);
        }

        public ActionResult GetLatestProducts()
        {
            var latest = _productService.GetLatestProducts(CurrentLanguage, CurrentCurrency, CurrentWebsite);

            return PartialView("_LatestProducts", latest);
        }
        public ActionResult GetMostPopularProducts()
        {
            var latest = _productService.GetMostPopularProducts(CurrentLanguage, CurrentCurrency, CurrentWebsite);

            return PartialView("_MostPopular", latest);
        }
    }
}