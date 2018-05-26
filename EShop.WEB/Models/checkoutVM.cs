using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EShop.BLL.DTO;
using EShop.Common;

namespace EShop.WEB.Models
{
    public class CheckoutVM
    {
        public long Id { get; set; }
        public long InvoiceId { get; set; }
        public string UserId { get; set; }
        public long SelectedShippingAddressId { get; set; }
        public long SelectedBillingAddressId { get; set; }
        public List<AddressDTO> AvailableAddresses { get; set; }
        public CouponDTO Coupon { get; set; }
        public DateTime CreationDate { get; set; }
        public string CurrencyName { get; set; }
        public ShippingCompanyDTO SelectShippingCompany { get; set; }
        public ShippingMethods ShippingMethod { get; set; }
        public double ShippingCost { get; set; }
        public List<OrderItemDTO> CheckOutItems { get; set; }
        public double SubTotalCost { get; set; }
        public double TotalCost { get; set; }
        public string TransactionId { get; set; }
        public bool IsSameAddresses { get; set; }
        [Required]
        public UserBankInforamtionVM UserBankInforamtion { get; set; }    
        public List<ShippingCompanyDTO> ShippingCompanies { get; set; }
        public List<ShippingMethodDTO> ShippingMethods { get; set; }
        public List<BillingMethodDTO> BillingMethods { get; set; }
        public BillingMethods SelectedBillingMethod { get; set; }
        public string TotalCostDisplay
        {
            get
            {
                return Utils.GetValueCurrencyDisplay(CurrencyName, TotalCost);
            }
        }
        public string SubTotalCostDisplay
        {
            get
            {
                return Utils.GetValueCurrencyDisplay(CurrencyName, SubTotalCost);
            }
        }
        public string ShippingCostDisplay
        {
            get
            {
                return Utils.GetValueCurrencyDisplay(CurrencyName, ShippingCost);
            }
        }

    }
}