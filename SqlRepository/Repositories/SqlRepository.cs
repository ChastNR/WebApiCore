using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SqlRepository.Interfaces;

namespace SqlRepository.Repositories
{
    public class SqlRepository : ISqlRepository
    {
        private readonly string _connectionString;
        public SqlRepository(string connectionString) => _connectionString = connectionString;
        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<T>($"SELECT * FROM [{typeof(T).Name}]");
        }

        public async Task<T> GetAsync<T>(object id) where T : class
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<T>($"SELECT * FROM [{typeof(T).Name}] WHERE Id=@Id", new { Id = id });
        }

        public async Task InsertAsync<T>(T t) where T : class
        {
            using var connection = CreateConnection();
            await connection.ExecuteAsync(GenerateInsertQuery<T>(), t);
        }

        public async Task SaveRangeAsync<T>(IEnumerable<T> list) where T : class
        {
            using var connection = CreateConnection();
            await connection.ExecuteAsync(GenerateInsertQuery<T>(), list);
        }

        public async Task DeleteRowAsync<T>(object id) where T : class
        {
            using var connection = CreateConnection();
            await connection.ExecuteAsync($"DELETE FROM [{typeof(T).Name}] WHERE Id=@Id", new { Id = id });
        }

        public async Task UpdateAsync<T>(T t) where T : class
        {
            using var connection = CreateConnection();
            await connection.ExecuteAsync(GenerateUpdateQuery<T>(), t);
        }

        private IEnumerable<PropertyInfo> GetProperties<T>() where T : class => typeof(T).GetProperties();

        private List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            var list = listOfProperties.Select(property => property.Name).ToList();
            list.Remove("Id");
            return list;
        }

        private string GenerateInsertQuery<T>() where T : class
        {
            var insertQuery = new StringBuilder($"INSERT INTO [{typeof(T).Name}] (");
            var properties = GenerateListOfProperties(GetProperties<T>());
            properties.ForEach(prop => insertQuery.Append($"{prop},"));
            insertQuery.Remove(insertQuery.Length - 1, 1).Append(") VALUES (");
            properties.ForEach(prop => insertQuery.Append($"@{prop},"));
            insertQuery.Remove(insertQuery.Length - 1, 1).Append(")");
            return insertQuery.ToString();
        }

        private string GenerateUpdateQuery<T>() where T : class
        {
            var updateQuery = new StringBuilder($"UPDATE [{typeof(T).Name}] SET ");
            GenerateListOfProperties(GetProperties<T>()).ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });
            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append(" WHERE Id=@Id");
            return updateQuery.ToString();
        }
    }
}