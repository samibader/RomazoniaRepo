using EShop.BLL.DTO;
using EShop.Common;
using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.BLL.Interfaces;
using EShop.DAL.Entities;
using EShop.BLL.Infrastructure;

namespace EShop.BLL.Services
{
    public class CheckOutService:ICheckOutService
    {

        IUnitOfWork unitOfWork { get; set; }
        AddressService addressService { get; set; }
        CartService cartService { get; set; }
        CouponService couponService { get; set; }
        public CheckOutService(IUnitOfWork uow,AddressService addressSvc,CartService cartSvc,CouponService couponSvc)
        {
            unitOfWork = uow;
            addressService = addressSvc;
            cartService = cartSvc;
            couponService = couponSvc;
        }
        public OperationDetails SetShippingMethod(string userId,ShippingMethods method)
        {
            CheckOut checkOut= unitOfWork.CheckOutRepository.GetByID(userId);
            if (checkOut == null)
                return new OperationDetails(false, "خطأ ما أثناء عملية التعديل", "");
                
            checkOut.SelectedShippingMethod = (long)method;
            unitOfWork.CheckOutRepository.Update(checkOut);
            unitOfWork.Save();
            return new OperationDetails(true, "تمت عملية التعديل بنجاح", "");

        }
        public OperationDetails SetBillingMethod(string userId, BillingMethods method)
        {
            CheckOut checkOut = unitOfWork.CheckOutRepository.GetByID(userId);
            if (checkOut == null)
                return new OperationDetails(false, "خطأ ما أثناء عملية التعديل", "");

            checkOut.SelectedBillingMethod = (long)method;
            unitOfWork.CheckOutRepository.Update(checkOut);
            unitOfWork.Save();
            return new OperationDetails(true, "تمت عملية التعديل بنجاح", "");

        }
        public OperationDetails SetShippingAddress(string userId, long addressId)
        {
            CheckOut checkOut = unitOfWork.CheckOutRepository.GetByID(userId);
            if (checkOut == null)
                return new OperationDetails(false, "خطأ ما أثناء عملية التعديل", "");

            checkOut.SelectedShippingAddress = addressId;
            unitOfWork.CheckOutRepository.Update(checkOut);
            unitOfWork.Save();
            return new OperationDetails(true, "تمت عملية التعديل بنجاح", "");

        }
        public OperationDetails SetBillingAddress(string userId, long addressId)
        {
            CheckOut checkOut = unitOfWork.CheckOutRepository.GetByID(userId);
            if (checkOut == null)
                return new OperationDetails(false, "خطأ ما أثناء عملية التعديل", "");

            checkOut.SelectedBillingAddress = addressId;
            unitOfWork.CheckOutRepository.Update(checkOut);
            unitOfWork.Save();
            return new OperationDetails(true, "تمت عملية التعديل بنجاح", "");

        }
        public OperationDetails SetShippingCompany(string userId, long company)
        {
            CheckOut checkOut = unitOfWork.CheckOutRepository.GetByID(userId);
            if (checkOut == null)
                return new OperationDetails(false, "خطأ ما أثناء عملية التعديل", "");

            checkOut.SelectedShippingCompany = company;
            unitOfWork.CheckOutRepository.Update(checkOut);
            unitOfWork.Save();
            return new OperationDetails(true, "تمت عملية التعديل بنجاح", "");

        }
        public OperationDetails SetStep(string userId, int step)
        {
            CheckOut checkOut = unitOfWork.CheckOutRepository.GetByID(userId);
            if (checkOut == null)
                return new OperationDetails(false, "خطأ ما أثناء عملية التعديل", "");

            checkOut.Step = step;
            unitOfWork.CheckOutRepository.Update(checkOut);
            unitOfWork.Save();
            return new OperationDetails(true, "تمت عملية التعديل بنجاح", "");

        }
        public OperationDetails SetCoupon(string userId, string coupon)
        {
            CheckOut checkOut = unitOfWork.CheckOutRepository.GetByID(userId);
            if (checkOut == null)
                return new OperationDetails(false, "خطأ ما أثناء عملية التعديل", "");

            checkOut.CouponCode = coupon;
            unitOfWork.CheckOutRepository.Update(checkOut);
            unitOfWork.Save();
            return new OperationDetails(true, "تمت عملية التعديل بنجاح", "");
        }

        public OperationDetails SetIsSame(string userId,bool isSame)
        {
            CheckOut checkOut = unitOfWork.CheckOutRepository.GetByID(userId);
            if (checkOut == null)
                return new OperationDetails(false, "خطأ ما أثناء عملية التعديل", "");

            checkOut.IsSame = isSame;
            if(isSame)
                SetShippingAddress(userId,checkOut.SelectedBillingAddress);
            unitOfWork.CheckOutRepository.Update(checkOut);
            unitOfWork.Save();
            return new OperationDetails(true, "تمت عملية التعديل بنجاح", "");
        }
        
