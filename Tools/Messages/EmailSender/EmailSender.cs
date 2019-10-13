using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Tools.Messages.EmailSender
{
    public class EmailSender : IMessageSender
    {
        private readonly EmailSettings _emailSettings;
        public EmailSender(IOptions<EmailSettings> emailSettings) => _emailSettings = emailSettings.Value;

        public async Task SendMessageAsync(MessageContract contract)
            => await new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort)
            {
                Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword),
                EnableSsl = true
            }.SendMailAsync(new MailMessage(
                new MailAddress(_emailSettings.FromEmail, _emailSettings.DisplayName),
                new MailAddress(contract.To))
            {
                Subject = contract.Title,
                Body = contract.Body
            });
    }
}