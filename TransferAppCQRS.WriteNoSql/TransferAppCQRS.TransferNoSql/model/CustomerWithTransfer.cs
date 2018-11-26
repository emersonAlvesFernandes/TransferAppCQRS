using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TransferAppCQRS.repository;

namespace TransferAppCQRS.TransferNoSql.model
{
    [SerializableAttribute]
    public abstract class CustomerWithTransfer
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public Guid Id { get; set; }      //ok  
        public string Name { get; set; } //ok
        public Double CurrentBalance { get; set; }
        public AccountSummary Account { get; set; } //ok

        public List<TransferSummary> DoneTransfers { get; set; } //ok
        public List<TransferSummary> ReceivedTransfers { get; set; }

        [BsonIgnore]
        protected readonly IConfiguration _config;
        [BsonIgnore]
        protected readonly Transfer _transfer;

        public CustomerWithTransfer(IConfiguration config, Transfer transfer)
        {
            this._config = config;
            this._transfer = transfer;
        }

        public abstract void  SetAccount();                
        public abstract void HandleCustomer();


    }
}