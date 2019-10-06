using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace UniversalWebApi.Extensions.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        public EmailSender(IOptions<EmailSettings> emailSettings) => _emailSettings = emailSettings.Value;

        public async Task SendEmailAsync(string mailTo, string mailSubject, string mailBody)
            => await new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort)
            {
                Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword),
                EnableSsl = true
            }.SendMailAsync(new MailMessage(
                new MailAddress(_emailSettings.FromEmail, _emailSettings.DisplayName),
                new MailAddress(mailTo))
            {
                Subject = mailSubject,
                Body = mailBody
            });
    }
}