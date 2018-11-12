using System;
using System.Collections.Generic;
using System.Text;

namespace TransferAppCQRS.Domain.ModelsNoSql.Contracts
{
    public interface ICustomerReadNoSql
    {
        IEnumerable<CustomerReadNoSql> GetAll();
    }
}
