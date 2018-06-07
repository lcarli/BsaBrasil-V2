using BsaBrasil.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BsaBrasil.Models
{
    public class AuthMessageSender : IEmailSender
    {
        public AuthMessageSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public EmailSettings _emailSettings { get; }

        public Task SendEmailAsync(string email, string subject, string message, string name)
        {

            Execute(email, subject, message, name).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(string email, string subject, string message, string name)
        {
            try
            {
                string toEmail = _emailSettings.ToEmail;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "BSA Site Meeting")
                };
                mail.To.Add(new MailAddress(toEmail));

                mail.Subject = subject;
                mail.Body = $"BSA SITE MEETING - {message} - SENDER: {name} - {email}";
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                //TODO: Create a Logger user interface ILogger and storage this into a Blob Storage
            }
        }
    }
}
