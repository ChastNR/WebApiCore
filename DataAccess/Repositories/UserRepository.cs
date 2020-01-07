using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Dapper;

using DataAccess.Contracts;
using DataAccess.Interfaces;
using DataAccess.Repositories.Base;

namespace DataAccess.Repositories
{
    public class UserRepository : SqlRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }
        
        public async Task<bool> AnotherUserWithSamePropsAsync(string email, string phoneNumber)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM [User] WHERE Email=@Email OR PhoneNumber=@PhoneNumber",
                new { Email = email, PhoneNumber = phoneNumber }) != null;
        }

        public async Task<User> GetUserByEmailOrPhoneNumberAsync(string login)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM [User] WHERE Email=@Email OR PhoneNumber=@PhoneNumber",
                new { Email = login, PhoneNumber = login });
        }

        public async Task<Guid> InsertUserWithReturnIdAsync(User user)
        {
            using var connection = CreateConnection();
            return (Guid) await connection.ExecuteScalarAsync(GenerateInsertQueryWithReturnId<User>(), user);
        }
    }
}