using System;
using MongoDB.Bson.Serialization.Attributes;

namespace TransferAppCQRS.model
{
    public class Customer
    {
        [BsonId]
        public Guid _id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}