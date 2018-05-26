using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using EShop.BLL.Services;
using EShop.Common;
using EShop.WEB.Models;
using Microsoft.AspNet.Identity;

namespace EShop.WEB.Controllers
{
    public class AddressController : BaseController
    {
        private IAddressService _addressService;
        private ICheckOutService _checkOutService;

        private CheckOutDTO checkout
        {
            get
            {
                return _checkOutService.GetSessionOrder(User.Identity.GetUserId(), CurrentLanguage,
                CurrentCurrency);
            }

        }

        public AddressController(IAddressService addressService, ICheckOutService checkOutService)
        {
            _addressService = addressService;
            _checkOutService = checkOutService;
        }
        //
        // POST: /Address/Add

        public PartialViewResult Add()
        {
            
            return PartialView("_AddAddress",new AddressVM());
        }
        public PartialViewResult Edit(long id)
        {
            //ViewBag.Id = id;
            //var checkOut = Session["checkout"] as CheckOutDTO;
            var address = checkout.AvailableAddresses.Where(ad => ad.Id == id).FirstOrDefault();
            Mapper.Initialize(cfg => cfg.CreateMap<AddressDTO, AddressVM>());
            AddressVM vm = Mapper.Map<AddressDTO, AddressVM>(address);
            return PartialView("_AddAddress",vm);
        }
        public PartialViewResult Addresses()
        {
            //var checkOut = Session["checkout"] as CheckOutDTO;

            Mapper.Initialize(cfg => cfg.CreateMap<CheckOutDTO, CheckoutVM>());
            CheckoutVM vm = Mapper.Map<CheckOutDTO, CheckoutVM>(checkout);
            ViewBag.NoCost = Utils.GetValueCurrencyDisplay(Utils.getCurrencyName(CurrentCurrency, CurrentLanguage), 0); 
            return PartialView("_UserAddresses", vm);
        }

        [HttpPost]
        public PartialViewResult Add(AddressVM address)
        {

            if (ModelState.IsValid)
            {

                var newAddress = new AddressDTO()
                {
                    Id = address.Id,
                    FirstName = address.FirstName,
                    LastName = address.LastName,
                    Address1 = address.Address1,
                    City = address.City,
                    Country = address.Country,
                    PostCode = address.PostCode,
                    Phone = address.Phone,
                    IsDefault = address.IsDefault
                };
                OperationDetails result = new OperationDetails(false,"","");
                if (address.Id > 0)
                {
                    result = _addressService.EditAddress(address.Id, newAddress);
                }
                else
                {
                    result = _addressService.AddAddress(User.Identity.GetUserId(), newAddress);
                    newAddress.Id = Int64.Parse(result.Message);
                }
                ViewBag.result = result;
                //if (result.Succedeed)
                //{
                //    //CheckOutDTO checkOut = Session["checkout"] as CheckOutDTO;
                //    //if (checkOut != null)
                //    //{
                //    //    if (newAddress.IsDefault)
                //    //    {
                //    //        checkOut.AvailableAddresses.ForEach(ad => ad.IsDefault = false);
                //    //    }
                //    //    if (address.Id <= 0)
                //    //    {
                //    //        checkOut.AvailableAddresses.Add(
                //    //            newAddress);
                //    //    }
                //    //    else
                //    //    {
                //    //        var index = checkOut.AvailableAddresses.FindIndex(ad => ad.Id == address.Id);

                //    //        checkOut.AvailableAddresses[index] = newAddress;
                //    //    }

                //    //    Session["checkout"] = checkOut;
                //    //}
                //}
                ViewBag.flag = true;
                return PartialView("_AddAddress",new AddressVM());
            }
            ViewBag.result = new OperationDetails(false,"خطأ","");
            address.IsDefault = false;
            return PartialView("_AddAddress", address);
        }

        public ActionResult Delete(long id)
        {
            var result = _addressService.RemoveAddress(id);
            
            if (result.Succedeed)
            {
            //    CheckOutDTO checkOut = Session["checkout"] as CheckOutDTO;
            //    var index=checkOut.AvailableAddresses.FindIndex(ad => ad.Id == id);
            //    checkOut.AvailableAddresses.Remove(checkOut.AvailableAddresses[index]);
                if (checkout.SelectedBillingAddressId == id)
                    _checkOutService.SetBillingAddress(User.Identity.GetUserId(), checkout.AvailableAddresses.Where(ad => ad.IsDefault).FirstOrDefault().Id);
                //        checkOut.SelectedBillingAddressId =
                //            checkOut.AvailableAddresses.Where(ad => ad.IsDefault).FirstOrDefault().Id;
                if (checkout.SelectedShippingAddressId == id)
                    _checkOutService.SetShippingAddress(User.Identity.GetUserId(), checkout.AvailableAddresses.Where(ad => ad.IsDefault).FirstOrDefault().Id);
                //        checkOut.SelectedShippingAddressId =
                //            checkOut.AvailableAddresses.Where(ad => ad.IsDefault).FirstOrDefault().Id;
                //    Session["checkout"] = checkOut;
            }
            return Json(result);
        }
        public ActionResult ChangeBillingAddress(long id)
        {
            //var checkout = Session["checkout"] as CheckOutDTO;
            //checkout.SelectedBillingAddressId = id;
           var res = _checkOutService.SetBillingAddress(User.Identity.GetUserId(), id);
            //Session["checkout"] = checkout;

            return Json(res);
        }
        public ActionResult ChangeShippingAddress(long id)
        {
            //var checkout = Session["checkout"] as CheckOutDTO;
            //checkout.SelectedShippingAddressId = id;
            //Session["checkout"] = checkout;
            var res = _checkOutService.SetShippingAddress(User.Identity.GetUserId(), id);
            return Json(res);
        }

        public ActionResult ChangeToSame(bool isSame, long id)
        {
            //var checkout = Session["checkout"] as CheckOutDTO;
            
            //checkout.SelectedBillingAddressId = id;

            //checkout.SelectedShippingAddressId = id;


            //checkout.IsSameAddresses = isSame;
           // Session["checkout"] = checkout;
            var res = _checkOutService.SetIsSame(User.Identity.GetUserId(), isSame);
            return Json(res);
        }
    }

}