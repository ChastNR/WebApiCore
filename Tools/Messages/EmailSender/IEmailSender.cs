using System.Threading.Tasks;

namespace Tools.Messages.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string mailTo, string mailSubject, string mailBody);
    }
}