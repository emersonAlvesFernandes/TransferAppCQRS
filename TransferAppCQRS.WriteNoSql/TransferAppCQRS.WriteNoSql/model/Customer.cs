using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TransferAppCQRS.WriteNoSql.repository;

namespace TransferAppCQRS.WriteNoSql
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

        public void Save(IConfiguration _config)
            => new MongoDbService<Customer>(_config, "customerCollection")
                .InsertOne(this);                    
    }

    [SerializableAttribute]
    public class Age
    {
        public Age(Customer customer)
        {
            //Customer = customer;
            Customers = new List<Customer>();
            Customers.Add(customer);
            SetAgeRange(customer);
        }
        
        [BsonId]
        public ObjectId _id { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public List<Customer> Customers { get; set; }

        protected void SetAgeRange(Customer customer)
        {            
            var today = DateTime.Today;            
            var age = today.Year - customer.BirthDate.Year;            
            if (customer.BirthDate > today.AddYears(-age)) age--;

            if(age >= 18 && age <= 25)
            {
                From = 18;
                To = 25;
                return;
            }            
            if(age >= 26 && age <= 30)
            {
                From = 26;
                To = 30;
                return;
            }
            if(age >= 31 && age <= 35)
            {
                From = 30;
                To = 35;
                return;
            }
            if(age >= 36 && age <= 40)
            {
                From = 36;
                To = 40;
                return;
            }
            if(age >= 41 && age <= 50)
            {
                From = 41;
                To = 50;
                return;
            }
            if(age >= 51)
            {
                From = 51;
                To = 99;
                return;
            }
        }
    
        public void Save(IConfiguration _config, Customer customer)
        {
            var _db = new MongoDbAgeService(_config, "customerByAgeCollection");
            
            var ageObj = _db.GetByRange(From, To);

            if(ageObj == null)
            {
                _db.InsertOne(this);
            }
            else
            {
                _db.UpdateRange(From, To, customer);
            }
        }
    }
}