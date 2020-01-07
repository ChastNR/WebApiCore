using System.Threading.Tasks;

using AuthenticationProcessor.Contracts;

namespace AuthenticationProcessor.Interfaces
{
    public interface IAuthProcessor
    {
        Task<string> Login(LoginModel contract);
        
        Task<bool> Register(RegistrationModel contract);
    }
}