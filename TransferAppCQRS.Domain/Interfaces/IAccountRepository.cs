using System;
using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Domain.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        double GetBalance(Guid guid);

        Account Get(int agency, int number);

        void UpdateBalance(Guid guid, double value);
    }
}
