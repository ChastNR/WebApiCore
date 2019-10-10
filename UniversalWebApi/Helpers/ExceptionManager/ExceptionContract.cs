using System;
using DataRepository.Interfaces.Base;
using MongoDB.Bson;

namespace UniversalWebApi.Helpers.ExceptionManager
{
    public class ExceptionContract : BasicExceptionContract
    {
        public int Id { get; set; }
    }

    public class MExceptionContract : BasicExceptionContract, IMongoDoc
    {
        public ObjectId Id { get; set; }
    }

    public class BasicExceptionContract
    {
        public string Message { get; set; }
        public string Method { get; set; }
        public string Class { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}