using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TransferAppCQRS.TransferNoSql.model
{
    [SerializableAttribute]
    public class Customer
    {
        [BsonId]        
        public ObjectId _id { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}