using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace EShop.DAL.Repositories
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Twilio Begin
            //var Twilio = new TwilioRestClient(
            //  System.Configuration.ConfigurationManager.AppSettings["SMSAccountIdentification"],
            //  System.Configuration.ConfigurationManager.AppSettings["SMSAccountPassword"]);
            //var result = Twilio.SendMessage(
            //  System.Configuration.ConfigurationManager.AppSettings["SMSAccountFrom"],
            //  message.Destination, message.Body
            //);
            //// Status is one of Queued, Sending, Sent, Failed or null if the number is not valid
            //Trace.TraceInformation(result.Status);
            // Twilio doesn't currently have an async API, so return success.

            // Plug in your SMS service here to send a text message.
            //var soapSms = new ASPSMSX2.ASPSMSX2SoapClient("ASPSMSX2Soap");
            //string res = soapSms.SendSimpleTextSMS(
            //    System.Configuration.ConfigurationManager.AppSettings["ASPSMSUSERKEY"],
            //    System.Configuration.ConfigurationManager.AppSettings["ASPSMSPASSWORD"],
            //    message.Destination,
            //    System.Configuration.ConfigurationManager.AppSettings["ASPSMSORIGINATOR"],
            //    message.Body);
            //soapSms.Close();
            //return Task.FromResult(0);
            // Twilio End
            throw new Exception();
        }
    }
}
