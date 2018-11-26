﻿using System;
using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Domain.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Customer GetByEmail(string email);

        new Customer GetById(Guid id);
    }
}
