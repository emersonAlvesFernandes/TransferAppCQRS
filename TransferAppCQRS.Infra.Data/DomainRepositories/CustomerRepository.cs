using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Domain.Models;
using TransferAppCQRS.Infra.Data.Context;

namespace TransferAppCQRS.Infra.Data.DomainRepositories
{
    public class CustomerRepository : BaseRepository<Customer> , ICustomerRepository
    {

        public CustomerRepository(TransferAppContext context)
            : base(context)
        {

        }

        public Customer GetByAccount(int agency, int accountNumber)
        {
            throw new NotImplementedException();
        }


        //public IQueryable<Customer> GetAll()
        //{
        //    var userCollection = new List<Customer>
        //    {
        //        new Customer(Guid.NewGuid(), "john", "john@john.com", DateTime.Now.AddYears(-20)),
        //        new Customer(Guid.NewGuid(), "john", "john@john.com", DateTime.Now.AddYears(-21)),
        //        new Customer(Guid.NewGuid(), "john", "john@john.com", DateTime.Now.AddYears(-22)),
        //        new Customer(Guid.NewGuid(), "john", "john@john.com", DateTime.Now.AddYears(-23)),
        //    };

        //    return userCollection.AsQueryable();
        //}

        public Customer GetByEmail(string email) 
            => DbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
        
    }
}
