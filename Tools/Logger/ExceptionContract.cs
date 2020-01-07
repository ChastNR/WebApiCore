using System;
using DataAccess.Interfaces.Base;
using MongoDB.Bson;

namespace Tools.Logger
{
    public class ExceptionContract : IMongoDoc
    {
        public ObjectId Id { get; set; }
        public string Path { get; set; }
        public string Method { get; set; }
        public string IpAddress { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}