using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Linq;

namespace TransferAppCQRS.CustomerAccount.repository
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
}