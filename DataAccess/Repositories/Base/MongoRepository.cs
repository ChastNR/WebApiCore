using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using MongoDB.Bson;
using MongoDB.Driver;

using DataAccess.Interfaces.Base;

namespace DataAccess.Repositories.Base
{
    public class MongoRepository : IMongoRepository
    {
        private readonly IMongoDatabase _db;
        
        public MongoRepository(string connectionString, string dbName)
        {
            _db = new MongoClient(connectionString).GetDatabase(dbName);
        }
        
        public MongoRepository(IConfiguration configuration)
        {
            _db = new MongoClient(configuration.GetConnectionString("Mongo"))
                .GetDatabase(configuration.GetConnectionString("MongoDbName"));
        }
        
        /// <summary>
        /// Get all elements from Mongo collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Collection of elements</returns>
        public async Task<IEnumerable<T>> GetAsync<T>() where T : class, IMongoDoc
        {
            return await _db.GetCollection<T>(typeof(T).Name).AsQueryable().ToListAsync();
        }
        
        /// <summary>
        /// Get one element from collection by id
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>T</returns>
        public async Task<T> GetAsync<T>(ObjectId id) where T : class, IMongoDoc
        {
            return await _db.GetCollection<T>(typeof(T).Name).Find(doc => doc.Id == id).FirstOrDefaultAsync();
        }
        
        /// <summary>
        /// Add element to collection
        /// </summary>
        /// <param name="t"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task AddAsync<T>(T t) where T : class, IMongoDoc
        {
            await _db.GetCollection<T>(typeof(T).Name).InsertOneAsync(t);
        }
        
        /// <summary>
        /// Update element in collection
        /// </summary>
        /// <param name="t"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task UpdateAsync<T>(T t) where T : class, IMongoDoc
        {
            await _db.GetCollection<T>(typeof(T).Name).ReplaceOneAsync(Builders<T>.Filter.Eq("_id", t.Id), t);
        } 
        
        /// <summary>
        /// Delete element from collection
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task RemoveAsync<T>(ObjectId id) where T : class, IMongoDoc
        {
            await _db.GetCollection<T>(typeof(T).Name).DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));
        }
            
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<List<T>> Find<T>(BsonDocument filter) where T : class, IMongoDoc
        {
            return await _db.GetCollection<T>(typeof(T).Name).Find(filter).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<List<T>> Find<T>(string property, string value) where T : class, IMongoDoc
        {
            return await _db.GetCollection<T>(typeof(T).Name).Find(new BsonDocument(property, value)).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<List<T>> FindDescSorted<T>(BsonDocument filter) where T : class, IMongoDoc
        {
            return await _db.GetCollection<T>(typeof(T).Name).Find(filter).Sort("{timeStamp: -1}").ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <param name="sortedString"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<List<T>> FindDescSorted<T>(string property, string value, string sortedString) where T : class, IMongoDoc
        {
            return await _db.GetCollection<T>(typeof(T).Name).Find(new BsonDocument(property, value)).Sort(sortedString).ToListAsync();
        }
    }
}