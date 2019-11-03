using System;
using DataRepository.Interfaces.Base;
using MongoDB.Bson;

namespace Tools.Logger
{
    public class ExceptionContract : IMongoDoc
    {
        public ObjectId Id { get; set; }
        public string Path { get; set; }
        public string Method { get; set; }
        public string Class { get; set; }
        public string IpAddress { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public int StatusCode { get; set; }
        public string Result { get; set; }
    }
}