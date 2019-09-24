using System.Collections.Generic;
using System.ComponentModel;
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
    public class DataRepository : IDataRepository
    {
        private readonly string _connectionString;

        public DataRepository(string connectionString) => _connectionString = connectionString;

        private IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        private static IEnumerable<PropertyInfo> GetProperties<T>()
            where T : class => typeof(T).GetProperties();

        public IEnumerable<T> GetAll<T>()
            where T : class
        {
            using (var connection = CreateConnection())
            {
                return connection.Query<T>($"SELECT * FROM [{typeof(T).Name}]");
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
            where T : class
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<T>($"SELECT * FROM [{typeof(T).Name}]");
            }
        }

        public async Task DeleteRowAsync<T>(int id)
            where T : class
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync($"DELETE FROM [{typeof(T).Name}] WHERE Id=@Id", new { Id = id });
            }
        }

        public T Get<T>(int id)
            where T : class
        {
            using (var connection = CreateConnection())
            {
                var result = connection.QuerySingleOrDefault<T>($"SELECT * FROM [{typeof(T).Name}] WHERE Id=@Id", new { Id = id });
                if (result == null)
                    throw new KeyNotFoundException($"{typeof(T).Name} with id {id} could not be found.");

                return result;
            }
        }

        public T Get<T>(string condition)
            where T : class
        {
            using (var connection = CreateConnection())
            {
                if (string.IsNullOrEmpty(condition)) return null;
                var result = connection.QuerySingleOrDefault<T>($"SELECT * FROM [{typeof(T).Name}] WHERE {condition}");
                if (result == null)
                    throw new KeyNotFoundException($"{typeof(T).Name} could not be found.");

                return result;
            }
        }

        public async Task<T> GetAsync<T>(int id)
            where T : class
        {
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM [{typeof(T).Name}] WHERE Id=@Id", new { Id = id });
                if (result == null)
                    throw new KeyNotFoundException($"{typeof(T).Name} with id {id} could not be found.");

                return result;
            }
        }

        public async Task<int> SaveRangeAsync<T>(IEnumerable<T> list)
            where T : class
        {
            var inserted = 0;
            var query = GenerateInsertQuery<T>();

            using (var connection = CreateConnection())
            {
                inserted += await connection.ExecuteAsync(query, list);
            }

            return inserted;
        }

        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            var list = (from prop in listOfProperties
                        let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                        where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                        select prop.Name).ToList();
            list.Remove("Id");
            return list;
        }


        public async Task InsertAsync<T>(T t)
            where T : class
        {
            var query = GenerateInsertQuery<T>();
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(query, t);
            }
        }

        private static string GenerateInsertQuery<T>()
            where T : class
        {
            var insertQuery = new StringBuilder($"INSERT INTO [{typeof(T).Name}] ");

            insertQuery.Append("(");

            var properties = GenerateListOfProperties(GetProperties<T>());
            properties.ForEach(prop => { insertQuery.Append($"{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");

            return insertQuery.ToString();
        }

        public async Task UpdateAsync<T>(T t)
            where T : class
        {
            var query = GenerateUpdateQuery<T>();
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(query, t);
            }
        }

        private static string GenerateUpdateQuery<T>()
            where T : class
        {
            var updateQuery = new StringBuilder($"UPDATE [{typeof(T).Name}] SET ");
            var properties = GenerateListOfProperties(GetProperties<T>());

            properties.ForEach(property =>
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