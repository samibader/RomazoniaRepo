using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using EShop.Common;
using EShop.DAL.Entities;
using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork unitOfWork { get; set; }
        ICartService cartService { get; set; }
        IAddressService addressService { get; set; }
        private ICheckOutService checkOutService { get; set; }
        private IUserService _userService;

        public OrderService(IUnitOfWork uow, AddressService _addressService, CartService _cartService, CheckOutService _checkOutService, IUserService _userService)
        {
            unitOfWork = uow;
            addressService = _addressService;
            cartService = _cartService;
            checkOutService = _checkOutService;
            this._userService = _userService;
        }


        public OperationDetails DeleteOrder(long Id)
        {
            Order order = unitOfWork.OrderRepository.Get(c => c.Id == Id).FirstOrDefault();
            if (order == null)
            {
                return new OperationDetails(false, "الطلب الذي تحاول حذفه غير موجود", "");
            }
            List<OrderHistory> histories = unitOfWork.OrderHistoryRepository.Get(c => c.OrderId == Id).ToList();
            if (histories != null)
            {
                foreach (var history in histories)
                {
                    unitOfWork.OrderHistoryRepository.Delete(history);
                }
                unitOfWork.Save();
            }
            unitOfWork.OrderRepository.Delete(order);
            unitOfWork.Save();
            return new OperationDetails(true, "تم حذف الطلب بنجاح", "");
        }



        public OperationDetails addInvoiceNumberToOrder(string invoice, long orderId)
        {
            Order order = unitOfWork.OrderRepository.Get(c => c.Id == orderId).FirstOrDefault();
            if (order == null)
                return new OperationDetails(false, "حدث خطأ في تثبيت رقم الفاتورة", "");
            order.InvoiceId = invoice;
            unitOfWork.OrderRepository.Update(order);
            unitOfWork.Save();
            return new OperationDetails(true, "تم تثبيت رقم الفاتورة بنجاح", "");
        }
        public OperationDetails ChangeState(OrderStates state, string desc, bool notifyUser, long orderId)
        {
            string invoice = "";
            OrderHistory orderHistory = new OrderHistory();
            orderHistory.OrderId = orderId;
            orderHistory.State = state;
            orderHistory.Description = "";
            if (state == OrderStates.paid)
            {
                invoice = Utils.generateInvoice();
                addInvoiceNumberToOrder(invoice, orderId);
                orderHistory.Description += "الطلب الان في حالة الدفع " + Environment.NewLine + "رقم الفاتورة: " + invoice + Environment.NewLine;
            }

            orderHistory.DateAdded = DateTime.Now;
            orderHistory.Description += desc;
            orderHistory.NotifyUser = notifyUser;
            orderHistory = unitOfWork.OrderHistoryRepository.Insert(orderHistory);
            Order order = unitOfWork.OrderRepository.GetByID(orderId);
            order.Status = state;
            unitOfWork.OrderRepository.Update(order);
            unitOfWork.Save();
            //if(state == OrderStates.paid)bys
            //notifyUser(userId,invoice);//or we can send the description instead of invoice
            //else
            // notifyUser(userId,"");
            return new OperationDetails(true, "تم تغيير الحالة بنجاح", "");
        }

        public List<OrderDTO> Filter(long? orderId, OrderStates? state, string userId, double? total, DateTime? dateAdded, DateTime? dateModefied, Sorts s, string inv, Langs l, Currency cu)
        {
            List<long> orderIds = unitOfWork.OrderRepository.Get(c => true).Select(c => c.Id).ToList();

            if (orderId != null && orderId != 0)
                FilterId(ref orderIds, orderId.Value);
            if (state != null)
                FilterState(ref orderIds, state.Value);
            if (!String.IsNullOrEmpty(userId))
                FilterUser(ref orderIds, userId);
            if (total != null && total != 0)
                FilterTotal(ref orderIds, total.Value);

            if (dateAdded != null)
                FilterDate(ref orderIds, dateAdded.Value, true);
            if (dateModefied != null)
                FilterDate(ref orderIds, dateModefied.Value, false);
            if (!String.IsNullOrEmpty(inv))
                FilterInv(ref orderIds, inv);

            List<OrderDTO> orders = new List<OrderDTO>();

            for (int i = 0; i < orderIds.Count; i++)
            {
                orders.Add(GetOrderById(orderIds[i], l, cu));
            }
            sortOrders(ref orders, s);
            return orders;
        }

        private void FilterInv(ref List<long> orderIds, string inv)
        {
            List<long> ids = unitOfWork.OrderRepository.
                                        Get(c => c.InvoiceId == inv).
                                            Select(op => op.Id).Distinct().ToList();
            orderIds = orderIds.Intersect(ids).ToList();
        }


        private void sortOrders(ref List<OrderDTO> orders, Sorts s)
        {
            switch (s)
            {
                case Sorts.OrderIdDown: orders.Sort((a, b) => -1 * a.Id.CompareTo(b.Id));
                    break;
                case Sorts.OrderIdUp: orders.Sort((a, b) => a.Id.CompareTo(b.Id));
                    break;
                case Sorts.OrederStatusDown: orders.Sort((a, b) => -1 * a.Status.CompareTo(b.Status));
                    break;
                case Sorts.OrederStatusUp: orders.Sort((a, b) => a.Status.CompareTo(b.Status));
                    break;
                case Sorts.TotalUp: orders.Sort((a, b) => a.Total.CompareTo(b.Total)); //products.OrderBy(op => op.Price);
                    break;
                case Sorts.TotalDown: orders.Sort((a, b) => -1 * a.Total.CompareTo(b.Total));
                    break;
                case Sorts.DateAddedDown: orders.Sort((a, b) => a.CreationDate.Value.CompareTo(b.CreationDate)); //products.OrderBy(op => op.Price);
                    break;
                case Sorts.DateAddedUp: orders.Sort((a, b) => -1 * a.CreationDate.Value.CompareTo(b.CreationDate));
                    break;
                case Sorts.InvoiceUp: orders.Sort((a, b) => a.InvoiceId.CompareTo(b.InvoiceId)); //products.OrderBy(op => op.Price);
                    break;
                case Sorts.InvoiceDown: orders.Sort((a, b) => -1 * a.InvoiceId.CompareTo(b.InvoiceId));
                    break;
                // case Sorts.DateAddedUp: orders.OrderBy(c=>c.CreationDate);
                //   break;
                //case Sorts.DateAddedDown: orders.OrderByDescending(c=>c.CreationDate);
                //  break;
                //case Sorts.DateModifiedUp: orders.OrderBy(c => c.DateModified);
                //  break;
                //case Sorts.DateModifiedDown: orders.OrderByDescending(c => c.DateModified);
                //  break;
                case Sorts.CustomerNameUp: orders.OrderBy(c => c.UserId);
                    break;
                case Sorts.CustomerNameDown: orders.OrderByDescending(c => c.UserId);
                    break;

            }

        }

        private void FilterId(ref List<long> orderIds, long id)
        {
            List<long> ids = unitOfWork.OrderRepository.
                                        Get(c => c.Id == id).
                                            Select(op => op.Id).Distinct().ToList();
            orderIds = orderIds.Intersect(ids).ToList();
        }
        private void FilterUser(ref List<long> orderIds, string userId)
        {
            List<long> ids = unitOfWork.OrderRepository.
                                        Get(c => c.UserId == userId).
                                            Select(op => op.Id).Distinct().ToList();
            orderIds = orderIds.Intersect(ids).ToList();
        }
        private void FilterTotal(ref List<long> orderIds, double total)
        {
            List<long> ids = unitOfWork.OrderRepository.
                                        Get(c => c.Total == total).
                                            Select(op => op.Id).Distinct().ToList();
            orderIds = orderIds.Intersect(ids).ToList();
        }
        private void FilterState(ref List<long> orderIds, OrderStates state)
        {
            List<long> ids = unitOfWork.OrderRepository.
                                        Get(c => c.Status == state).
                                            Select(op => op.Id).Distinct().ToList();
            orderIds = orderIds.Intersect(ids).ToList();
        }
        private void FilterDate(ref List<long> productIds, DateTime date, bool isDateAdded)
        {
            string dateStr = date.ToString().Substring(0, 8);
            List<long> ids;
            if (isDateAdded)
            {

                ids = unitOfWork.OrderRepository.
                                        Get(c => (c.CreationDate.Value.Year == date.Year) && (c.CreationDate.Value.Month == date.Month) && (c.CreationDate.Value.Day == date.Day)).
                                            Select(op => op.Id).Distinct().ToList();
            }
            else
            {
                ids = unitOfWork.OrderRepository.
                                        Get(c => (c.DateModified.Value.Year == date.Year) && (c.DateModified.Value.Month == date.Month) && (c.DateModified.Value.Day == date.Day)).
                                            Select(op => op.Id).Distinct().ToList();
            }

            productIds = productIds.Intersect(ids).ToList();
        }
        public OperationDetails AddOrder(string userId,string userIp,string userAgent,Langs l, Currency currency)
        {
            Order order = new Order();
            CheckOutDTO checkOutDTO = checkOutService.GetSessionOrder(userId, Langs.English, Currency.Rial);
            order.ShippingCost = checkOutDTO.ShippingCost;
            order.Status = OrderStates.pending;
            order.UserId = userId;
            order.CouponCode = checkOutDTO.Coupon.Code;
            order.CouponValue = checkOutDTO.Coupon.Value;
            order.CouponIsPercentage = checkOutDTO.Coupon.IsPercentage;
            order.UserAgent = userAgent;
            order.UserIpAddress = userIp;
            order.CreationDate = DateTime.Now;
            order.DateModified = DateTime.Now;
            order.SubTotal = checkOutDTO.SubTotalCost;
            order.Total = checkOutDTO.TotalCost;
            order.ArabicBillingName = GetMethodName((long)checkOutDTO.SelectedBillingMethod, Langs.Arabic);
            order.EnglishBillingName = GetMethodName((long)checkOutDTO.SelectedBillingMethod, Langs.English);
            order.ArabicShippingName = GetMethodName((long)checkOutDTO.ShippingMethod, Langs.Arabic);
            order.EnglishShippingName = GetMethodName((long)checkOutDTO.ShippingMethod, Langs.English);

            if (checkOutDTO.ShippingMethod == ShippingMethods.WithCompany)
            {
                long coid = checkOutDTO.SelectShippingCompany.Id;
                order.ArabicCompanyName = unitOfWork.ShippingCompanyDescriptionRepository
                    .Get(c => c.ShippingCompanyId == coid && c.LanguageId == (long)Langs.Arabic).FirstOrDefault().Name;
                order.EnglishCompanyName = unitOfWork.ShippingCompanyDescriptionRepository
                    .Get(c => c.ShippingCompanyId == coid && c.LanguageId == (long)Langs.English).FirstOrDefault().Name;
            }

            order.OrderItems = AddOrderItems(checkOutDTO.CheckOutItems);
            order.ShippingAddress =
                AddOrderShippingAddress(addressService.GetAddressById(checkOutDTO.SelectedShippingAddressId));
            order.BillingAddress =
                AddOrderBillingAddress(addressService.GetAddressById(checkOutDTO.SelectedBillingAddressId));
            order = unitOfWork.OrderRepository.Insert(order);
            string billingMetaDesc = unitOfWork.ShippingBillingMethodRepository
                    .GetByID((long)checkOutDTO.SelectedBillingMethod)
                    .ShippingBillingMethodDescriptions.FirstOrDefault(c => c.LanguageId == (long)Langs.English).MetaDescription;
            cartService.RemoveAll(checkOutDTO.UserId);
            unitOfWork.Save();
            ChangeState(OrderStates.pending, billingMetaDesc, true, order.Id);
            //unitOfWork.Save();
            CheckOut userCheckOut = unitOfWork.CheckOutRepository.GetByID(order.UserId);
            unitOfWork.CheckOutRepository.Delete(userCheckOut);
            unitOfWork.Save();
            return new OperationDetails(true, "تمت الإضافة بنجاح", order.Id.ToString());
        }

        public OperationDetails AddOrderWithInfo(string userId,string userIp,string userAgent, Langs l, Currency currency, AddtionalInfoDTO info)
        {
            var res = AddOrder(userId,userIp,userAgent, l, currency);
            if (res.Succedeed)
            {
                var id = Int64.Parse(res.Property);
                var order = unitOfWork.OrderHistoryRepository.Get(ord => ord.OrderId == id).FirstOrDefault();
                order.Description += String.Format(@"<div><p>رقم الحساب:{0}</p><p>المبلغ المحول:{1}</p>المرسل:{2}<p>اسم البنك:{3}</p><p>تاريخ الإرسال:{4}</p></div>", info.AccountNumber, info.Amount,
                    info.AutherName, info.BankName, info.TransferDate);
                unitOfWork.Save();
                return new OperationDetails(true, "", res.Property);
            }
            else
            {
                return res;
            }
        }
        public BankInformationDTO GetBankInformation()
        {
            var desc = unitOfWork.ShippingBillingMethodDescriptionRepository.Get(c => c.ShippingBillingMethodId == (long)BillingMethods.BankTransfer).FirstOrDefault();
            BankInformationDTO dto = new BankInformationDTO();
            dto.Description = desc.MetaDescription;
            return dto;

        }

        private string GetMethodName(long methodId, Langs l)
        {
            return unitOfWork.ShippingBillingMethodRepository.
                             GetByID(methodId).
                             ShippingBillingMethodDescriptions.
                             Where(c => c.LanguageId == (long)l).FirstOrDefault().Name;
        }
        private List<OrderItem> AddOrderItems(List<OrderItemDTO> checkOutItems)
        {
            List<OrderItem> items = new List<OrderItem>();
            foreach (var item in checkOutItems)
            {
                items.Add(AddOrderItem(item));
            }
            return items;
        }
        private OrderItem AddOrderItem(OrderItemDTO checkOutItem)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.ImageUrl = checkOutItem.ImageUrl;
            orderItem.Name = checkOutItem.Name;
            orderItem.SKU = checkOutItem.SKU;

            orderItem.ColorArabic = checkOutItem.ColorArabic;
            orderItem.ColorEnglish = checkOutItem.ColorEnglish;
            orderItem.SizeArabic = checkOutItem.SizeArabic;
            orderItem.SizeEnglish = checkOutItem.SizeEnglish;

            orderItem.Quantity = checkOutItem.Quantity;
            orderItem.UnitPrice = checkOutItem.UnitPrice;
            orderItem.TotalPrice = checkOutItem.TotalPrice;
            orderItem = unitOfWork.OrderItemRepository.Insert(orderItem);
            return orderItem;
        }
        private BillingAddress AddOrderBillingAddress(AddressDTO addressDTO)
        {
            BillingAddress billingAddress = new BillingAddress();
            billingAddress.Address1 = addressDTO.Address1;
            billingAddress.City = addressDTO.City;
            billingAddress.Country = addressDTO.Country;
            billingAddress.FirstName = addressDTO.FirstName;
            billingAddress.LastName = addressDTO.LastName;
            billingAddress.Phone = addressDTO.Phone;
            billingAddress.PostCode = addressDTO.PostCode;

            billingAddress = unitOfWork.BillingAddressRepository.Insert(billingAddress);
            return billingAddress;
        }
        private ShippingAddress AddOrderShippingAddress(AddressDTO addressDTO)
        {
            ShippingAddress shippingAddress = new ShippingAddress();
            shippingAddress.Address1 = addressDTO.Address1;
            shippingAddress.City = addressDTO.City;
            shippingAddress.Country = addressDTO.Country;
            shippingAddress.FirstName = addressDTO.FirstName;
            shippingAddress.LastName = addressDTO.LastName;
            shippingAddress.Phone = addressDTO.Phone;
            shippingAddress.PostCode = addressDTO.PostCode;
            shippingAddress = unitOfWork.ShippingAddressRepository.Insert(shippingAddress);
            return shippingAddress;
        }
        public List<OrderDTO> GetAllOrderDTO(Langs l, Currency cu)
        {
            List<OrderDTO> orders = new List<OrderDTO>();
            List<long> orderIds = unitOfWork.OrderRepository.Get(c => true).Select(c => c.Id).ToList();
            foreach (var id in orderIds)
            {
                orders.Add(GetOrderById(id, l, cu));
            }
            return orders;
        }
        public List<OrderDTO> GetUserOrdersByState(string userId, OrderStates state, Langs l, Currency cu)
        {
            if (state == OrderStates.All)
                return GetUserOrders(userId, l, cu);
            if (state == OrderStates.Active)
                return GetUserOrders(userId, l, cu).Where(c => c.Status != OrderStates.complete && c.Status != OrderStates.canceled).Distinct().ToList();
            List<long> orderIds = unitOfWork.OrderRepository.Get(c => c.UserId == userId && c.Status == state).Select(c => c.Id).ToList();
            List<OrderDTO> orderDTOs = new List<OrderDTO>();
            foreach (var orderId in orderIds)
            {
                orderDTOs.Add(GetOrderById(orderId, l, cu));
            }
            sortOrders(ref orderDTOs, Sorts.DateAddedUp);
            return orderDTOs;
        }
        public List<OrderDTO> GetUserOrders(string userId, Langs l, Currency cu)
        {
            List<OrderDTO> orderDTOs = GetUserOrdersByState(userId, OrderStates.canceled, l, cu).
                                    Union(GetUserOrdersByState(userId, OrderStates.pending, l, cu)).
                                    Union(GetUserOrdersByState(userId, OrderStates.complete, l, cu)).
                                    Union(GetUserOrdersByState(userId, OrderStates.paid, l, cu)).
                                    Union(GetUserOrdersByState(userId, OrderStates.pendingShipping, l, cu)).
                                    Union(GetUserOrdersByState(userId, OrderStates.shipped, l, cu)).ToList();

            sortOrders(ref orderDTOs, Sorts.DateAddedUp);
            return orderDTOs;
        }
        public OrderDTO GetOrderById(long id, Langs l, Currency c)
        {
            OrderDTO orderDTO = new OrderDTO();
            Order order = unitOfWork.OrderRepository.GetByID(id);
            orderDTO.BillingAddress = GetOrderAddress(order.BillingAddress);
            orderDTO.ShippingAddress = GetOrderAddress(order.ShippingAddress);
            orderDTO.OrderItems = GetOrderItems(order.Id, l, c);
            orderDTO.CurrencyName = Utils.getCurrencyName(c, l);
            orderDTO.InvoiceId = order.InvoiceId;
            orderDTO.ClosingDate = order.ClosingDate;
            orderDTO.UserIpAddress = order.UserIpAddress;
            orderDTO.UserAgent = order.UserAgent;
            if (!String.IsNullOrEmpty(order.CouponCode))
            {
                orderDTO.CouponCode = order.CouponCode;
                orderDTO.CouponValue = order.CouponValue;
                orderDTO.CouponCurrency = orderDTO.CurrencyName;
                if (!order.CouponIsPercentage)
                {
                    orderDTO.CouponValue = Utils.getCurrency(c, l, order.CouponValue).Item1;
                    orderDTO.CouponCurrency = "-";
                }
                else
                    orderDTO.CouponCurrency = "%";
            }

            // get Order Owner
            var user = _userService.getuserById(order.UserId);

            orderDTO.CreationDate = order.CreationDate.Value;
            orderDTO.DateModified = order.DateModified;
            orderDTO.Discount = order.Discount;
            orderDTO.Id = order.Id;
            orderDTO.InvoiceId = order.InvoiceId;
            orderDTO.PaymentDate = order.PaymentDate;
            orderDTO.PaymentNotes = order.PaymentNotes;
            orderDTO.PaymentDate = order.PaymentDate;
            orderDTO.ShippingCost = order.ShippingCost;
            orderDTO.Status = order.Status;
            orderDTO.SubTotal = Utils.getCurrency(c, l, order.SubTotal).Item1;
            orderDTO.Total = Utils.getCurrency(c, l, order.Total).Item1;
            orderDTO.TransactionDetails = order.TransactionDetails;
            orderDTO.TransactionId = order.TransactionId;
            orderDTO.UserId = order.UserId;
            orderDTO.Phone = order.ShippingAddress.Phone;
            orderDTO.UserName = user.FirstName + " " + user.LastName;
            orderDTO.Email = user.Email;
            orderDTO.OrderHistories = GetOrderHistory(id);


            ShippingMethodDTO shippingMethodDTO = new ShippingMethodDTO();
            BillingMethodDTO billingMethodDTO = new BillingMethodDTO();
            ShippingCompanyDTO shippingCompanyDTO = new ShippingCompanyDTO();
            if (l == Langs.English)
            {
                shippingMethodDTO.Name = order.EnglishShippingName;
                billingMethodDTO.Name = order.EnglishBillingName;
                shippingCompanyDTO.Name = order.EnglishCompanyName;
            }
            else
            {
                shippingMethodDTO.Name = order.ArabicShippingName;
                billingMethodDTO.Name = order.ArabicBillingName;
                shippingCompanyDTO.Name = order.ArabicCompanyName;
            }

            orderDTO.ShippingMethod = shippingMethodDTO;
            orderDTO.BillingMethod = billingMethodDTO;
            return orderDTO;
        }
        private List<OrderHistoryDTO> GetOrderHistory(long orderId)
        {
            List<OrderHistory> orderHistories = unitOfWork.OrderHistoryRepository.Get(c => c.OrderId == orderId).ToList();
            List<OrderHistoryDTO> orderHistoryDTOs = new List<OrderHistoryDTO>();
            foreach (var orderHistory in orderHistories)
            {
                OrderHistoryDTO orderHistoryDTO = new OrderHistoryDTO();
                orderHistoryDTO.DateAdded = orderHistory.DateAdded;
                orderHistoryDTO.Description = orderHistory.Description;
                orderHistoryDTO.Id = orderHistory.Id;
                orderHistoryDTO.NotifyUser = orderHistory.NotifyUser;
                orderHistoryDTO.State = orderHistory.State;
                orderHistoryDTOs.Add(orderHistoryDTO);
            }
            return orderHistoryDTOs;
        }
        private List<OrderItemDTO> GetOrderItems(long orderId, Langs
            l, Currency cu)
        {
            List<OrderItemDTO> orderItemDTOs = new List<OrderItemDTO>();
            List<long> orderItemIds = unitOfWork.OrderItemRepository.Get(c => c.OrderId == orderId).Select(c => c.Id).ToList();
            foreach (var orderItemId in orderItemIds)
            {
                orderItemDTOs.Add(GetOrderItemById(orderItemId, l, cu));
            }
            return orderItemDTOs;
        }
        private OrderItemDTO GetOrderItemById(long orderItemId, Langs
            l, Currency cu)
        {
            OrderItem orderItem = unitOfWork.OrderItemRepository.GetByID(orderItemId);
            OrderItemDTO orderItemDTO = new OrderItemDTO();
            orderItemDTO.Id = orderItem.Id;
            orderItemDTO.ImageUrl = orderItem.ImageUrl;
            orderItemDTO.Name = orderItem.Name;


            if (l == Langs.English)
            {
                orderItemDTO.ColorEnglish = orderItem.ColorEnglish;
                orderItemDTO.SizeEnglish = orderItem.SizeEnglish;
            }
            else
            {
                orderItemDTO.ColorEnglish = orderItem.ColorArabic;
                orderItemDTO.SizeEnglish = orderItem.SizeArabic;
            }



            orderItemDTO.OrderId = orderItem.OrderId;
            orderItemDTO.Quantity = orderItem.Quantity;
            orderItemDTO.TotalPrice = Utils.getCurrency(cu, l, orderItem.TotalPrice).Item1;
            orderItemDTO.UnitPrice = Utils.getCurrency(cu, l, orderItem.UnitPrice).Item1;
            orderItemDTO.CurrencyName = Utils.getCurrencyName(cu, l);
            orderItemDTO.SKU = orderItem.SKU;
            return orderItemDTO;
        }
        private AddressDTO GetOrderAddress(BillingAddress billingAddress)
        {
            AddressDTO addressDTO = new AddressDTO();
            addressDTO.Address1 = billingAddress.Address1;
            addressDTO.City = billingAddress.City;
            addressDTO.Country = billingAddress.Country;
            addressDTO.FirstName = billingAddress.FirstName;
            addressDTO.Id = billingAddress.Id;
            addressDTO.LastName = billingAddress.LastName;
            addressDTO.Phone = billingAddress.Phone;
            addressDTO.PostCode = billingAddress.PostCode;
            return addressDTO;
        }
        private AddressDTO GetOrderAddress(ShippingAddress shippingAddress)
        {
            AddressDTO addressDTO = new AddressDTO();
            addressDTO.Address1 = shippingAddress.Address1;
            addressDTO.City = shippingAddress.City;
            addressDTO.Country = shippingAddress.Country;
            addressDTO.FirstName = shippingAddress.FirstName;
            addressDTO.Id = shippingAddress.Id;
            addressDTO.LastName = shippingAddress.LastName;
            addressDTO.Phone = shippingAddress.Phone;
            addressDTO.PostCode = shippingAddress.PostCode;
            return addressDTO;

        }
        public OperationDetails addBankInformation(BankInformationDTO dto)
        {
            var descriptions = unitOfWork.ShippingBillingMethodDescriptionRepository.Get(c => c.ShippingBillingMethodId == (long)Common.BillingMethods.BankTransfer).ToList();
            if (descriptions == null)
                return new OperationDetails(false, "حدث خطأ ما، يرجى مراجعة معلومات البنك", "");

            foreach (var desc in descriptions)
            {
                desc.MetaDescription = dto.Description;
                unitOfWork.ShippingBillingMethodDescriptionRepository.Update(desc);
                unitOfWork.Save();
            }

            return new OperationDetails(true, "تم اضافة الوصف بنجاح", "");
        }
    }
}
