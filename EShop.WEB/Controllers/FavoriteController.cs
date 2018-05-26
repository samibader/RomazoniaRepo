using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using EShop.Common;
using EShop.WEB.Models;
using EShop.WEB.Util;
using Microsoft.AspNet.Identity;
using MvcFlashMessages;

namespace EShop.WEB.Controllers
{
    [AjaxAuthorize]
    public class FavoriteController : BaseController
    {
        private readonly IFavoriteService _favoriteService;
        private IProductService _productService;
        public FavoriteController(IFavoriteService favoriteService,IProductService productService)
        {
            _favoriteService = favoriteService;
            _productService = productService;
        }
        //
        // GET: /Favorite/
        [AllowAnonymous]
        public ActionResult Count()
        {
            return Content(_favoriteService.Count(User.Identity.GetUserId()).ToString());
        }

        public ActionResult Browse()
        {
            List<ProductDTO> products = _favoriteService.GetFavoriteProducts(User.Identity.GetUserId(), CurrentLanguage,Currency.Dollar);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ProductDTO, FavoriteItem>()
                    .ForMember(dto => dto.Color, opt => opt.MapFrom(source => source.SelectedColor>=0?source.Colors[source.SelectedColor]:null))
                        .ForMember(dto => dto.Size, oopt => oopt.MapFrom(source => source.SelectedSize>=0?source.Sizes[source.SelectedSize]:null));
            });
            List<FavoriteItem> list = Mapper.Map<List<ProductDTO>, List<FavoriteItem>>(products);

            //List<CartItem> list = (List<CartItem>)System.Web.HttpContext.Current.Session["Cart"];
            if (list == null)
                return new EmptyResult();
            return View(list);
        }

        public ActionResult Remove(long productId,long skuId)
        {
            var result = _favoriteService.Remove(productId,skuId,User.Identity.GetUserId());
           
            //return Content(result.Message);
            return Json(new { Success = result.Succedeed, Msg = result.Message });
        }

        public ActionResult AddToFavorite(long productId,String cName)
        {
            long? skuId = null;
            string sizeName = null;
            _productService.Fix(productId, ref skuId, ref cName, ref sizeName);
           
            var result = _favoriteService.AddToFavorite(productId,skuId.Value, User.Identity.GetUserId());

          //  return Content(result.Message);
            return Json(new { Success = result.Succedeed, Msg = result.Message });

        }
    }
}