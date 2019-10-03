using System;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        public short Age { get; set; }
    }
}