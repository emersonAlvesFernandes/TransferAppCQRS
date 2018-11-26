using Microsoft.EntityFrameworkCore;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Domain.Models;
using TransferAppCQRS.Infra.Data.Context;

namespace TransferAppCQRS.Infra.Data.DomainRepositories
{
    public class TransferRepository : ITransferRepository
    {
        protected readonly TransferAppCQRSContext Db;
        protected readonly DbSet<Transfer> DbSet;

        public TransferRepository(TransferAppCQRSContext context)
        {
            Db = context;
            DbSet = Db.Set<Transfer>();
        }

        public void Add(Transfer transfer) => DbSet.Add(transfer);                    
    }
}
