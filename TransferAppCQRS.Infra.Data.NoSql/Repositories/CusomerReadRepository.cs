using Microsoft.AspNetCore.Hosting;
using TransferAppCQRS.Domain.ModelsNoSql;
using TransferAppCQRS.Domain.ModelsNoSql.Contracts;

namespace TransferAppCQRS.Infra.Data.NoSql.Repositories
{
    public class CusomerReadRepository : BaseMongoRepository<CustomerReadNoSql>, ICustomerReadNoSql
    {
        private const string _collection = "customers";
        public CusomerReadRepository(IHostingEnvironment env) : base(env, _collection)
        {
            
        }    
    }
}
