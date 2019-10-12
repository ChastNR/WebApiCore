using System.Collections.Generic;
using System.Threading.Tasks;
using DataRepository.Interfaces.Base;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataRepository.Repositories.Base
{
    public class MongoDbSettings
    {
        public string DbConnection { get; set; }
        public int DbName { get; set; }
    }

    public class MongoRepository : IMongoRepository
    {
        private readonly IMongoDatabase _db;

        public MongoRepository(string connectionString, string dbName) =>
            _db = new MongoClient(connectionString).GetDatabase(dbName);

        public async Task<IEnumerable<T>> GetAsync<T>() where T : class, IMongoDoc
            => await _db.GetCollection<T>(typeof(T).Name).Find(_ => true).ToListAsync();

        public async Task<T> GetAsync<T>(ObjectId id) where T : class, IMongoDoc =>
            await _db.GetCollection<T>(typeof(T).Name).Find(doc => doc.Id == id).FirstOrDefaultAsync();

        public async Task AddAsync<T>(T t) where T : class, IMongoDoc =>
            await _db.GetCollection<T>(typeof(T).Name).InsertOneAsync(t);

        public async Task UpdateAsync<T>(T t) where T : class, IMongoDoc => await _db.GetCollection<T>(typeof(T).Name)
            .ReplaceOneAsync(Builders<T>.Filter.Eq("_id", t.Id), t);

        public async Task RemoveAsync<T>(ObjectId id) where T : class, IMongoDoc =>
            await _db.GetCollection<T>(typeof(T).Name).DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));

        //Additional methods
        public List<T> Find<T>(BsonDocument filter) where T : class, IMongoDoc =>
            _db.GetCollection<T>(typeof(T).Name).Find(filter).ToList();

        public List<T> Find<T>(string property, string value) where T : class, IMongoDoc =>
            _db.GetCollection<T>(typeof(T).Name).Find(new BsonDocument(property, value)).ToList();

        public List<T> FindDescSotted<T>(BsonDocument filter) where T : class, IMongoDoc =>
            _db.GetCollection<T>(typeof(T).Name).Find(filter).Sort("{timeStamp: -1}").ToList();

        public List<T> FindDescSotted<T>(string property, string value, string sortedString)
            where T : class, IMongoDoc => _db.GetCollection<T>(typeof(T).Name).Find(new BsonDocument(property, value))
            .Sort(sortedString).ToList();
    }
}