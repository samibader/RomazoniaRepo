using EShop.BLL.DTO;
using EShop.BLL.Interfaces;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Services
{
    public class PaypalService : IPaypalService
    {

        public Payment CreatePayment(APIContext apiContext, string redirectUrl, CheckOutDTO dto)
        {
            // ADD CART ITEMS
            var itemList = new ItemList() { items = new List<Item>() };
            foreach (var item in dto.CheckOutItems)
            {
                itemList.items.Add(new Item()
                {
                    name = item.Name,
                    currency = "USD",
                    price = item.UnitPrice.ToString(),
                    quantity = item.Quantity.ToString(),
                    sku = item.SKU,
                    description = item.Name
                });
            }

            // CHECK COUPON
            if(dto.Coupon.Code!="")
            {
                itemList.items.Add(new Item()
                    {
                        name = "Coupon Discount:" + dto.Coupon.ValueDisplay,
                        quantity = "1",
                        sku = dto.Coupon.Code,
                        price = dto.CouponDiscount.ToString(),
                        currency = "USD"
                    });
            }

            // CALCULATE AMOUNTS
            var details = new Details()
            {
                subtotal = dto.SubTotalCostDiscounted.ToString(),
                shipping = dto.ShippingCost.ToString(),
            };

            var amount = new Amount()
            {
                currency = "USD",
                total = dto.TotalCost.ToString(), // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            // SET TRANSACTION
            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Cart Checkout from romazonia.com",
                invoice_number = dto.InvoiceId,
                amount = amount,
                item_list = itemList
            });

            // CREATE PAYER
            var payer = new Payer() { payment_method = "paypal" };

            // CREATE URLS
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // CREATE PAYMENT
            Payment payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // RETURN PAYMENT
            return payment.Create(apiContext);

            // 
            //var profile = new WebProfile
            //{
            //    name = "My web experience profile",
            //    presentation = new Presentation
            //    {
            //        brand_name = "My brand name",
            //        locale_code = "US",
            //        logo_image = "https://www.paypalobjects.com/webstatic/i/ex_ce2/logo/logo_paypal_106x28.png",
            //        return_url_label="Back to romazonia.com"
            //    },
            //    input_fields = new InputFields
            //    {
            //        address_override=1,
            //        no_shipping =0
            //    }
            //};
            //var itemList = new ItemList() { items = new List<Item>() };
            //foreach (var item in dto.CheckOutItems)
            //{
            //    itemList.items.Add(new Item()
            //    {
            //        name = item.Name,
            //        currency = "USD",
            //        price = item.UnitPrice.ToString(),
            //        quantity = item.Quantity.ToString(),
            //        sku = item.SKU,
            //        description = item.Name
            //    });
            //}
     
            //AddressDTO billingAddress=dto.AvailableAddresses.Where(c=>c.Id==dto.SelectedBillingAddressId).SingleOrDefault();

            //Address paypalBillingAddress = new Address()
            //{
            //    city = billingAddress.City,
            //    country_code = billingAddress.Country,
            //    line1 = billingAddress.Address1,
            //    phone = billingAddress.Phone,
            //    postal_code = billingAddress.PostCode
            //};

            //AddressDTO shippingAddress = dto.AvailableAddresses.Where(c => c.Id == dto.SelectedShippingAddressId).SingleOrDefault();

            //ShippingAddress paypalShippingAddress = new ShippingAddress()
            //{
            //    city = shippingAddress.City,
            //    country_code = shippingAddress.Country,
            //    line1 = shippingAddress.Address1,
            //    phone = shippingAddress.Phone,
            //    postal_code = shippingAddress.PostCode
            //};

            ////itemList.shipping_address = paypalShippingAddress; 

            //var payer = new Payer() { payment_method = "paypal" };
            //var redirUrls = new RedirectUrls()
            //{
            //    cancel_url = redirectUrl,
            //    return_url = redirectUrl
            //};
            //var details = new Details()
            //{
            //    subtotal = dto.SubTotalCost.ToString(),
            //    shipping=dto.ShippingCost.ToString(),
            //};

            //var amount = new Amount()
            //{
            //    currency = "USD",
            //    total = dto.TotalCost.ToString(), // Total must be equal to sum of shipping, tax and subtotal.
            //    details = details
            //};

            //var transactionList = new List<Transaction>();

            //transactionList.Add(new Transaction()
            //{
            //    description = "Checkout from romazonia.com",
            //    invoice_number = dto.InvoiceId,
            //    amount = amount,
            //    item_list = itemList
            //});

            //Payment payment = new Payment()
            //{
            //    //experience_profile_id = profile.id,
            //    intent = "sale",
            //    payer = payer,
            //    transactions = transactionList,
            //    redirect_urls = redirUrls
            //};
            //return payment.Create(apiContext);

        }

        public Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId, ref Payment payment)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            payment = new Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExecution);
        }
    }
}