        private CheckOut GetUserCheckOut(string userId)
        {
            CheckOut userCheckOut = unitOfWork.CheckOutRepository.GetByID(userId);
            if (userCheckOut == null)
            {
                userCheckOut = new CheckOut();
                userCheckOut.Id = userId;
                userCheckOut.SelectedBillingMethod=(long)BillingMethods.BankTransfer;
                userCheckOut.SelectedShippingMethod=(long)ShippingMethods.Free;
                if (unitOfWork.AddressRepository.Get(c => c.UserId == userId).Count() > 0)
                {
                    userCheckOut.SelectedBillingAddress = addressService.GetDefaultUserAddress(userId).Id;
                    userCheckOut.SelectedShippingAddress = addressService.GetDefaultUserAddress(userId).Id;
                }
                userCheckOut.IsSame = false;
                userCheckOut.Step = 2;
                unitOfWork.CheckOutRepository.Insert(userCheckOut);
                unitOfWork.Save();
            }
            return userCheckOut;
        }
        public CheckOutDTO GetSessionOrder(string userId,Langs l,Currency currency)
        {
            CheckOutDTO chickOutDTO = new CheckOutDTO();
            CheckOut userCheckOut = GetUserCheckOut(userId);
            chickOutDTO.ShippingMethod =(ShippingMethods) userCheckOut.SelectedShippingMethod;
            chickOutDTO.SelectedBillingMethod = (BillingMethods)userCheckOut.SelectedBillingMethod;
            chickOutDTO.SelectedBillingAddressId = userCheckOut.SelectedBillingAddress;
            chickOutDTO.SelectedShippingAddressId = userCheckOut.SelectedShippingAddress;
            chickOutDTO.SelectShippingCompany = GetShippingCompanies(l, currency).
                            Where(c =>userCheckOut.SelectedShippingCompany!=null && c.Id == userCheckOut.SelectedShippingCompany).FirstOrDefault() ;
            chickOutDTO.Step = userCheckOut.Step;
            chickOutDTO.Coupon = couponService.GetCoupon( userCheckOut.CouponCode,l,currency );
            chickOutDTO.IsSameAddresses = userCheckOut.IsSame;

            chickOutDTO.CurrencyName = Utils.getCurrencyName(currency, l);

            chickOutDTO.UserId = userId;
            chickOutDTO.AvailableAddresses = addressService.GetUserAddresses(userId) ;
            chickOutDTO.CheckOutItems = ToOrderItemDTO(cartService.GetCartProducts(userId,l,currency),l,currency) ;

            if (chickOutDTO.AvailableAddresses.Count == 1)
            {
                SetShippingAddress(userId, chickOutDTO.AvailableAddresses[0].Id);
                SetBillingAddress(userId, chickOutDTO.AvailableAddresses[0].Id);
                chickOutDTO.SelectedShippingAddressId = chickOutDTO.AvailableAddresses[0].Id;
                    chickOutDTO.SelectedBillingAddressId=chickOutDTO.AvailableAddresses[0].Id;
            }

            chickOutDTO.CreationDate = DateTime.Now;
            chickOutDTO.ShippingCompanies = GetShippingCompanies(l,currency);
            if (chickOutDTO.ShippingMethod == ShippingMethods.WithCompany)
                chickOutDTO.ShippingCost = chickOutDTO.SelectShippingCompany.ShippingCost;
            chickOutDTO.BillingMethods = GetBillingMethods(l);
            chickOutDTO.ShippingMethods = GetShippingMethods(l);
            chickOutDTO.SubTotalCost = GetSubTotalCost(chickOutDTO.CheckOutItems);
            
            return chickOutDTO;
        }

        private double GetSubTotalCost(List<OrderItemDTO> itemList)
        {
            double sum = 0;
            foreach (var item in itemList)
            {
                sum += item.TotalPrice;
            }
            return sum;
        }
        
