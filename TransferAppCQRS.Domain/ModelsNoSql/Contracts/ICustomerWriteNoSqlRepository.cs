using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Domain.ModelsNoSql.Contracts
{
    public interface ICustomerWriteNoSqlRepository
    {
        void InsertOne(CustomerReadNoSql customer);
    }
}
