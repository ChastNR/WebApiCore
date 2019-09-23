using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace UniversalWebApi.Extensions.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private EmailSettings EmailSettings { get; }
        public EmailSender(IOptions<EmailSettings> emailSettings) => EmailSettings = emailSettings.Value;

        public async Task SendEmailAsync(string mailTo, string mailSubject, string mailBody)
            => await new SmtpClient(EmailSettings.PrimaryDomain, EmailSettings.PrimaryPort)
            {
                Credentials = new NetworkCredential(EmailSettings.UsernameEmail, EmailSettings.UsernamePassword),
                EnableSsl = true
            }.SendMailAsync(new MailMessage(
                new MailAddress(EmailSettings.FromEmail, EmailSettings.DisplayName),
                new MailAddress(mailTo))
            {
                Subject = mailSubject,
                Body = mailBody
            });
    }
}