using System;
using System.Collections.Generic;
using System.Linq;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Infra.Data.DomainRepositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void Add(Customer obj)
        {
            
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public IQueryable<Customer> GetAll()
        {
            var userCollection = new List<Customer>
            {
                new Customer(Guid.NewGuid(), "john", "john@john.com", DateTime.Now.AddYears(-20)),
                new Customer(Guid.NewGuid(), "john", "john@john.com", DateTime.Now.AddYears(-21)),
                new Customer(Guid.NewGuid(), "john", "john@john.com", DateTime.Now.AddYears(-22)),
                new Customer(Guid.NewGuid(), "john", "john@john.com", DateTime.Now.AddYears(-23)),
            };

            return userCollection.AsQueryable();
        }

        public Customer GetByEmail(string email)
        {
            return null;            
        }

        public Customer GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Customer obj)
        {
            throw new NotImplementedException();
        }
    }
}
