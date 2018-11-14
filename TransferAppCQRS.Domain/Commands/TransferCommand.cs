using System;
using TransferAppCQRS.Domain.Core.Commands;

namespace TransferAppCQRS.Domain.Commands
{
    public abstract class TransferCommand : Command
    {
        public Guid Id { get; protected set; }

        //public Account Origin { get; protected set; }
        public Guid OriginId { get; set; }

        //public Account Recipient { get; protected set; }
        public Guid RecipientId { get; set; }

        public string Description { get; protected set; }

        public DateTime? ScheduledDate { get; protected set; }

        public double Value { get; protected set; }


        public bool OriginAccountHasSuficientFunds { get; set; }
    }
}
