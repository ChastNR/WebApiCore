using System;
using DataRepository.Interfaces.Base;
using MongoDB.Bson;

namespace UniversalWebApi.Filters.Contracts
{
    public class ActionResultContract : IMongoDoc
    {
        public ObjectId Id { get; set; }
        public string Path { get; set; }
        public string Method { get; set; }
        public string IpAddress { get; set; }
        public int StatusCode { get; set; }
        public DateTime Time { get; set; }
        public string Result { get; set; }
    }
}