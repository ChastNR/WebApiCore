using System;
using System.Threading.Tasks;

using DataAccess.Contracts;
using DataAccess.Interfaces.Base;

namespace DataAccess.Interfaces
{
    public interface IUserRepository : ISqlRepository
    {
        /// <summary>
        /// Check if there is another user with the same email or phone number in database
        /// </summary>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        /// <returns>boolean</returns>
        Task<bool> AnotherUserWithSamePropsAsync(string email, string phoneNumber);
        
        /// <summary>
        /// Get user from database by login
        /// </summary>
        /// <param name="login"></param>
        /// <returns>User</returns>
        Task<User> GetUserByEmailOrPhoneNumberAsync(string login);
        
        /// <summary>
        /// Insert user in database and return its id
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User id</returns>
        Task<Guid> InsertUserWithReturnIdAsync(User user);
    }
}