using System.Threading.Tasks;
using DataRepository.Contracts;
using DataRepository.Interfaces.Base;

namespace DataRepository.Interfaces
{
    public interface IUserRepository : ISqlRepository
    {
        Task<User> GetUserWithConditionAsync(string email, string phoneNumber);
    }
}