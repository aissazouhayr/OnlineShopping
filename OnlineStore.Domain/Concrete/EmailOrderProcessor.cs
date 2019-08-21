using OnlineStore.Domain.Abstract;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Concrete
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailsetting;
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailsetting = settings;
        }
        public void ProcessOrder(Cart cart, ShippingDetails shippinInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailsetting.UseSsl;
                smtpClient.Host = emailsetting.ServerName;
                smtpClient.Port = emailsetting.ServerPort;
               // smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential(
                    emailsetting.Username, emailsetting.Password
                    );
               
                StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("----")
                    .AppendLine("Items:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal:{2:c}", 
                        line.Quantity,line.Product.Name, subtotal);
                }
                body.AppendFormat("Total Order value: {0:c}",
                    cart.computeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Ship to:")
                    .AppendLine(shippinInfo.Name)
                    .AppendLine(shippinInfo.Line1)
                    .AppendLine(shippinInfo.Line2 ?? "")
                    .AppendLine(shippinInfo.Line3 ?? "")
                    .AppendLine(shippinInfo.City)
                    .AppendLine(shippinInfo.State)
                    .AppendLine(shippinInfo.Country)
                    .AppendLine(shippinInfo.Zip)
                    .AppendLine("---")
                    .AppendFormat("Gift wrap: {0}", shippinInfo.GiftWrap ?"Yes":"No")
                    ;
                  MailMessage mailMessage = new MailMessage(
                  emailsetting.MailFromAddress,
                  emailsetting.MailToAddress,
                  "new order Submitted",
                  body.ToString()
                  );
                smtpClient.Send(mailMessage);
            }
         
        }
    }
}
