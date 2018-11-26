using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TransferAppCQRS.repository;
using TransferAppCQRS.TransferNoSql.model;

namespace TransferAppCQRS.TransferNoSql.model
{
    public class TransferSummary
    {        
        [BsonId]
        public ObjectId _id { get; set; }
        public Guid Id { get; set; }
        public Guid CustomerGuid { get; set; }
        public string CustomerName { get; set; }
        public Guid CustomerAccountGuid { get; set; }
        public int CustomerAccountAgency { get; set; }
        public int CustomerAccountNumber { get; set; }        
        public string Description { get; set; }
        public DateTime ScheduledDate { get; set; }
        public double Value { get; set; }
    }
}