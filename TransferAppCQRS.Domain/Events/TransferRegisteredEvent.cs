using System;
using TransferAppCQRS.Domain.Core.Events;
using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Domain.Events
{
    public class TransferRegisteredEvent : Event
    {
        public TransferRegisteredEvent(Guid id, Account origin, Account recipient, DateTime datetime, double value)
        {
            Id = id;
            Origin = origin;
            Recipient = recipient;            
            ScheduledDate = datetime;
            Value = value;
        }

        public Guid Id { get; private set; }

        public Account Origin { get; private set; }

        public Account Recipient { get; private set; }
        
        public DateTime ScheduledDate { get; private set; }

        public double Value { get; set; }
    }
}
