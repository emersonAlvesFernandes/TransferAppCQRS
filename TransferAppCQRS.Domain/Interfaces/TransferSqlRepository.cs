using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Domain.Interfaces
{
    public interface TransferSqlRepository
    {
        Transfer Add(Transfer transfer);        
    }
}
