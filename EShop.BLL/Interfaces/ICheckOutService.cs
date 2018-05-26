using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.BLL.DTO;
using EShop.Common;
using EShop.BLL.Infrastructure;

namespace EShop.BLL.Interfaces
{
    public interface ICheckOutService
    {
        CheckOutDTO GetSessionOrder(string userId, Langs l, Currency currency);
        OperationDetails SetShippingCompany(string userId, long company);
        OperationDetails SetBillingAddress(string userId, long addressId);
        OperationDetails SetStep(string userId, int step);
        OperationDetails SetIsSame(string userId, bool isSame);
        OperationDetails SetCoupon(string userId, string coupon);
        OperationDetails SetShippingAddress(string userId, long addressId);
        OperationDetails SetBillingMethod(string userId, BillingMethods method);
        OperationDetails SetShippingMethod(string userId, ShippingMethods method);
       
    }
}
