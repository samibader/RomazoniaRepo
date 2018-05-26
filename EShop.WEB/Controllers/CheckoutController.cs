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
using EShop.DAL.Entities;
using EShop.WEB.Models;
using Microsoft.AspNet.Identity;
using Ninject.Planning.Targets;
using PayPal.Api;
using EShop.WEB.Models.Paypal;
using EShop.WEB.Util;


namespace EShop.WEB.Controllers
{
    [AjaxAuthorize]
    public class CheckoutController : BaseController
    {
        //
        // GET: /Checkout/
        //private IAddressService _addressService;
        private ICheckOutService _checkOutService;
        private ICouponService _couponService;
        private IPaypalService _paypalService;

        private CheckOutDTO checkout
        {
            get
            {
                return _checkOutService.GetSessionOrder(User.Identity.GetUserId(), CurrentLanguage,
                CurrentCurrency);
            }

        }

        public CheckoutController(IAddressService addressService, ICheckOutService checkOutService, ICouponService couponService, IPaypalService paypalService)
        {
            //_addressService = addressService;
            _checkOutService = checkOutService;
            _couponService = couponService;
            _paypalService = paypalService;
        }
        public ActionResult Index()
        {
            //if (Session["step"] == null)
            //{
            //    var step = CheckoutSteps.Coupon;
            //    Session["step"] = step;
            //}
            //if (Session["checkout"] == null)
            //{


            ViewBag.Step = (CheckoutSteps)checkout.Step;
            if (checkout.CheckOutItems.Count <= 0)
            {
                return View("EmptyCheckout");
            }
            //Session["checkout"] = checkOut;
            //}
            return View();
        }

        public ActionResult ApplyCoupon(String couponCode)
        {
            //var checkout = Session["checkout"] as CheckOutDTO;
            if (_couponService.CheckCoupon(couponCode))
            {
                CouponDTO couppDto = _couponService.GetCoupon(couponCode,CurrentLanguage,CurrentCurrency);
                var Message="";
                if (couppDto.IsPercentage)
                {
                    Message =couppDto.Value.ToString();
                    Message += " %" + "حسم";
                }
                else
                {
                    Message = "- "+ couppDto.ValueDisplay;
                }

               // checkout.Coupon = couppDto;
                //Session["checkout"] = checkout;
                _checkOutService.SetCoupon(User.Identity.GetUserId(), couponCode);
                return Json(new OperationDetails(true, Message,Utils.GetValueCurrencyDisplay(Utils.getCurrencyName(CurrentCurrency,CurrentLanguage),checkout.TotalCost)));
            }
            else
            {
                //checkout.Coupon = new CouponDTO();

                return Json(new OperationDetails(false, "كوبون غير صالح", ""));
            }

        }

        public ActionResult ShippingMethods()
        {
            //var checkout = Session["checkout"] as CheckOutDTO;
            var vm = new { ShippingMethods = checkout.ShippingMethods, ShippingCompanies = checkout.ShippingCompanies };
            return PartialView("_ShippingMethods", vm);
        }

        public ActionResult Coupon()
        {
            //var checkout = Session["checkout"] as CheckOutDTO;
            var coupon = checkout.Coupon;
            //if (coupon == null)
            //coupon = new CouponDTO();
            //checkout.Coupon = coupon;
            //Session["checkout"] = checkout;
            return PartialView("_Coupon", coupon);
        }
        public ActionResult NextStep()
        {
            //var checkout = Session["checkout"] as CheckOutDTO;
            //var step = (CheckoutSteps)Session["step"];
            var step = (CheckoutSteps)checkout.Step;
            switch (step)
            {
                //case CheckoutSteps.Coupon:
                    //step = CheckoutSteps.Address;
                    //_checkOutService.SetStep(User.Identity.GetUserId(), (int)CheckoutSteps.Address);
                    //break;
                case CheckoutSteps.Address:
                    if (checkout.AvailableAddresses.Count <= 0)
                    {
                        return Json(new OperationDetails(false, "يجب أن تملك عنوان على الأقل", ""));

                    }
                    else
                    { //step = CheckoutSteps.Payment;
                        _checkOutService.SetStep(User.Identity.GetUserId(), (int)CheckoutSteps.Payment);
                    }
                    break;
            }
            //ViewData["Step"] = checkout.Step;
            return Json(new OperationDetails(true, "", ""));
        }
        public ActionResult BackStep()
        {
            //var step = (CheckoutSteps)Session["step"];
            var step = (CheckoutSteps)checkout.Step;
            switch (step)
            {
                //case CheckoutSteps.Address:
                    //_checkOutService.SetStep(User.Identity.GetUserId(), (int)CheckoutSteps.Coupon);

                    //step = CheckoutSteps.Coupon;
                    //break;
                case CheckoutSteps.Payment:
                    _checkOutService.SetStep(User.Identity.GetUserId(), (int)CheckoutSteps.Address);

                    //step = CheckoutSteps.Address;
                    break;
            }
            //Session["step"] = step;
            return Json(new OperationDetails(true, "", ""));
        }

