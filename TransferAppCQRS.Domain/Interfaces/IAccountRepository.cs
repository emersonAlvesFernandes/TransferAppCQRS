using System;
using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Domain.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        double GetBalance(Guid guid);
        Account Get(int agency, int number);
        new Account GetById(Guid id);
        Account GetFullById(Guid id);
        void UpdateBalance(Guid guid, double value);
    }
}
