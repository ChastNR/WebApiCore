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

        public async Task SendMessageAsync(Message contract)
        {
            var message = new MailMessage(
                FromAddress(),
                new MailAddress(contract.To))
            {
                Subject = contract.Title,
                Body = contract.Body
            };
            
            await GetClient().SendMailAsync(message);
        }
        
        public async Task SendServiceMessageAsync(ServiceMessage message)
        {
            var mailMessage = new MailMessage(FromAddress(), new MailAddress("liltihon@tut.by"))
            {
                Subject = message.Title,
                Body = message.Body
            };
            
            await GetClient().SendMailAsync(mailMessage);
        }

        private SmtpClient GetClient()
        {
            return new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort)
            {
                Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword),
                EnableSsl = false
            };
        }

        private MailAddress FromAddress()
        {
            return new MailAddress(_emailSettings.FromEmail, _emailSettings.DisplayName);
        }
    }
}