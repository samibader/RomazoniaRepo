using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using EShop.WEB.Models;
using EShop.BLL.DTO;
using EShop.Common;
using EShop.WEB.Util;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Provider;


namespace EShop.WEB.Controllers
{
    public class CartController : BaseController
    {
        private ICartService _cartService;
        private IProductService _productService;
        public CartController(ICartService cartService,IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }
        //
        // GET: /Cart/
        // sideFlag parameter for sidebar cartSummary In CategoryBrowseView ..
        // it drive action to render the Corresponding Partial. 
        public ActionResult CartSummary(bool sideFlag = false)
        {
            List<ProductDTO> products = _cartService.GetCartProducts(User.Identity.GetUserId(), CurrentLanguage, CurrentCurrency);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ProductDTO, CartItem>();
            });
            List<CartItem> list = Mapper.Map<List<ProductDTO>, List<CartItem>>(products);
            ViewBag.TotalDisplay = Utils.GetValueCurrencyDisplay(
                Utils.getCurrencyName(CurrentCurrency, CurrentLanguage), list.Sum(c => c.Quantity * c.TotalPrice));
            //List<CartItem> list = (List<CartItem>)System.Web.HttpContext.Current.Session["Cart"];
            if (list == null)
                return new EmptyResult();
            if (!sideFlag)
                return PartialView("_CartSummary", list);
            else
            {
                return PartialView("_SideCartSummary", list);
            }

        }

        [AjaxAuthorize]
        public ActionResult QuickAdd(long ProductId, string color)
        {
            long? skuId = null;
            string sizeName = null;
            _productService.Fix(ProductId, ref skuId, ref color, ref sizeName);
            return Add(skuId.Value, ProductId, 1);
        }
        [AjaxAuthorize]
        public ActionResult Add(long SkuId, long ProductId, int Quantity)
        {

            // if (Quantity > 5) return Content("لايوجد هذه الكمية ");
            OperationDetails result = _cartService.AddToCart(SkuId, ProductId, User.Identity.GetUserId(), Quantity);
            //if (result.Succedeed)
            //    this.Flash("sucess",result.Message);
            //else
            //    this.Flash("error",result.Message);
            //return Content(result.Message);
            return Json(new { Success = result.Succedeed, Msg = result.Message });
            //List<CartItem> list = (List<CartItem>)System.Web.HttpContext.Current.Session["Cart"];
            //if (list == null)
            //    System.Web.HttpContext.Current.Session["Cart"] = new List<CartItem>();
            //CartItem cart = list.Where(c => c.SkuId == SkuId).FirstOrDefault();
            //if (cart == null)
            //{
            //    cart = new CartItem();
            //    cart.SkuId = SkuId;
            //    cart.Quantity += Quantity;
            //    list.Add(cart);
            //}
            //else
            //{
            //    cart.Quantity += Quantity;
            //}
            //System.Web.HttpContext.Current.Session["Cart"] = list;
            //return Content("Added To Cart");
        }

        [AjaxAuthorize]
      
        public ActionResult Remove(String SkuId)
        {
            String[] ids = SkuId.Split('-');
            long skuId = Int64.Parse(ids[0]);
            long productId = Int64.Parse(ids[1]);
            OperationDetails result = _cartService.Remove(skuId, productId, User.Identity.GetUserId());

            //List<CartItem> list = (List<CartItem>)System.Web.HttpContext.Current.Session["Cart"];
            //bool res = list.Remove(list.FirstOrDefault(c => c.SkuId == skuId));
            //if (res)
            //    return Content("Item Removed");

            return Json(result);

            //return Content("Error Occured");

        }
        
        [AjaxAuthorize]
        public ActionResult Browse()
        {
            DetailedCartVM vm = new DetailedCartVM();
            List<ProductDTO> cartProducts = _cartService.GetCartProducts(User.Identity.GetUserId(), CurrentLanguage,
                CurrentCurrency);

            List<DetailedCartItemVM> cartItems = new List<DetailedCartItemVM>();
            foreach (var cartProduct in cartProducts)
            {
                cartItems.Add(new DetailedCartItemVM()
                {
                    Availabel = cartProduct.IsAvailable,
                    Name = cartProduct.Name,
                    Description = cartProduct.Text,
                    Id = cartProduct.Id,
                    SKUId = cartProduct.SKUId,
                    Color = (cartProduct.SelectedColor >= 0 ? cartProduct.Colors[cartProduct.SelectedColor] : null),
                    Size = (cartProduct.SelectedSize >= 0 ? cartProduct.Sizes[cartProduct.SelectedSize] : null),
                    Image = cartProduct.Images[0],
                    Quantity = cartProduct.Quantity,
                    Price = cartProduct.TotalPrice,
                    PriceDisplay = cartProduct.TotalPriceDisplay,
                    TotalPrice = cartProduct.Quantity * cartProduct.TotalPrice,
                    TotalPriceDisplay = Utils.GetValueCurrencyDisplay(cartProduct.CurrencyName, cartProduct.Quantity * cartProduct.TotalPrice)

                });
            }
            vm.DetailedCartItems = cartItems;
            //vm.DetailedCartItems = new List<DetailedCartItemVM>();
            //vm.DetailedCartItems.Add(c1);
            //vm.DetailedCartItems.Add(c1);
            //vm.DetailedCartItems.Add(c1);
            //vm.DetailedCartItems.Add(c1);
            vm.TotalPrice = vm.DetailedCartItems.Sum(c => c.Price * c.Quantity);
            vm.TotalPriceDisplay = Utils.GetValueCurrencyDisplay(Utils.getCurrencyName(CurrentCurrency, CurrentLanguage), vm.TotalPrice);

            return View(vm);
        }
        
        [AjaxAuthorize]
        public ActionResult RemoveAll()
        {
            var res = _cartService.RemoveAll(User.Identity.GetUserId());
            return Json(res);

        }

        public ActionResult Edit(List<DetailedCartItemVM> products)
        {
            var productDtos = new List<ProductDTO>();
            foreach (var detailedCartItemVm in products)
            {
                productDtos.Add(new ProductDTO()
                {
                    Id = detailedCartItemVm.Id,
                    SKUId = detailedCartItemVm.SKUId,
                    Quantity = detailedCartItemVm.Quantity
                });
            }
            var res = _cartService.UpdateCart(User.Identity.GetUserId(), productDtos);
            return Json(res) ;
        }
    }
}