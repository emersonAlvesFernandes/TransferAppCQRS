﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Domain.Models;
using TransferAppCQRS.Infra.Data.Context;

namespace TransferAppCQRS.Infra.Data.DomainRepositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(TransferAppCQRSContext Db) : base(Db)
        {
        }

        public Account Get(int agency, int number)
            => DbSet.FirstOrDefault(x => x.Agency == agency && x.Number == number);
        

        public double GetBalance(Guid guid)
        {
            return 100;
        }

        public new Account GetById(Guid id) 
            => DbSet.FirstOrDefault(x => x.Id == id);

        public Account GetFullById(Guid id)
            => Db.Accounts
                .Include(x => x.Customer)
                .FirstOrDefault(x => x.Id == id);

        public void UpdateBalance(Guid guid, double value)
        {
            //throw new NotImplementedException();
        }
    }
}
