using System;
using DataRepository.Interfaces.Base;
using MongoDB.Bson;

namespace Tools.Logger
{
    public class ExceptionContract : IMongoDoc
    {
        public ObjectId Id { get; set; }
        public string Message { get; set; }
        public string Method { get; set; }
        public string Class { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        //Additional Data
        public byte StatusCode { get; set; }
    }
}