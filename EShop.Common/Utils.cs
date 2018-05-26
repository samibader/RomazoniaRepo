using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace EShop.Common
{
    public static class Utils
    {

        public static string GenerateSlug(string phrase)
        {
            if (String.IsNullOrEmpty(phrase))
                return "";
            string str = Utils.RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public static string RemoveAccent(string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
        private static string GenerateInvoiceNo()
        {
            DateTime d = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            sb.Append("INV-");
            sb.Append(d.Day < 10 ? "0" + d.Day.ToString() : d.Day.ToString());
            sb.Append(d.Month.ToString("D2"));
            sb.Append(d.Year.ToString().Substring(2));
            sb.Append("-");
            Random r = new Random();
            sb.Append(r.Next(10000).ToString("D4"));

            return sb.ToString();


        }

        public static string buildEmail(OrderStates state )
        {

            string body =String.Format(@"
<html xmlns=""http://www.w3.org/1999/xhtml"" style=""font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
<head>
<meta name=""viewport"" content=""width=device-width"" />
<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
<title>Actionable emails e.g. reset password</title>


<style type=""text/css"">
img {{
max-width: 100%;
}}
body {{
-webkit-font-smoothing: antialiased; -webkit-text-size-adjust: none; width: 100% !important; height: 100%; line-height: 1.6em;
  direction:rtl;
}}
body {{
background-color: #f6f6f6;
}}
@media only screen and (max-width: 640px) {{
  body {{
    padding: 0 !important;
  }}
  h1 {{
    font-weight: 800 !important; margin: 20px 0 5px !important;
  }}
  h2 {{
    font-weight: 800 !important; margin: 20px 0 5px !important;
  }}
  h3 {{
    font-weight: 800 !important; margin: 20px 0 5px !important;
  }}
  h4 {{
    font-weight: 800 !important; margin: 20px 0 5px !important;
  }}
  h1 {{
    font-size: 22px !important;
  }}
  h2 {{
    font-size: 18px !important;
  }}
  h3 {{
    font-size: 16px !important;
  }}
  .container {{
    padding: 0 !important; width: 100% !important;
  }}
  .content {{
    padding: 0 !important;
  }}
  .content-wrap {{
    padding: 10px !important;
  }}
  .invoice {{
    width: 100% !important;
  }}
}}
</style>
</head>

<body itemscope itemtype=""http://schema.org/EmailMessage"" style=""direction:rtl; font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; -webkit-font-smoothing: antialiased; -webkit-text-size-adjust: none; width: 100% !important; height: 100%; line-height: 1.6em; background-color: #f6f6f6; margin: 0;"" bgcolor=""#f6f6f6"">

<table class=""body-wrap"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; background-color: #f6f6f6; margin: 0;"" bgcolor=""#f6f6f6""><tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><td style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;"" valign=""top""></td>
		<td class=""container"" width=""600"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto;"" valign=""top"">
			<div class=""content"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 20px;"">
				<table class=""main"" width=""100%"" cellpadding=""0"" cellspacing=""0"" itemprop=""action"" itemscope itemtype=""http://schema.org/ConfirmAction"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-radius: 3px; background-color: #fff; margin: 0; border: 1px solid #e9e9e9;"" bgcolor=""#fff""><tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><td class=""content-wrap"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 20px;"" valign=""top"">
							<meta itemprop=""name"" content=""Confirm Email"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"" /><table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
										{0}
									</td>
								</tr><tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
										{1}
									</td>
								</tr><tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><td class=""content-block"" itemprop=""handler"" itemscope itemtype=""http://schema.org/HttpActionHandler"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
										<a href=""http://www.romazonia.com"" class=""btn-primary"" itemprop=""url"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; color: #FFF; text-decoration: none; line-height: 2em; font-weight: bold; text-align: center; cursor: pointer; display: inline-block; border-radius: 5px; text-transform: capitalize; background-color: #348eda; margin: 0; border-color: #348eda; border-style: solid; border-width: 10px 20px;"">الرجوع إلى رومازونيا</a>
									</td>
								</tr><tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
										&mdash; فريق الدعم في رومازونيا
									</td>
								</tr></table></td>
					</tr></table><div class=""footer"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; clear: both; color: #999; margin: 0; padding: 20px;"">
					<table width=""100%"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><td class=""aligncenter content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 12px; vertical-align: top; color: #999; text-align: center; margin: 0; padding: 0 0 20px;"" align=""center"" valign=""top"">Follow <a href=""http://twitter.com/mail_gun"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 12px; color: #999; text-decoration: underline; margin: 0;"">@Mail_Gun</a> on Twitter.</td>
						</tr></table></div></div>
		</td>
		<td style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;"" valign=""top""></td>
	</tr></table></body>
</html>


", "لقد اتحويل طلبك الى الحالة",getOrderStateByLang(state));
            return body;
        }
        public static string getOrderStateByLang(OrderStates s)
        {
            if(CultureInfo.CurrentCulture.CompareInfo.Name == "ar-SA")
            {
                switch (s)
                {
                    case OrderStates.pending:
                        return "معلق";
                       
                    case OrderStates.paid:
                        return "مدفوع";
                    case OrderStates.pendingShipping:
                        return "بانتظار الشحن";
                    case OrderStates.shipped:
                        return "مشحون";
                    case OrderStates.complete:
                        return "مكتمل";
                    case OrderStates.canceled:
                        return "ملغي";
                    default:
                        return "";
                }
            }
            else
            {
                return s.ToString();
            }
        }
        public static string GetValueCurrencyDisplay(string currency,double value)
        {
            CultureInfo info = CultureInfo.InvariantCulture;
            NumberFormatInfo nfi = (NumberFormatInfo)info.NumberFormat.Clone();
            nfi.CurrencySymbol = currency;
            if (currency == "$")
            {
                
            }
            else
            {
                nfi.CurrencyPositivePattern = 3;
            }
            return String.Format(nfi, "{0:C0}", value);
        }
        public static long getLanguage(Langs l)
        {
            return (long)l;
        }
        public static Tuple<double,String> getCurrency(Currency c,Langs l,double  amount)
        {
            //if (c == Currency.Dollar)
            if(false)
            {
                amount = Math.Round(amount/3.75, 2);
                if (l == Langs.English)
                {
                    return new Tuple<double, string>(amount,"$");
                    //return new Tuple<double, string>(amount, amount.ToString("C", CultureInfo.CreateSpecificCulture("en-US")));
                }
                else
                {
                    return new Tuple<double, string>(amount, "$");
                    //return new Tuple<double, string>(amount, amount.ToString("C", CultureInfo.CreateSpecificCulture("en-US")));
                }
            }
            else
            {
                //amount*=3.75;
                if (l == Langs.English)
                {
                    //return new Tuple<double, string>(amount, amount.ToString("C", CultureInfo.CreateSpecificCulture("ar-SA")));
                    return new Tuple<double, string>(amount, "SAR");
                }
                else
                {
                    return new Tuple<double, string>(amount, "ريال");
                    //return new Tuple<double, string>(amount, amount.ToString("C", CultureInfo.CreateSpecificCulture("ar-SA")));
                }
            }
            
        }
        public static string generateInvoice()
        {
            string tempInvoice = "INV-" + DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            tempInvoice = tempInvoice.Replace('/', ' ');
            string invoice = Regex.Replace(tempInvoice, @"\s+", "");
            Random rnd = new Random();
            int rand = rnd.Next(1, 10000);
            invoice += rand.ToString();
            return invoice;
        }

        public static string getCurrencyName(Currency c, Langs l)
        {
            if (c == Currency.Dollar)
            {
                if (l == Langs.English)
                {
                    return  "$";
                  
                }
                else
                {
                    return "$";
                   
                }
            }
            else
            {
                
                if (l == Langs.English)
                {
                   
                    return "SAR";
                }
                else
                {
                    return "ريال";
                   
                }
            }

        }



        public static string buildInvoiceEmail(string userId, string userName, string invoice, string invoiceId, string totalPrice, string copun, string shipingCost)
        {
            string copunString = !String.IsNullOrWhiteSpace(copun) ? "الكوبون : " + copun : "";
            string shiping = !String.IsNullOrWhiteSpace(shipingCost) ? "تكلفة الشحن : " + shipingCost : "";

            string email = String.Format(@"
<html style=""font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
<head>
<style type=""text/css"">
img {{
max-width: 100%;
}}
body {{
-webkit-font-smoothing: antialiased; -webkit-text-size-adjust: none; width: 100% !important; height: 100%; line-height: 1.6em;
}}
body {{
background-color: #f6f6f6;
}}
@media only screen and (max-width: 640px) {{
  body {{
    padding: 0 !important;
  }}
  h1 {{
    font-weight: 800 !important; margin: 20px 0 5px !important;
  }}
  h2 {{
    font-weight: 800 !important; margin: 20px 0 5px !important;
  }}
  h3 {{
    font-weight: 800 !important; margin: 20px 0 5px !important;
  }}
  h4 {{
    font-weight: 800 !important; margin: 20px 0 5px !important;
  }}
  h1 {{
    font-size: 22px !important;
  }}
  h2 {{
    font-size: 18px !important;
  }}
  h3 {{
    font-size: 16px !important;
  }}
  .container {{
    padding: 0 !important; width: 100% !important;
  }}
  .content {{
    padding: 0 !important;
  }}
  .content-wrap {{
    padding: 10px !important;
  }}
  .invoice {{
    width: 100% !important;
  }}
}}
table {{
  direction:rtl;
  border-spacing: 0;
  border-collapse: collapse;
}}
td,
th {{
  padding: 0;
}}
.table-bordered th,
  .table-bordered td {{
    border: 1px solid #ddd !important;
  }}
.table-bordered {{
  border: 1px solid #ddd;
}}
.table-bordered > thead > tr > th,
.table-bordered > tbody > tr > th,
.table-bordered > tfoot > tr > th,
.table-bordered > thead > tr > td,
.table-bordered > tbody > tr > td,
.table-bordered > tfoot > tr > td {{
  border: 1px solid #ddd;
}}
.table-bordered > thead > tr > th,
.table-bordered > thead > tr > td {{
  border-bottom-width: 2px;
}}
</style>
</head>

<body itemscope itemtype=""http://schema.org/EmailMessage"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; -webkit-font-smoothing: antialiased; -webkit-text-size-adjust: none; width: 100% !important; height: 100%; line-height: 1.6em; background-color: #f6f6f6; margin: 0;"" bgcolor=""#f6f6f6"">

<table class=""body-wrap"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; background-color: #f6f6f6; margin: 0;"" bgcolor=""#f6f6f6""><tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><td style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;"" valign=""top""></td>
		<td class=""container"" width=""600"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto;"" valign=""top"">
			<div class=""content"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 20px;"">
				<table class=""main table table-bordered"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-radius: 3px; background-color: #fff; margin: 0; border: 1px solid #e9e9e9;"" bgcolor=""#fff""><tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><td class=""content-wrap aligncenter"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; text-align: center; margin: 0; padding: 20px;"" align=""center"" valign=""top"">
الاسم: {1} <br/>
رقم الفاتورة: {2} <br/>
							<table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
<thead>
<tr>
<th>الاسم</th>
<th>سعر الواحدة</th>
<th>الكمية</th>
<th>السعر الاجمالي</th>
</tr>
</thead>
<tbody>
							{0}
</tbody>
							</table><div class=""footer"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; clear: both; color: #999; margin: 0; padding: 20px;"">
					<table width=""100%"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><td class=""aligncenter content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 12px; vertical-align: top; color: #999; text-align: center; margin: 0; padding: 0 0 20px;"" align=""center"" valign=""top"">
{5}<br/>
{4} <br/>
<b style=""font-size: large;"">
السعر الاجمالي:{3}
<b>
</td>
						</tr></table></div></div>
		</td>
		<td style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;"" valign=""top""></td>
	</tr></table></body>
</html>


", invoice, userName, invoiceId, totalPrice, copunString, shiping);
            return email;
    }

        public static string buildUserConfirmEmail(string callBackUrl)
        {
           return String.Format(@"
    
<html xmlns=""http://www.w3.org/1999/xhtml"" style=""font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"">
<head>
<meta name=""viewport"" content=""width=device-width"" />
<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
<title>Actionable emails e.g. reset password</title>


<style type=""text/css"">
img {{
max-width: 100%;
}}
body {{
-webkit-font-smoothing: antialiased; -webkit-text-size-adjust: none; width: 100% !important; height: 100%; line-height: 1.6em;
  direction:rtl;
}}
body {{
background-color: #f6f6f6;
}}
@media only screen and (max-width: 640px) {{
  body {{
    padding: 0 !important;
  }}
  h1 {{
    font-weight: 800 !important; margin: 20px 0 5px !important;
  }}
  h2 {{
    font-weight: 800 !important; margin: 20px 0 5px !important;
  }}
  h3 {{
    font-weight: 800 !important; margin: 20px 0 5px !important;
  }}
  h4 {{
    font-weight: 800 !important; margin: 20px 0 5px !important;
  }}
  h1 {{
    font-size: 22px !important;
  }}
  h2 {{
    font-size: 18px !important;
  }}
  h3 {{
    font-size: 16px !important;
  }}
  .container {{
    padding: 0 !important; width: 100% !important;
  }}
  .content {{
    padding: 0 !important;
  }}
  .content-wrap {{
    padding: 10px !important;
  }}
  .invoice {{
    width: 100% !important;
  }}
}}
</style>
</head>

<body itemscope itemtype=""http://schema.org/EmailMessage"" style=""direction:rtl; font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; -webkit-font-smoothing: antialiased; -webkit-text-size-adjust: none; width: 100% !important; height: 100%; line-height: 1.6em; background-color: #f6f6f6; margin: 0;"" bgcolor=""#f6f6f6"">

<table class=""body-wrap"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; background-color: #f6f6f6; margin: 0;"" bgcolor=""#f6f6f6""><tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><td style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;"" valign=""top""></td>
		<td class=""container"" width=""600"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; display: block !important; max-width: 600px !important; clear: both !important; margin: 0 auto;"" valign=""top"">
			<div class=""content"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; max-width: 600px; display: block; margin: 0 auto; padding: 20px;"">
				<table class=""main"" width=""100%"" cellpadding=""0"" cellspacing=""0"" itemprop=""action"" itemscope itemtype=""http://schema.org/ConfirmAction"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; border-radius: 3px; background-color: #fff; margin: 0; border: 1px solid #e9e9e9;"" bgcolor=""#fff""><tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><td class=""content-wrap"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 20px;"" valign=""top"">
							<meta itemprop=""name"" content=""Confirm Email"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;"" /><table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
										يرجى تفعيل الحساب لتتمكن من التمتع بخدمات رومازونيا
									</td>
								</tr><tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
										
									</td>
								</tr><tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><td class=""content-block"" itemprop=""handler"" itemscope itemtype=""http://schema.org/HttpActionHandler"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
										<a href=""{0}"" class=""btn-primary"" itemprop=""url"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; color: #FFF; text-decoration: none; line-height: 2em; font-weight: bold; text-align: center; cursor: pointer; display: inline-block; border-radius: 5px; text-transform: capitalize; background-color: #348eda; margin: 0; border-color: #348eda; border-style: solid; border-width: 10px 20px;"">تفعيل الحساب</a>
									</td>
								</tr><tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><td class=""content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0; padding: 0 0 20px;"" valign=""top"">
										&mdash; فريق الدعم في رومازونيا
									</td>
								</tr></table></td>
					</tr></table><div class=""footer"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; width: 100%; clear: both; color: #999; margin: 0; padding: 20px;"">
					<table width=""100%"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><tr style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; margin: 0;""><td class=""aligncenter content-block"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 12px; vertical-align: top; color: #999; text-align: center; margin: 0; padding: 0 0 20px;"" align=""center"" valign=""top"">Follow <a href=""http://twitter.com/mail_gun"" style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 12px; color: #999; text-decoration: underline; margin: 0;"">@Mail_Gun</a> on Twitter.</td>
						</tr></table></div></div>
		</td>
		<td style=""font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; box-sizing: border-box; font-size: 14px; vertical-align: top; margin: 0;"" valign=""top""></td>
	</tr></table></body>
</html>

", callBackUrl);
        }
    }

    //public enum WebSites : long { Fashion = 10, Mobile = 11, Gym = 12 };
    public enum WebSites : long { Fashion = 11, Mobile = 12, Gym = 13 }; 
    public enum BaseCategories : long { Women = 1, Men = 2, Babe = 3 };
    public enum Langs : long { Arabic = 1, English =2};
    public static class DefaultImages
    {
        public static string Designer = "/uploads/designer-default.jpg";

        public static string Product = "/uploads/product-default.jpg";

        public static string Category = "/uploads/category-default.jpg";
        public static string Banner = "/uploads/banner-default.jpg";
    }
    
    public enum Sorts : long { DateAddedUp = 1, DateAddedDown = 2,
        NameUpDown=3,NameDownUp=4,
        PriceUpDown=5,PriceDownUp=6,
        ArabicNameUp=7, ArabicNameDown=8,
        EnglishNameUp=9,EnglishNameDown=10,
        StatusUp = 11, StatusDown = 12,
        IdUp = 13,IdDown=14,
        OrderIdUp = 15, OrderIdDown=16,
        CustomerNameUp=17,CustomerNameDown=18,
        OrederStatusUp = 19, OrederStatusDown=20,
        TotalUp=21,TotalDown=22,
        DateModifiedUp = 23, DateModifiedDown=24,
        QuantityUp = 25, QuantityDown = 26,
        AttributeGroupIdUp = 27, AttributeGroupIdDown = 28,
        ValueUp=29,ValueDown=30,
        InvoiceUp=31,InvoiceDown=32,
        EmailUp = 33,EmailDown=34,
        lockedUp = 35,lockedDown=36

    };
    public enum OrderStates : long
    {
       pending=1,
        paid=2,pendingShipping =3,
        shipped =4,complete =5,
        canceled = 6,All=7,Active=8
    };
   
    public enum Currency : long { Dollar = 1, Rial = 2 };
    public enum CheckoutSteps {Coupon=1,Address=2,Payment=3 }
    public enum ShippingMethods:long { Free=1,WithCompany=2,ByHand=3}
    public enum BillingMethods:long { BankTransfer=5,Paybal=6,Cash=4} 
}
