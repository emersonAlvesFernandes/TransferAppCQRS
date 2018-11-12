using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TransferAppCQRS.Infra.Data.NoSql.DataModels
{
    public class CustomerRead
    {
        [BsonId]
        ObjectId _id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
    }
}
