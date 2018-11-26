using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TransferAppCQRS.TransferNoSql.model
{
    [SerializableAttribute]
    public class AccountSummary
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public Guid Id { get; set; }
        public int AgencyNumber { get; set; }
        public int Number { get; set; }    
        public string Address { get; set; }                
        
        public AccountSummary(Account a)
        {
           _id = a._id;
           Id = a.Id;
           AgencyNumber = a.Agency;
           a.Number = a.Number;
           Address = a.Address;           
        } 
        public AccountSummary()
        {
            
        } 
    }
}