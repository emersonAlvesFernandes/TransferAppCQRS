using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Domain.Interfaces
{
    public interface IAccountRepository
    {
        double GetBalance(int agency, int number);

        Account Get(int agency, int number);

        void UpdateBalance(int agency, int number, double value);
    }
}
