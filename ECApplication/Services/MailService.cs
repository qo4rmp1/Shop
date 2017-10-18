using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ECApplication.Services
{
    public class MailService
    {
        private string gmail_account = "test";
        private string gmail_mail = "test@gmail.com";
        private string gmail_password = "test";

        public string GetRegisterMailBody(string TempString, string UserName, string ValidateUrl)
        {
            TempString = TempString.Replace("{{Name}}", UserName);
            TempString = TempString.Replace("{{AUTH_URL}}", ValidateUrl);

            return TempString;
        }

        public void SendRegisterMail(string MailBody, string ToMail)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential(gmail_account, gmail_password);
            SmtpServer.EnableSsl = true;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(gmail_mail);
            mail.To.Add(ToMail);
            mail.Subject = "會員註冊確認信";
            mail.Body = MailBody;
            mail.IsBodyHtml = true;
            SmtpServer.Send(mail);
        }
    }
}