using EShop.BLL.DTO;
using EShop.BLL.Interfaces;
using EShop.DAL.Entities;
using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Common;

namespace EShop.BLL.Services
{
    public class CouponService:ICouponService
    {
        IUnitOfWork unitOfWork { get; set; }
        public CouponService(IUnitOfWork uow)
        {

            unitOfWork = uow;
        }
        public CouponDTO GetCoupon(string couponCode,Langs l,Currency currency)
        {
            Coupon coupon = unitOfWork.CouponRepository.Get(c => c.Code == couponCode).FirstOrDefault();
            if (coupon == null)
                return new CouponDTO();
            CouponDTO couponDTO = new CouponDTO();
            couponDTO.Code = coupon.Code;
            couponDTO.CurrencyName = Utils.getCurrencyName(currency, l);
            couponDTO.Id = coupon.Id;
            couponDTO.IsPercentage = coupon.IsPercentage;
            couponDTO.Value = coupon.Value;
            if(!coupon.IsPercentage)
            couponDTO.Value = Utils.getCurrency(currency,l,coupon.Value).Item1;
            return couponDTO;
        }
        public bool CheckCoupon(string couponCode)
        {
            Coupon coupon=unitOfWork.CouponRepository.Get(c=>c.Code==couponCode).FirstOrDefault();

            return coupon!=null &&
                   unitOfWork.OrderRepository.Get(c => c.CouponCode == couponCode).Count() <= coupon.MaxNumOfUsage &&
                   coupon.IsActive &&
                   (
                       (coupon.EndDate.Value != null && coupon.EndDate.Value.CompareTo(DateTime.Now) >= 0) || 
                        coupon.EndDate.Value == null
                   );
        }

    }
}
