using System;
using TransferAppCQRS.Domain.Core.Models;

namespace TransferAppCQRS.Domain.Models
{
    public class Account : Entity
    {
        protected Account(){ }

        public Account(int agency, int number, string address, Guid customerGuId)
        {
            Agency = agency;
            Number = number;
            Address = address;
            CustomerGuId = customerGuId;
        }

        public int Agency { get; private set; }
        public int Number { get; private set; }    
        public string Address { get; private set; }


        public Guid CustomerGuId { get; set; }
        public Customer Customer { get; set; }

        
    }
}
