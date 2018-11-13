using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TransferAppCQRS.Domain.Interfaces;

namespace TransferAppCQRS.Infra.Data.NoSql.Repositories
{
    public class BaseMongoRepository<T> : 
        IBaseWriteRepository<T>, IBaseReadRepository<T> where T : class
    {
        private readonly string _hostName;
        private readonly string _port;
        private readonly string _database;
        private readonly IMongoCollection<T> _collection;

        public BaseMongoRepository(string collection)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("customers");
            _collection = db.GetCollection<T>(collection);
        }
        public BaseMongoRepository(IConfiguration _config, string collection)
        {
            _hostName = _config["RabbitMq:Hostname"];
            _port = _config["RabbitMq:Port"];
            _database = _config["RabbitMq:Database"];

            var client = new MongoClient($"{_hostName}:{_port}");
            var db = client.GetDatabase(_database);
            _collection = db.GetCollection<T>(collection);
        }                
        public BaseMongoRepository(IHostingEnvironment env, string collection)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            IConfiguration _config = builder.Build();

            _hostName = _config["MongoDb:Hostname"];
            _port = _config["MongoDb:Port"];
            _database = _config["MongoDb:Database"];

            var client = new MongoClient($"{_hostName}:{_port}");
            var db = client.GetDatabase(_database);
            _collection = db.GetCollection<T>(collection);
        }

        public void InsertOne(T obj) => _collection.InsertOne(obj);        
        public List<T> GetAll() => _collection.Find(_ => true).ToList();
        public T GetById(Guid id) => _collection.Find(_ => true).FirstOrDefault();
        public void InsertMany(List<T> objCollection) => _collection.InsertMany(objCollection);        
    }
}
