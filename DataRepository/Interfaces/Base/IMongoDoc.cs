using MongoDB.Bson;

namespace DataRepository.Interfaces.Base
{
    public interface IMongoDoc
    {
        ObjectId Id { get; set; }
    }
}