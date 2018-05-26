using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.BLL.DTO;
using EShop.Common;

namespace EShop.BLL.Interfaces
{
    public interface ICouponService
    {
        bool CheckCoupon(string couponCode);
        CouponDTO GetCoupon(string couponCode,Langs l,Currency currency);
    }
}
