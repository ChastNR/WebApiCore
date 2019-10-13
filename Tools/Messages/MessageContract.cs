using DataRepository.Interfaces.Base;
using MongoDB.Bson;

namespace Tools.Messages
{
    public class MessageContract : IMongoDoc
    {
        public ObjectId Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}