using MongoDB.Driver;
using TransferAppCQRS.Domain.ModelsNoSql;
using TransferAppCQRS.Domain.ModelsNoSql.Contracts;

namespace TransferAppCQRS.Infra.Data.NoSql.Repositories
{
    public class CustomerWriteRepository : ICustomerWriteNoSqlRepository
    {
        private readonly IMongoCollection<CustomerReadNoSql> collection;
        public CustomerWriteRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("TransferApp");
            collection = db.GetCollection<CustomerReadNoSql>("customers");
        }
        public void InsertOne(CustomerReadNoSql customer)
            => collection.InsertOne(customer);
    }
}
