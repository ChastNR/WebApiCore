using System.Threading.Tasks;
using Dapper;
using DataRepository.Contracts;
using DataRepository.Repositories.Base;

namespace DataRepository.Repositories
{
    public class UserRepository : SqlRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
            ConnectionString = connectionString;
        }
        
        public async Task<User> GetUserWithConditionAsync(string email, string phoneNumber)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
                $"SELECT * FROM [{typeof(User).Name}] WHERE Email=@Email OR PhoneNumber=@PhoneNumber",
                new {Email = email, PhoneNumber = phoneNumber});
        }
    }
}