        private List<ShippingMethodDTO> GetShippingMethods(Langs l)
        {
            List<ShippingMethodDTO> ShippingMethodDTOs = new List<ShippingMethodDTO>();
            var lang = Utils.getLanguage(l);
            var methods = unitOfWork.ShippingBillingMethodRepository.Get(c => c.IsActive == true&&c.IsShipping==true).ToList();
            foreach (var method in methods)
            {
                ShippingMethodDTO shippingMethodDTO = new ShippingMethodDTO();
                shippingMethodDTO.Id = method.Id;

                ShippingBillingMethodDescription desc = unitOfWork.ShippingBillingMethodDescriptionRepository.Get(c =>
                                    c.LanguageId == lang &&
                                    c.ShippingBillingMethodId == method.Id).FirstOrDefault();

                shippingMethodDTO.Name = desc.Name;
                shippingMethodDTO.MetaDescription = desc.MetaDescription;

                ShippingMethodDTOs.Add(shippingMethodDTO);
            }
            return ShippingMethodDTOs;
        }
        private List<BillingMethodDTO> GetBillingMethods(Langs l)
        {
            List<BillingMethodDTO> billingMethodDTOs = new List<BillingMethodDTO>();
            var lang = Utils.getLanguage(l);
            var methods = unitOfWork.ShippingBillingMethodRepository.Get(c => c.IsActive == true && c.IsShipping == false).ToList();
            foreach (var method in methods)
            {
                BillingMethodDTO billingMethodDTO = new BillingMethodDTO();
                billingMethodDTO.Id = method.Id;

                ShippingBillingMethodDescription desc = unitOfWork.ShippingBillingMethodDescriptionRepository.Get(c =>
                                    c.LanguageId == lang &&
                                    c.ShippingBillingMethodId == method.Id).FirstOrDefault();

                billingMethodDTO.Name = desc.Name;
                billingMethodDTO.MetaDescription = desc.MetaDescription;

                billingMethodDTOs.Add(billingMethodDTO);
            }
            return billingMethodDTOs;
        }
        private List<ShippingCompanyDTO> GetShippingCompanies(Langs l,Currency currency)
        {
            List<ShippingCompanyDTO> ShippingCompanyDTOs = new List<ShippingCompanyDTO>();
            var companies = unitOfWork.ShippingCompanyRepository.Get(c => c.IsActive == true).ToList();
            foreach (var company in companies)
            {
                ShippingCompanyDTO shippingCompanyDTO = new ShippingCompanyDTO();
                shippingCompanyDTO.Id = company.Id;
                shippingCompanyDTO.ShippingCost = Utils.getCurrency(currency, l, company.ShippingCost).Item1;
                shippingCompanyDTO.ShippingCurrency = Utils.getCurrency(currency, l, company.ShippingCost).Item2;
                long lang = Utils.getLanguage(l);

                ShippingCompanyDescription desc = unitOfWork.ShippingCompanyDescriptionRepository.Get(c =>
                                    c.LanguageId == lang &&
                                    c.ShippingCompanyId == company.Id).FirstOrDefault();

                shippingCompanyDTO.Name = desc.Name;
                shippingCompanyDTO.MetaDescription = desc.MetaDescription;

                ShippingCompanyDTOs.Add(shippingCompanyDTO);
            }
            return ShippingCompanyDTOs;
        }
        private List<OrderItemDTO> ToOrderItemDTO(List<ProductDTO> products,Langs l,Currency currency)
        {
            List<OrderItemDTO> orderItemDTOs = new List<OrderItemDTO>();
            foreach (var product in products)
            {
                OrderItemDTO orderItemDTO = new OrderItemDTO();
                orderItemDTO.Name = product.Name;

                if (product.SelectedColor >= 0)
                {
                    var optionId = product.Colors[product.SelectedColor].OptionId;
                    var valueId = product.Colors[product.SelectedColor].ValueId;
                    orderItemDTO.ColorArabic = unitOfWork.OptionValueDescriptionRepository.Get(c => c.LanguageId == (long)Langs.Arabic
                        && c.ProductId == product.Id && c.OptionId == optionId
                        && c.ValueId ==valueId ).FirstOrDefault().Name;   
                    orderItemDTO.ColorEnglish=product.Colors[product.SelectedColor].ColorNameEnglish;
                }
                
                if (product.SelectedSize >= 0)
                {
                    var optionId = product.Sizes[product.SelectedSize].OptionId;
                    var valueId = product.Sizes[product.SelectedSize].ValueId;
                    orderItemDTO.SizeArabic=unitOfWork.OptionValueDescriptionRepository.Get(c => c.LanguageId == (long)Langs.Arabic
                        && c.ProductId == product.Id && c.OptionId == optionId
                        && c.ValueId == valueId).FirstOrDefault().Name;
                    orderItemDTO.SizeEnglish = product.Sizes[product.SelectedSize].EnglishName;
                }

                orderItemDTO.Quantity = product.Quantity;
                orderItemDTO.UnitPrice = product.TotalPrice;
                orderItemDTO.TotalPrice = orderItemDTO.UnitPrice * orderItemDTO.Quantity;
                orderItemDTO.ImageUrl = product.Images.FirstOrDefault();
                orderItemDTO.CurrencyName = Utils.getCurrencyName(currency, l);
                orderItemDTO.Description = product.MetaDescriptions;
                orderItemDTO.Id = product.Id;
                orderItemDTO.SKUId = product.SKUId;
                orderItemDTOs.Add(orderItemDTO);
            }
            return orderItemDTOs;
        }


    }
}
