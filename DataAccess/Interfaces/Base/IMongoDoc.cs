using MongoDB.Bson;

namespace DataAccess.Interfaces.Base
{
    public interface IMongoDoc
    {
        ObjectId Id { get; set; }
    }
}