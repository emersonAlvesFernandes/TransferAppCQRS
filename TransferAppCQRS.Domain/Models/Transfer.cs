using System;
using TransferAppCQRS.Domain.Core.Models;

namespace TransferAppCQRS.Domain.Models
{
    public class Transfer : Entity
    {
        public Transfer(Guid id, Account origin, Account recipient, string description, DateTime? datetime, double value)
        {
            Id = id;
            Origin = origin;
            Recipient = recipient;
            Description = description;
            ScheduledDate = datetime == null ? DateTime.Now : ScheduledDate;
            Value = value;
        }

        public Account Origin { get; private set; }

        public Account Recipient { get; private set; }

        public string Description { get; private set; }

        public DateTime ScheduledDate { get; private set; }

        public double Value { get; set; }
    }
}
