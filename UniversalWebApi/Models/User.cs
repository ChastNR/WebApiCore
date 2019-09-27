using System;
using MongoDB.Bson;
using MongoRepository.Interfaces;

namespace UniversalWebApi.Models
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
    
    public class MUser : IMongoDoc
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}