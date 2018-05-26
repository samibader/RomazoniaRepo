using EShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class Order
    {
        public long  Id { get; set; }
        public string UserId { get; set; }

        public string CouponCode { get; set; }
        public bool CouponIsPercentage { get; set; }
        public double CouponValue { get; set; }
        public string ArabicShippingName { get; set; }
        public string EnglishShippingName { get; set; }
        public string ArabicBillingName { get; set; }
        public string EnglishBillingName { get; set; }
        public double SubTotal { get; set; }
        public double ShippingCost { get; set; }
        public string Discount { get; set; }
        public double Total { get; set; }
        public string InvoiceId { get; set; }
        public long TransactionId { get; set; }
        public string TransactionDetails { get; set; }
        public string UserIpAddress { get; set; }
        public string UserAgent { get; set; }

        public string ArabicCompanyName { get; set; }
        public string EnglishCompanyName { get; set; }


        public OrderStates Status { get; set; }
        public string PaymentNotes { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DateModified { get; set; }

        public DateTime? PaymentDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
        public virtual ShippingAddress ShippingAddress { get; set; }
        public virtual BillingAddress BillingAddress { get; set; }
    }
}
