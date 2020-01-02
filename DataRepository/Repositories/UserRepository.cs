using System;
using System.Threading.Tasks;

using Dapper;

using DataRepository.Contracts;
using DataRepository.Interfaces;
using DataRepository.Repositories.Base;

namespace DataRepository.Repositories
{
    public class UserRepository : SqlRepository, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<bool> AnotherUserWithSameProps(string email, string phoneNumber)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM [User] WHERE Email=@Email OR PhoneNumber=@PhoneNumber",
                new { Email = email, PhoneNumber = phoneNumber }) != null;
        }

        public async Task<User> GetUserByEmailOrPhoneNumber(string login)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM [User] WHERE Email=@Email OR PhoneNumber=@PhoneNumber",
                new { Email = login, PhoneNumber = login });
        }

        public async Task<Guid> InsertUserAsyncWithReturnId(User user)
        {
            using var connection = CreateConnection();
            return (Guid) await connection.ExecuteScalarAsync(GenerateInsertQueryWithReturnId<User>(), user);
        }
    }
}