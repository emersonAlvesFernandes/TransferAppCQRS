using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TransferAppCQRS.repository;
using TransferAppCQRS.TransferNoSql.model;

namespace TransferAppCQRS.TransferNoSql.model
{

    [SerializableAttribute]
    public class Transfer
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public Guid Id { get; set; }
        public Guid OriginGuid { get; set; }
        public Account Origin { get; set; }
        public Guid RecipientGuid { get; set; }
        public Account Recipient { get; set; }
        public string Description { get; set; }
        public DateTime ScheduledDate { get; set; }
        public double Value { get; set; }

        public void Save(IConfiguration _config)
        {
            new MongoDbService<Transfer>(_config, "transferCollection")
                .InsertOne(this);
        }
    }
}