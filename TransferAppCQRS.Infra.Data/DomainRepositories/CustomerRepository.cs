using System;
using System.Linq;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Domain.Models;
using TransferAppCQRS.Infra.Data.Context;

namespace TransferAppCQRS.Infra.Data.DomainRepositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {        
        public CustomerRepository(TransferAppCQRSContext Db) : base(Db)
        {

        }

        public Customer GetByEmail(string email) => DbSet.FirstOrDefault(x => x.Email == email);

        public new Customer GetById(Guid id) => DbSet.FirstOrDefault(x => x.Equals(id));
        
    }
}
