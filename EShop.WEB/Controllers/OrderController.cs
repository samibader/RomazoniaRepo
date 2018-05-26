using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.UI.WebControls;
using AutoMapper;
using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Services;
using EShop.WEB.Models;
using Microsoft.AspNet.Identity;

namespace EShop.WEB.Controllers
{
    public class OrderController : BaseController
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        //
        // GET: /Order/
        public ActionResult Index(long id)
        {
            OrderDTO orderDto = _orderService.GetOrderById(id,CurrentLanguage,CurrentCurrency);
            Mapper.Initialize(cfg => cfg.CreateMap<OrderItemDTO, OrderItemVM>());
            Mapper.Initialize(cfg => cfg.CreateMap<AddressDTO, AddressVM>());
            Mapper.Initialize(cfg =>cfg.CreateMap<OrderDTO,OrderVM>());
        

            OrderVM vm = Mapper.Map<OrderDTO, OrderVM>(orderDto);

            return View(vm);
        }

        [HttpPost]
        public ActionResult BankConfirm(UserBankInforamtionVM model)

        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg=>cfg.CreateMap<UserBankInforamtionVM,AddtionalInfoDTO>());
                var dto = Mapper.Map<UserBankInforamtionVM, AddtionalInfoDTO>(model);
                var res = _orderService.AddOrderWithInfo(User.Identity.GetUserId(),Request.UserHostAddress,Request.UserAgent, CurrentLanguage, CurrentCurrency,dto);
              
                if (res.Succedeed)
                {
                    ViewBag.flag = true;
                    ViewBag.data = Url.Action("Index", "Order", new {id = Int64.Parse(res.Property)});
                }
                else
                {
                    ViewBag.flag = false;
                }
            }
            else
            {
                ViewBag.flag = false;
            }
            return PartialView("_AddtionalInformationPartial", model);

        }
        public ActionResult Confirm()
        {
            //var checkout = Session["checkout"] as CheckOutDTO;

            var res = _orderService.AddOrder(User.Identity.GetUserId(),Request.UserHostAddress,Request.UserAgent,CurrentLanguage,CurrentCurrency);

            //if (res.Succedeed)
            //{
            //    //Session["checkout"] = null;
            //    //Session["step"] = null;
                res = new OperationDetails(true,Url.Action("Index","Order", new {id = Int64.Parse(res.Property)}),"");
            //}
            return Json(res);
        }
	}
}