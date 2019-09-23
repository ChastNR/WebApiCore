using System.Threading.Tasks;

namespace UniversalWebApi.Extensions.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string mailTo, string mailSubject, string mailBody);
    }
}