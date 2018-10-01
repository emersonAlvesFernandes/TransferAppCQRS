using TransferAppCQRS.Domain.Core.Models;

namespace TransferAppCQRS.Domain.Models
{
    public class Account : Entity
    {
        public Account(int agency, int number, string address)
        {
            Agency = agency;
            Number = number;
            Address = address;
        }

        public int Agency { get; private set; }

        public int Number { get; private set; }

        public string Address { get; private set; }

        public int CustomerId { get; private set; }
    }
}
