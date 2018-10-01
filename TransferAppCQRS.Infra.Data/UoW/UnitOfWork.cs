using System;
using System.Collections.Generic;
using System.Text;
using TransferAppCQRS.Domain.Core.Commands;
using TransferAppCQRS.Domain.Interfaces;

namespace TransferAppCQRS.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        public CommandResponse Commit()
        {
            return new CommandResponse(true);
        }

        public void Dispose()
        {
           
        }
    }
}
