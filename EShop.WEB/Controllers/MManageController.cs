using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EShop.BLL.DTO;
using EShop.BLL.Services;
using EShop.Common;
using EShop.WEB.Models;
using EShop.WEB.Util;
using Microsoft.AspNet.Identity;

namespace EShop.WEB.Controllers
{
    [AjaxAuthorize]
    [Authorize]
    public class MManageController : BaseController
    {
        private readonly AddressService _addressService;
        private readonly OrderService _orderService;
        //
        // GET: /MManage/
        public MManageController(AddressService addressService, OrderService orderService)
        {
            _addressService = addressService;
            _orderService = orderService;

        }

        public ActionResult Index()
        {
            ViewBag.Archived =
                _orderService.GetUserOrdersByState(User.Identity.GetUserId(), OrderStates.complete, CurrentLanguage,CurrentCurrency)
                    .Count;
            ViewBag.Canceled =
                _orderService.GetUserOrdersByState(User.Identity.GetUserId(), OrderStates.canceled, CurrentLanguage,CurrentCurrency)
                    .Count;
            ViewBag.Active =
                            _orderService.GetUserOrdersByState(User.Identity.GetUserId(), OrderStates.Active, CurrentLanguage,CurrentCurrency)
                                .Count;

            return View();
        }

        public ActionResult AddressBook()
        {
            List<AddressDTO> addresses = _addressService.GetUserAddresses(User.Identity.GetUserId());
            Mapper.Initialize(cfg => cfg.CreateMap<AddressDTO, AddressVM>());
            var vm = Mapper.Map<List<AddressDTO>, List<AddressVM>>(addresses);
            return View(vm);
        }

        public ActionResult EditAddress(long id)
        {
            var addressDto = _addressService.GetAddressById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<AddressDTO, AddressVM>());
            var vm = Mapper.Map<AddressDTO, AddressVM>(addressDto);
            return View(vm);
        }

        [HttpPost]
        public ActionResult EditAddress(AddressVM vm)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<AddressVM, AddressDTO>());
                var addressDto = Mapper.Map<AddressVM, AddressDTO>(vm);
                var res = _addressService.EditAddress(vm.Id, addressDto);
                if (res.Succedeed)
                {
                    return RedirectToAction("AddressBook");
                }
                else
                {
                    ModelState.AddModelError(res.Property, res.Message);
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }

        }

        public ActionResult DeleteAddress(long id)
        {
            var res = _addressService.RemoveAddress(id);

            return Json(res);
        }

        public ActionResult AddAddress()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAddress(AddressVM vm)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<AddressVM, AddressDTO>());
                var addressDto = Mapper.Map<AddressVM, AddressDTO>(vm);
                var res = _addressService.AddAddress(User.Identity.GetUserId(), addressDto);
                if (res.Succedeed)
                {
                    return RedirectToAction("AddressBook");
                }
                else
                {
                    ModelState.AddModelError(res.Property, res.Message);
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }

        }


        public ActionResult Order(string state)
        {

            List<OrderDTO> orders;

            var st = (OrderStates)Enum.Parse(typeof(OrderStates), state, true);

            orders = _orderService.GetUserOrdersByState(User.Identity.GetUserId(), st, CurrentLanguage,CurrentCurrency);
            switch (st)
            {
                case OrderStates.canceled:
                    ViewBag.State = "الطلبات الملغاة";
                    break;
                case OrderStates.complete:
                    ViewBag.State = "الطلبات المؤرشفة";
                    break;
                default:
                    ViewBag.State = "الطلبات الفعالة";
                    break;
            }


            Mapper.Initialize(cfg => cfg.CreateMap<OrderDTO, OrderVM>());

            var vm = Mapper.Map<List<OrderDTO>, List<OrderVM>>(orders);
            return View(vm);
        }

    }
}