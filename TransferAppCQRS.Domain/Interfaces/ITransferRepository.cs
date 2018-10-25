using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Domain.Interfaces
{
    public interface ITransferRepository
    {        
        void Add(Transfer transfer);
    }
}
