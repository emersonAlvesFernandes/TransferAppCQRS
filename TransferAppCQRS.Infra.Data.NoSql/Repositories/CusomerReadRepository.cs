using MongoDB.Driver;
using System.Collections.Generic;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Domain.Models;
using TransferAppCQRS.Domain.ModelsNoSql;
using TransferAppCQRS.Domain.ModelsNoSql.Contracts;

namespace TransferAppCQRS.Infra.Data.NoSql.Repositories
{
    public class CusomerReadRepository : ICustomerReadNoSql
    {
        private readonly IMongoCollection<CustomerReadNoSql> collection;
        public CusomerReadRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("TransferApp");
            collection = db.GetCollection<CustomerReadNoSql>("customers");
        }
        public IEnumerable<CustomerReadNoSql> GetAll()
            => collection.Find(_ => true).ToList();        
    }
}
