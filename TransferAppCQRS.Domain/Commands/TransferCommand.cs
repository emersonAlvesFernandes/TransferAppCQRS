using System;
using TransferAppCQRS.Domain.Core.Commands;
using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Domain.Commands
{
    public abstract class TransferCommand : Command
    {
        public Guid Id { get; protected set; }

        public Account Origin { get; protected set; }

        public Account Recipient { get; protected set; }

        public string Description { get; protected set; }

        public DateTime? ScheduledDate { get; protected set; }

        public double Value { get; protected set; }
    }
}
