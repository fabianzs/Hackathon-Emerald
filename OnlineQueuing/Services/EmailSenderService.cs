using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace OnlineQueuing.Services
{
    public class EmailSenderService
    {
        private IConfiguration configuration;
        private SmtpClient smtpClient;

        public EmailSenderService(IConfiguration config, SmtpClient sc)
        {
            this.configuration = config;
            this.smtpClient = sc;
        }
        public void SendEmail(string toEmail, string toName)
        {
            MailAddress fromAddress = new MailAddress(configuration["GmailUser"], "");
            MailAddress toAddress = new MailAddress(toEmail, toName);
            string fromPassword = configuration["GmailPassword"];
            string subject = "Subject";
            string body = $"Dear @{toName}!/n We recieved your reservation!/n We are waiting for you!/n Staff";

            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(fromAddress.Address, fromPassword);            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtpClient.Send(message);
            }
        }
    }
}