using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace EShop.DAL.Repositories
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await ConfigSendEmailasync(message);
        }

        private SmtpClient GetSmtpClient()
        {
            //SmtpClient client = new SmtpClient();
            //client.Credentials = new NetworkCredential("S.Bader@infosyria.com", "123qwe");
            //client.Host = "mail.infosyria.com";
            //client.Port = 587;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;

            return new SmtpClient();
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task ConfigSendEmailasync(IdentityMessage message)
        {

            var myMessage = new MailMessage();
            myMessage.To.Add(message.Destination);
            myMessage.From = new System.Net.Mail.MailAddress(
                                "noreply@instagramkom.com", "رسالة من انستغرامكوم دوت كوم");
            myMessage.Subject = message.Subject;
            myMessage.Body = message.Body;
            myMessage.IsBodyHtml = true;
            using (var smtp = GetSmtpClient())
            {
                await smtp.SendMailAsync(myMessage);
            }
        }
    }
}
