using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Domain.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByEmail(string email);
    }
}
