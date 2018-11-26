using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TransferAppCQRS.TransferNoSql.model
{
    [SerializableAttribute]
    public class Account
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public Guid Id { get; set; }
        public int Agency { get; set; }
        public int Number { get; set; }    
        public string Address { get; set; }                
        public Guid CustomerGuId { get; set; }
        public Customer Customer { get; set; }    
    }
}