using System.Threading.Tasks;
using AuthenticationProcessor.Contracts;

namespace AuthenticationProcessor.Interfaces
{
    public interface IAuthProcessor
    {
        Task<bool> Register(RegistrationContract contract);
        Task<string> Login(LoginContract contract);
    }
}