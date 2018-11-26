using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TransferAppCQRS.CustomerAccount.repository;

namespace TransferAppCQRS.CustomerAccount.model
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
        

        public void Save(IConfiguration _config)
        {
            var customer = new MongoDbService<Customer>(_config, "customerCollection")
                .GetById(CustomerGuId);
            this.Customer = customer;

            new MongoDbService<Account>(_config, "accountCollection")
                .InsertOne(this);                    
        }
    }

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