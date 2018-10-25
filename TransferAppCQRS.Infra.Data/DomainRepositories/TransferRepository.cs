using System;
using System.Collections.Generic;
using System.Linq;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Infra.Data.DomainRepositories
{
    public class TransferRepository : ITransferRepository
    {
        public void Add(Transfer transfer)
        {
            throw new NotImplementedException();
        }
    }
}
