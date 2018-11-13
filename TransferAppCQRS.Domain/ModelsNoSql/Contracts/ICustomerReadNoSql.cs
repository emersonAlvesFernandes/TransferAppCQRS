using TransferAppCQRS.Domain.Interfaces;

namespace TransferAppCQRS.Domain.ModelsNoSql.Contracts
{
    public interface ICustomerReadNoSql : IBaseReadRepository<CustomerReadNoSql>
    {
        //IEnumerable<CustomerReadNoSql> GetAll();
    }
}
