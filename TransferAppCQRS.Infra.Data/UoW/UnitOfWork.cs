using System;
using System.Collections.Generic;
using System.Text;
using TransferAppCQRS.Domain.Core.Commands;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Infra.Data.Context;

namespace TransferAppCQRS.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransferAppContext _context;

        public UnitOfWork(TransferAppContext context)
        {
            _context = context;
        }

        public CommandResponse Commit()
        {
            var rowsAffected = _context.SaveChanges();
            return new CommandResponse(rowsAffected > 0);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
