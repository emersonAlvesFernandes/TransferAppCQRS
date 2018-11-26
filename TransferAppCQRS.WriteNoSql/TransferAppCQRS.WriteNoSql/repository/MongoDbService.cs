using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace TransferAppCQRS.WriteNoSql.repository
{
    public class MongoDbService<T> where T : class
    {
        private readonly string _hostName;
        private readonly string _port;
        private readonly string _database;
        private readonly IMongoCollection<T> _collection;
        public MongoDbService(IConfiguration _config, string collection)
        {
            _hostName = _config["MongoDb:Hostname"];
            _port = _config["MongoDb:Port"];
            _database = _config["MongoDb:Database"];

            var client = new MongoClient($"{_hostName}:{_port}");
            var db = client.GetDatabase(_database);
            // _collection = db.GetCollection<T>(_config["MongoDb:Collection"]);
            _collection = db.GetCollection<T>(collection);
        }
        public void InsertOne(T obj) => _collection.InsertOne(obj);
        public void InsertMany(List<T> objCollection) => _collection.InsertMany(objCollection);
        public T GetById(Guid id) => _collection
            .Find(Builders<T>.Filter.Eq("Id", id))
            .FirstOrDefault();
    }

    public class MongoDbAgeService
    {
        private readonly string _hostName;
        private readonly string _port;
        private readonly string _database;
        private readonly IMongoCollection<Age> _collection;
        public MongoDbAgeService(IConfiguration _config, string collection)
        {
            _hostName = _config["MongoDb:Hostname"];
            _port = _config["MongoDb:Port"];
            _database = _config["MongoDb:Database"];

            var client = new MongoClient($"{_hostName}:{_port}");
            var db = client.GetDatabase(_database);
            _collection = db.GetCollection<Age>(collection);
        }
        public void InsertOne(Age obj) => _collection.InsertOne(obj);

        public Age  GetByRange(int from, int to)
        {            
            var builder = Builders<Age>.Filter;
            
            var filter = builder.Eq(x => x.From, from) & builder.Eq(x => x.From, from);
            
            return _collection.Find(filter).FirstOrDefault();             
        }

        public void  UpdateRange(int from, int to, Customer c)
        {            
            var builder = Builders<Age>.Filter;

            var filter = builder.Eq(x => x.From, from) & builder.Eq(x => x.From, from);

            var update = Builders<Age>.Update.Push("Customers", c);
            
            _collection.FindOneAndUpdateAsync(filter, update);             
        }
    }
}