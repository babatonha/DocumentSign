using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net.Mail;
using System.Net;
using MbpmAPI.BusinessLogic;

namespace DocumentSign_API.Providers
{


    public class IdentityEmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {

            string response;
            try
            {
                SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                using (MailMessage mm = new MailMessage())
                {
                    mm.From = new MailAddress(smtpSection.From, "Reset Password on Payment Requisition platform");
                    mm.To.Add(message.Destination);
                    mm.Subject = message.Subject;
                    mm.Body = message.Body;
                    mm.IsBodyHtml = true;



                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = smtpSection.Network.Host;
                    smtp.EnableSsl = smtpSection.Network.EnableSsl;

                    smtp.UseDefaultCredentials = false;// smtpSection.Network.DefaultCredentials;

                    NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                
                    smtp.Credentials = networkCred;
                    smtp.Port = smtpSection.Network.Port;
                    smtp.Send(mm);
                }

                ErrorLogger.connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                response = "Send";

            }
            catch (Exception e)
            {
                response = e.Message;
                ErrorLogger.connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                ErrorLogger.LogError("Payment Requisition", "Send Email", "Email address", e);
            }

            return Task.FromResult(response);
        }
    }
}