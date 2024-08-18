using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using RecipeManagementSystemApplication.Interface;
using RecipeManagementSystemApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManagementSystemInfrastructure.Implementation
{
    public class EmailServicemImplementation : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailServicemImplementation(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(EmailModel emailModel)
        {
            var emailMessage = new MimeMessage();
            var from = _configuration["EmailSettings:From"];
            emailMessage.From.Add(new MailboxAddress("PasswordManager", from));
            emailMessage.To.Add(new MailboxAddress(null, emailModel.To));
            emailMessage.Subject = emailModel.Subject;

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format(emailModel.Body)
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_configuration["EmailSettings:SmtpServer"], 465, true);
                    client.Authenticate(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
                    client.Send(emailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
