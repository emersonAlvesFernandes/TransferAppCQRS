using Microsoft.AspNetCore.Hosting;
using TransferAppCQRS.Domain.ModelsNoSql;
using TransferAppCQRS.Domain.ModelsNoSql.Contracts;

namespace TransferAppCQRS.Infra.Data.NoSql.Repositories
{
    public class CustomerWriteRepository : BaseMongoRepository<CustomerReadNoSql>, ICustomerWriteNoSqlRepository
    {

        private const string _collection = "customers";

        public CustomerWriteRepository(IHostingEnvironment env) : base(env, _collection)
        {            
        }        
    }
}
