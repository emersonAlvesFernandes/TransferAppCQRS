using System;
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
