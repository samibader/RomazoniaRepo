using EShop.BLL.Interfaces;
using EShop.Common;
using EShop.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using EShop.BLL.DTO;
using AutoMapper;
using EShop.BLL.Infrastructure;
using System.Globalization;
using System.Threading.Tasks;

namespace EShop.WEB.Controllers
{
    [Authorize(Roles = "AdminRole")]
    public class ManageOrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public ManageOrderController(IOrderService _orderService)
        {
            this._orderService = _orderService;
        }
        //
        // GET: /manageOrder/
        public ActionResult Index(String cn, string inv, string dc, string dm, Double? t, OrderStates? s, int PageSize = PAGE_SIZE, int page = 1, Sorts sb = Sorts.OrderIdUp, Int64 Id = 0)
        {
            page = page > 1 ? page : 1;
            PageSize = PageSize > 0 ? PageSize : PAGE_SIZE;
            DateTime? DateCreation;
            DateTime? DateModified;
            if (!String.IsNullOrWhiteSpace(dc))
                DateCreation = DateTime.ParseExact(dc, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            else
                DateCreation = null;

            if (!String.IsNullOrWhiteSpace(dm))
                DateModified = DateTime.ParseExact(dm, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            else
                DateModified = null;

            List<OrderDTO> orderDTOs = _orderService.Filter(Id, s, cn, t, DateCreation, DateModified, sb, inv, CurrentLanguage, CurrentCurrency);
            Mapper.Initialize(c => c.CreateMap<OrderDTO, OrderVM>());
            List<OrderVM> orderVMs = Mapper.Map<List<OrderDTO>, List<OrderVM>>(orderDTOs);

            ManageOrderVM orders = new ManageOrderVM
            {
                Orders = orderVMs.ToPagedList(page, PageSize),
                filters = new OrderFiltersVM
                {
                    Id = Id,
                    PageNum = page,
                    PageSize = PageSize,
                    SortBy = sb,
                    Status = s,
                    Total = t,
                    UserId = cn,
                    CreationDate = DateCreation,
                    DateModified = DateModified,
                    Invoice = inv

                }
            };
            return View(orders);
        }

        [HttpPost]
        public ActionResult Index(String cn, string inv, Double? t, string dc, string dm, OrderStates? s, Sorts sb = Sorts.OrderIdUp, int page = 1, int PageSize = PAGE_SIZE, Int64 Id = 0)
        {
            //send values of filters to service
            DateTime? DateCreation;
            DateTime? DateModified;
            if (!String.IsNullOrWhiteSpace(dc))
                DateCreation = DateTime.ParseExact(dc, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            else
                DateCreation = null;

            if (!String.IsNullOrWhiteSpace(dm))
                DateModified = DateTime.ParseExact(dm, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            else
                DateModified = null;

            List<OrderDTO> orderDTOs = _orderService.Filter(Id, s, cn, t, DateCreation, DateModified, sb, inv, CurrentLanguage, CurrentCurrency);
            ManageOrderVM orders = new ManageOrderVM();

            Mapper.Initialize(c => c.CreateMap<OrderDTO, OrderVM>());
            List<OrderVM> ordersVM = Mapper.Map<List<OrderDTO>, List<OrderVM>>(orderDTOs);
            OrderFiltersVM filters = new OrderFiltersVM();

            filters.Id = Id;
            filters.PageNum = page;
            filters.PageSize = PageSize;
            filters.SortBy = sb;
            filters.Status = s;
            filters.Total = t;
            filters.UserId = cn;
            filters.CreationDate = DateCreation;
            filters.DateModified = DateModified;
            filters.Invoice = inv;

            orders.filters = filters;
            orders.Orders = ordersVM.ToPagedList(page, PageSize);
            return View(orders);


        }
        public ActionResult Add(long Id)
        {
            OrderDTO orderDTO = _orderService.GetOrderById(Id, CurrentLanguage, CurrentCurrency);
            Mapper.Initialize(c => c.CreateMap<OrderDTO, OrderVM>());
            OrderVM order = Mapper.Map<OrderDTO, OrderVM>(orderDTO);

            return View(order);
        }
        public ActionResult ViewOrder(long Id)
        {
            OrderDTO orderDTO = _orderService.GetOrderById(Id, CurrentLanguage, CurrentCurrency);
            Mapper.Initialize(c => c.CreateMap<OrderDTO, OrderVM>());
            OrderVM order = Mapper.Map<OrderDTO, OrderVM>(orderDTO);
            order.CurrencyName = Utils.getCurrencyName(CurrentCurrency, CurrentLanguage);
            return View(order);
        }

        private string GetInvoiceStringForEmail(OrderDTO dto)
        {
            string result = "";
            foreach (var item in dto.OrderItems)
            {
                result += "<tr><td>" + item.Name + "<br/>" + (!String.IsNullOrWhiteSpace(item.ColorEnglish) ? "اللون :" + item.ColorEnglish : "") + "<br/>" + (!String.IsNullOrWhiteSpace(item.SizeEnglish) ? "اللون :" + item.SizeEnglish : "") + "</td>" +
                    "<td>" + item.UnitPriceDisplay + "</td>" +
                    "<td>" + item.Quantity + "</td>" +
                    "<td>" + item.TotalPriceDisplay + "</td></tr>";
            }
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> ViewOrder(OrderStates state, string notify, string comment, long Id)
        {
            bool noti = string.IsNullOrEmpty(notify) ? false : true;
            OrderDTO dto = _orderService.GetOrderById(Id,CurrentLanguage,CurrentCurrency);
            string userId = dto.UserId;
            string userEmail = UserManager.Users.Where(c => c.Id == userId).FirstOrDefault().Email;
            string userName = UserManager.Users.Where(c => c.Id == userId).FirstOrDefault().FirstName + " " + UserManager.Users.Where(c => c.Id == userId).FirstOrDefault().LastName;
            
            OperationDetails op = _orderService.ChangeState(state, comment, noti, Id);
            if (op.Succedeed)
            {
                if (state != OrderStates.paid)
                {
                    string email = Utils.buildEmail(state);
                    await UserManager.SendEmailAsync(userId, "تنبيه", email);
                }
                else if (state == OrderStates.paid)
                {
                    OrderDTO order = _orderService.GetOrderById(Id, CurrentLanguage, CurrentCurrency);
                    string copun = "";
                    string shipingCost = "";
                    if (!String.IsNullOrEmpty(order.CouponCode))
                        copun = order.CouponCurrency == "%" ? order.CouponCurrency + order.CouponValue : order.CouponValueDisplay;
                    if (order.ShippingCost != 0)
                        shipingCost = order.ShippingCostDisplay;
                    string invoice = GetInvoiceStringForEmail(order);
                    string inviceId = order.InvoiceId;
                    string totalPrice = order.TotalDisplay;
                    string email = Utils.buildInvoiceEmail(userId, userName, invoice, inviceId, totalPrice, copun, shipingCost);
                    await UserManager.SendEmailAsync(userId, "تنبيه", email);

                }
            }
            return Json(new { Succedeed = op.Succedeed, message = op.Message });
        }

        [HttpPost]
        public async Task<JsonResult> Invoice(long orderId)
        {
            
            OperationDetails op = _orderService.ChangeState(OrderStates.paid, "", true, orderId);
            if (op.Succedeed)
            {
                OrderDTO order = _orderService.GetOrderById(orderId, CurrentLanguage, CurrentCurrency);
                string userId = order.UserId;
                // string userEmail = UserManager.Users.Where(c => c.Id == userId).FirstOrDefault().Email;
                string userName = UserManager.Users.Where(c => c.Id == userId).FirstOrDefault().FirstName + " " + UserManager.Users.Where(c => c.Id == userId).FirstOrDefault().LastName;
                string copun = "";
                string shipingCost = "";
                if (!String.IsNullOrEmpty(order.CouponCode))
                    copun = order.CouponCurrency == "%" ? order.CouponCurrency + order.CouponValue : order.CouponValueDisplay;
                if (order.ShippingCost != 0)
                    shipingCost = order.ShippingCostDisplay;
                string invoice = GetInvoiceStringForEmail(order);
                string inviceId = order.InvoiceId;
                string totalPrice = order.TotalDisplay;
                string email = Utils.buildInvoiceEmail(userId, userName, invoice, inviceId, totalPrice, copun, shipingCost);
                await UserManager.SendEmailAsync(userId, "تنبيه", email);
            }
            return Json(new { Succedeed = op.Succedeed, message = op.Message });
        }

        [HttpPost]
        public ActionResult Delete(long Id)
        {

            OperationDetails op = _orderService.DeleteOrder(Id);
            return Json(new { Succedeed = op.Succedeed, message = op.Message });
        }
        public ActionResult BankInformation()
        {
            var descript = _orderService.GetBankInformation();
            Mapper.Initialize(c => c.CreateMap<BankInformationDTO, BankInformationVM>());
            BankInformationVM desc = Mapper.Map<BankInformationDTO, BankInformationVM>(descript);
            return View(desc);
        }

        [HttpPost]
        public ActionResult BankInformation(BankInformationVM descript)
        {
            Mapper.Initialize(c => c.CreateMap<BankInformationVM, BankInformationDTO>());
            BankInformationDTO desc = Mapper.Map<BankInformationVM, BankInformationDTO>(descript);

            OperationDetails op = _orderService.addBankInformation(desc);
            return Json(new { Succedeed = op.Succedeed, message = op.Message });

        }
    }
}