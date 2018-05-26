using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EShop.WEB.Controllers
{
    public class IPNPaypalController : Controller
    {
        [HttpPost]
        public HttpStatusCodeResult Receive()
        {
            //Store the IPN received from PayPal
            LogRequest(Request);

            //Fire and forget verification task
            Task.Run(() => VerifyTask(Request));

            //Reply back a 200 code
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private void VerifyTask(HttpRequestBase ipnRequest)
        {
            var verificationResponse = string.Empty;

            try
            {
                var verificationRequest = (HttpWebRequest)WebRequest.Create("https://www.sandbox.paypal.com/cgi-bin/webscr");

                //Set values for the verification request
                verificationRequest.Method = "POST";
                verificationRequest.ContentType = "application/x-www-form-urlencoded";
                var param = Request.BinaryRead(ipnRequest.ContentLength);
                var strRequest = Encoding.ASCII.GetString(param);

                //Add cmd=_notify-validate to the payload
                strRequest = "cmd=_notify-validate&" + strRequest;
                verificationRequest.ContentLength = strRequest.Length;

                //Attach payload to the verification request
                var streamOut = new StreamWriter(verificationRequest.GetRequestStream(), Encoding.ASCII);
                streamOut.Write(strRequest);
                streamOut.Close();

                //Send the request to PayPal and get the response
                var streamIn = new StreamReader(verificationRequest.GetResponse().GetResponseStream());
                verificationResponse = streamIn.ReadToEnd();
                streamIn.Close();

            }
            catch (Exception exception)
            {
                //Capture exception for manual investigation
                Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
            }

            ProcessVerificationResponse(verificationResponse);
        }


        private void LogRequest(HttpRequestBase request)
        {
            // Persist the request values into a database or temporary data store
            StringBuilder sb = new StringBuilder();

        for (int i = 0; i < request.QueryString.Count; i++)
        {
            sb.AppendFormat("Key={0}, Value={1}<br/>", request.QueryString.Keys[i], request.QueryString[i]);
        }

        for (int i = 0; i < request.Form.Count; i++)
        {
            sb.AppendFormat("Key={0}, Value={1}<br/>", request.Form.Keys[i], request.Form[i]);
        }
        EShop.WEB.Models.Paypal.PaypalLogger.Log(sb.ToString());
        }

        private void ProcessVerificationResponse(string verificationResponse)
        {
            if (verificationResponse.Equals("VERIFIED"))
            {
                // check that Payment_status=Completed
                // check that Txn_id has not been previously processed
                // check that Receiver_email is your Primary PayPal email
                // check that Payment_amount/Payment_currency are correct
                // process payment
            }
            else if (verificationResponse.Equals("INVALID"))
            {
                //Log for manual investigation
            }
            else
            {
                //Log error
            }
        }
    }
}