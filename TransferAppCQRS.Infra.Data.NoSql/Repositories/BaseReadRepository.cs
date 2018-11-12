using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Infra.Data.NoSql.DataModels;

namespace TransferAppCQRS.Infra.Data.NoSql.Repositories
{
    public class BaseMongoRepository<T> : 
        IBaseWriteRepository<T>, IBaseReadRepository<T> where T : class
    {
        private readonly IMongoCollection<T> collection;

        public BaseMongoRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("customers");
            collection = db.GetCollection<T>("pedidosCollection");
        }

        public void Add(T obj)
        {
            collection.InsertOne(obj);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            return collection.Find(_ => true).ToList();
        }

        public T GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
