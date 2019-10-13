using System.Threading.Tasks;
using DataRepository.Contracts;
using DataRepository.Interfaces.Base;

namespace DataRepository.Interfaces
{
    public interface IUserRepository : ISqlRepository
    {
        Task<bool> AnotherUserWithSameProps(string email, string phoneNumber);
        Task<User> GetUserByEmailOrPhoneNumber(string email, string phoneNumber);
    }
}