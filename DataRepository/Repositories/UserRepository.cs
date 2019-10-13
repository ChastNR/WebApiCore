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
            ConnectionString = connectionString;
        }

        public async Task<bool> AnotherUserWithSameProps(string email, string phoneNumber)
            => await GetUserByEmailOrPhoneNumber(email, phoneNumber) != null;

        public async Task<User> GetUserByEmailOrPhoneNumber(string email, string phoneNumber)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
                $"SELECT * FROM [{typeof(User).Name}] WHERE Email=@Email OR PhoneNumber=@PhoneNumber",
                new {Email = email, PhoneNumber = phoneNumber});
        }
    }
}