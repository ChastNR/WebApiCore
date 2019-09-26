using MongoDB.Bson;

namespace MongoRepository.Interfaces
{
    public interface IMongoDoc
    {
        ObjectId Id { get; set; }
    }
}