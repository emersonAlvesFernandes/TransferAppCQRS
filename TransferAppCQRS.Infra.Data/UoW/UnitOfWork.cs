using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Infra.Data.Context;

namespace TransferAppCQRS.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransferAppCQRSContext _context;
        public UnitOfWork(TransferAppCQRSContext context)
        {
            _context = context;
        }

        public bool Commit() => _context.SaveChanges() > 0;
        public void Dispose() => _context.Dispose();
    }
}
