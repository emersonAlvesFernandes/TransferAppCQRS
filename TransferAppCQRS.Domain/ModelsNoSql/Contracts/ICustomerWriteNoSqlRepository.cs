using TransferAppCQRS.Domain.Interfaces;

namespace TransferAppCQRS.Domain.ModelsNoSql.Contracts
{
    public interface ICustomerWriteNoSqlRepository : IBaseWriteRepository<CustomerReadNoSql>
    {
        
    }
}
