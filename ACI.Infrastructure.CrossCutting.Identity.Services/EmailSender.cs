using ACI.Infrastructure.CrossCutting.Identity.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ACI.Infrastructure.CrossCutting.Identity.Services
{
    public class EmailSender : IEmailSender
    {

        private static bool _mailSent;
        private readonly IConfiguration _configuration;

        public EmailSender()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<bool> SendAsync(string to, string body)
        {
            string from = _configuration["MailSettings:from"];
            string password = _configuration["MailSettings:password"];
            string subject = _configuration["MailSettings:subject"];

            using (SmtpClient client = new SmtpClient())
            {
                client.Host = _configuration["MailSettings:host"];
                client.Port = Convert.ToInt32(_configuration["MailSettings:port"]);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(from, password);

                using (MailMessage message = new MailMessage(from, to))
                {
                    message.Subject = subject;
                    message.Body = body;
                    client.SendCompleted += new SendCompletedEventHandler(CallBack);
                    await client.SendMailAsync(message);
                }
                return _mailSent;
            }

        }

        private void CallBack(object sender, AsyncCompletedEventArgs e)
        {
            switch (e.Error != null)
            {
                case true:
                    _mailSent = false;
                    break;
                case false:
                    _mailSent = true;
                    break;
            }
        }
    }
}