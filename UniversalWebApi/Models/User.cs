using System;
using MongoDB.Bson;
using MongoRepository.Interfaces;

namespace UniversalWebApi.Models
{
    [Serializable]
    public class User : BasicUser
    {
        public int Id { get; set; }
    }

    public class MUser : BasicUser, IMongoDoc
    {
        public ObjectId Id { get; set; }
    }

    [Serializable]
    public class BasicUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}