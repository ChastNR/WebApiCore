using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataRepository.Interfaces.Base;
using static DataRepository.DataAccessServices;

namespace DataRepository.Repositories.Base
{
    public class SqlRepository : ISqlRepository
    {
        protected IDbConnection CreateConnection() => new SqlConnection(ConnectionString);

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<T>($"SELECT * FROM [{typeof(T).Name}]");
        }

        public async Task<T> GetAsync<T>(object id) where T : class
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<T>($"SELECT * FROM [{typeof(T).Name}] WHERE Id=@Id",
                new { Id = id });
        }

        public async Task InsertAsync<T>(T t) where T : class
        {
            using var connection = CreateConnection();
            await connection.ExecuteAsync(GenerateInsertQuery<T>(), t);
        }

        public async Task<TOut> InsertAsyncWithReturnId<T, TOut>(T t) where T : class
        {
            using var connection = CreateConnection();
            return (TOut)await connection.ExecuteScalarAsync(GenerateInsertQueryWithReturnId<T>(), t);
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

        protected IEnumerable<PropertyInfo> GetProperties<T>() where T : class => typeof(T).GetProperties();

        protected List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            var list = listOfProperties.Select(property => property.Name).ToList();
            list.Remove("Id");
            return list;
        }

        protected string GenerateInsertQuery<T>() where T : class
        {
            var insertQuery = new StringBuilder($"INSERT INTO [{typeof(T).Name}] (");
            var properties = GenerateListOfProperties(GetProperties<T>());
            properties.ForEach(prop => insertQuery.Append($"{prop},"));
            insertQuery.Remove(insertQuery.Length - 1, 1).Append(") VALUES (");
            properties.ForEach(prop => insertQuery.Append($"@{prop},"));
            insertQuery.Remove(insertQuery.Length - 1, 1).Append(")");
            return insertQuery.ToString();
        }

        protected string GenerateInsertQueryWithReturnId<T>() where T : class
        {
            var insertQuery = new StringBuilder($"INSERT INTO [{typeof(T).Name}] (");
            var properties = GenerateListOfProperties(GetProperties<T>());
            properties.ForEach(prop => insertQuery.Append($"{prop},"));
            insertQuery.Remove(insertQuery.Length - 1, 1).Append(") OUTPUT Inserted.ID VALUES (");
            properties.ForEach(prop => insertQuery.Append($"@{prop},"));
            insertQuery.Remove(insertQuery.Length - 1, 1).Append(")");
            return insertQuery.ToString();
        }

        protected string GenerateUpdateQuery<T>() where T : class
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