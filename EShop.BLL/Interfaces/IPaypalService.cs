using EShop.BLL.DTO;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Interfaces
{
    public interface IPaypalService
    {
        Payment CreatePayment(APIContext apiContext, string redirectUrl, CheckOutDTO dto);
        Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId, ref Payment payment);
    }
}
