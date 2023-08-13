using System;
using System.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;

namespace DocumentSign_BusinessLayer
{
    public class EmailRepository
    {
        public string SendEmail(string emailTo, string emailSubject, string emailOutline, string emailBody)
        {
            string response;
            try
            {
                SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                using (MailMessage mm = new MailMessage())
                {
                    mm.From = new MailAddress(smtpSection.From, emailOutline);
                    mm.To.Add(emailTo);
                    mm.Subject = emailSubject;
                    mm.Body = emailBody;
                    mm.IsBodyHtml = true;


                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = smtpSection.Network.Host;
                    smtp.EnableSsl = smtpSection.Network.EnableSsl;

                    NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                    smtp.Credentials = networkCred;
                    smtp.Port = smtpSection.Network.Port;
                    smtp.Send(mm);
                }


                response = "Send";

            }
            catch (Exception e)
            {

                response = e.Message;
            }

            return response;

        }
    }
}
