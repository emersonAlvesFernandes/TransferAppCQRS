using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Domain.ModelsNoSql
{
    public class CustomerReadNoSql
    {
        public CustomerReadNoSql()
        {

        }

        public CustomerReadNoSql(Customer customer)
        {
            Name = customer.Name;
            Email = customer.Email;
            BirthDate = customer.BirthDate;
            _id = customer.Id;
        }

        [BsonId]
        public Guid _id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
