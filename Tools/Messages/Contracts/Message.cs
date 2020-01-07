using MongoDB.Bson;

using DataAccess.Interfaces.Base;

namespace Tools.Messages.Contracts
{
    public class Message : IMongoDoc
    {
        public ObjectId Id { get; set; }
        
        public string From { get; set; }
        
        public string To { get; set; }
        
        public string Title { get; set; }
        
        public string Body { get; set; }
    }
}