using EShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using EShop.Common;

namespace EShop.BLL.DTO
{
    public class CheckOutDTO
    {
        public long Id { get; set; }
        public string InvoiceId { get; set; }
        public string UserId { get; set; }
        public int Step { get; set;}
        public string CurrencyName { get; set; }
        public long SelectedShippingAddressId { get; set; }
        public long SelectedBillingAddressId { get; set; }
        public List<AddressDTO> AvailableAddresses { get; set; }
        public CouponDTO Coupon { get; set; }
        public DateTime CreationDate { get; set; }
        public ShippingMethods ShippingMethod { get; set; }
        public ShippingCompanyDTO SelectShippingCompany { get; set; }
        public List<OrderItemDTO> CheckOutItems { get; set; }
        public double ShippingCost { get; set; }
        public double SubTotalCost { get; set; }
        public double SubTotalCostDiscounted {
            get
            {

                //double c = SubTotalCost + (SelectShippingCompany!=null?SelectShippingCompany.ShippingCost:0);
                double c = SubTotalCost;
                if (Coupon.IsPercentage)
                    c *= (100 - (double)Coupon.Value) / 100.0;
                else c -= Coupon.Value;

                return c;
            }
        }
        public double TotalCost
        {
            get
            {
                
                //double c = SubTotalCost + (SelectShippingCompany!=null?SelectShippingCompany.ShippingCost:0);
                double c = SubTotalCost;
                if (Coupon.IsPercentage)
                    c *= (100-(double) Coupon.Value)/100.0;
                else c -= Coupon.Value;

                c += (SelectShippingCompany != null ? SelectShippingCompany.ShippingCost : 0);

                return c;
            }
set { }        }

        public double CouponDiscount
        {
            get
            {
                double c = SubTotalCost;
                double cc=c;
                if (Coupon.IsPercentage)
                    cc *= (100-(double) Coupon.Value)/100.0;
                else cc -= Coupon.Value;
                return cc-c;
            }
        }
        public string TransactionId { get; set; }
        public bool IsSameAddresses { get; set; }
        public List<ShippingCompanyDTO> ShippingCompanies { get; set; }
        public List<ShippingMethodDTO> ShippingMethods { get; set; }
        public List<BillingMethodDTO> BillingMethods { get; set; }
        public BillingMethods SelectedBillingMethod { get; set; }
    }
}