        public ActionResult Payment()
        {
            //var checkout = Session["checkout"] as CheckOutDTO;
            Mapper.Initialize(cfg => cfg.CreateMap<CheckOutDTO, CheckoutVM>());
            CheckoutVM vm = Mapper.Map<CheckOutDTO, CheckoutVM>(checkout);
            return PartialView("_Payment", vm);
        }

        public ActionResult ChangeShippingMethod(long id)
        {
            //var checkout = Session["checkout"] as CheckOutDTO;
            var result = new OperationDetails(true, "تم تعديل الطريقة بنجاح", "");

            if (id == 0)
            {
                //checkout.ShippingMethod = Common.ShippingMethods.Free;
                result = _checkOutService.SetShippingMethod(User.Identity.GetUserId(), Common.ShippingMethods.Free);
            }
            else if (id == -1)
            {
                //checkout.ShippingMethod = Common.ShippingMethods.ByHand;
                result = _checkOutService.SetShippingMethod(User.Identity.GetUserId(), Common.ShippingMethods.ByHand);

            }
            else
            {
                //checkout.ShippingMethod = Common.ShippingMethods.WithCompany;
                result = _checkOutService.SetShippingMethod(User.Identity.GetUserId(), Common.ShippingMethods.WithCompany);

                if (result.Succedeed)
                {
                    result = _checkOutService.SetShippingCompany(User.Identity.GetUserId(), id);
                }
                //checkout.SelectShippingCompany = checkout.ShippingCompanies.Where(sc => sc.Id == id).FirstOrDefault();
                //checkout.TotalCost = checkout.SelectShippingCompany.ShippingCost + checkout.SubTotalCost;
            }
            //Session["checkout"] = checkout;

            return Json(result);
        }
        public ActionResult ChangeBillingMethod(BillingMethods id)
        {
            //var checkout = Session["checkout"] as CheckOutDTO;

            //checkout.SelectedBillingMethod = id;
            var result = _checkOutService.SetBillingMethod(User.Identity.GetUserId(), id);
            //Session["checkout"] = checkout;

            return Json(result);
        }

        public ActionResult DeleteCoupon()
        {
            var res = _checkOutService.SetCoupon(User.Identity.GetUserId(), "");
            return Json(new OperationDetails(res.Succedeed,res.Message,Utils.GetValueCurrencyDisplay(Utils.getCurrencyName(CurrentCurrency,CurrentLanguage),checkout.TotalCost)));

        }
        private PayPal.Api.Payment payment;
        public ActionResult PaymentWithPaypal()
        {
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Checkout/PaymentWithPayPal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var model = _checkOutService.GetSessionOrder(User.Identity.GetUserId(),Langs.English,EShop.Common.Currency.Dollar);
                    var createdPayment = _paypalService.CreatePayment(apiContext, baseURI + "guid=" + guid, model);
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    Session.Add(guid, createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executedPayment = _paypalService.ExecutePayment(apiContext, payerId, Session[guid] as string,ref payment);
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                    else
                    {

                        //var model = Session["SubscriptionModel"] as PaypalTransactionViewModel;
                        //// SUCCESSFUL TRANSACTION
                        //SubscriptionInvoice inv = new SubscriptionInvoice()
                        //{
                        //    Amount = model.Amount,
                        //    CycleStartDate = DateTime.Now,
                        //    CycleEndDate = DateTime.Now.AddMonths(1),
                        //    Description = model.Description,
                        //    InvoiceDate = model.InvoiceDate,
                        //    InvoiceNo = model.InvoiceNo,
                        //    ItemCurrency = model.Items[0].Currency,
                        //    ItemDescription = model.Items[0].Description,
                        //    ItemName = model.Items[0].Name,
                        //    ItemPrice = model.Items[0].Price,
                        //    ItemQuantity = model.Items[0].Quantity,
                        //    Paid = true,
                        //    PayerID = executedPayment.payer.payer_info.payer_id,
                        //    PaymentId = executedPayment.id,
                        //    PaymentToken = executedPayment.token,
                        //    TransactionId = executedPayment.transactions[0].related_resources[0].sale.id,
                        //    UserId = model.AppUser.Id,
                        //    PayPalJSON = executedPayment.ConvertToJson()
                        //};
                        //_unitOfWork.SubscriptionInvoiceRepository.Insert(inv);
                        //_unitOfWork.Save();
                        //var user = UserManager.FindById(User.Identity.GetUserId());
                        //user.IsPremium = true;
                        //user.PremiumExpiryDate = inv.CycleEndDate;
                        //UserManager.Update(user);
                        //return View("SuccessView", inv);
                        return View("SuccessView");
                    }

                }
            }
                catch(PayPal.PaymentsException ex)
            {
                PaypalLogger.Log("Error" + ex.Details.ConvertToJson());
                return View("FailureView");
            }
            catch (Exception ex)
            {
                PaypalLogger.Log("Error" + ex.Message);
                return View("FailureView");
            }
            //return View("SuccessView");
        }
    }
}