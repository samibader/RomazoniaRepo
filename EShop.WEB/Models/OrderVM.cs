using EShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EShop.BLL.DTO;
using System.ComponentModel.DataAnnotations;

namespace EShop.WEB.Models
{
    public class OrderVM
    {
        public long Id { get; set; }
        public string CurrencyName { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CouponCode { get; set; }
        public string CouponValue { get; set; }
        public string CouponCurrency { get; set; }
        public string CouponValueDisplay { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [DataType(DataType.Currency)]
        public double SubTotal { get; set; }

        public string SubTotalDisplay
        {
            get
            {
                return Utils.GetValueCurrencyDisplay(CurrencyName, SubTotal);
            }
        }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        [DataType(DataType.Currency)]
        public double ShippingCost { get; set; }

        public string ShippingCostDisplay
        {
            get
            {
                return Utils.GetValueCurrencyDisplay(CurrencyName, ShippingCost);
            }
        }
        public string Discount { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public double Total { get; set; }

        public string TotalDisplay
        {
            get
            {
                return Utils.GetValueCurrencyDisplay(CurrencyName, Total);
            }
        }

        public string InvoiceId { get; set; }
        public long TransactionId { get; set; }
        public string TransactionDetails { get; set; }
        public OrderStates Status { get; set; }
        public string PaymentNotes { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DateModified { get; set; }
        public ShippingMethodDTO ShippingMethod { get; set; }
        public BillingMethodDTO BillingMethod { get; set; }
        public ShippingCompanyDTO ShippingCompany { get; set; }
        public string UserIpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
        public List<OrderHistoryDTO> OrderHistories { get; set; }
        public AddressDTO ShippingAddress { get; set; }
        public AddressDTO BillingAddress { get; set; }
        public string Phone { get; set; }
        
    }
}