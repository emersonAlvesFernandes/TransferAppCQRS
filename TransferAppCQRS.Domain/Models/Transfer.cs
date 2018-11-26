using System;
using TransferAppCQRS.Domain.Core.Models;

namespace TransferAppCQRS.Domain.Models
{
    public class Transfer : Entity
    {
        protected Transfer()
        {

        }

        public Transfer(Guid id, Account origin, Account recipient, string description, DateTime? datetime, double value)
        {
            Id = id;
            Origin = origin;
            OriginGuid = origin.Id;
            Recipient = recipient;
            RecipientGuid = recipient.Id;
            Description = description;



            //ScheduledDate = datetime == DateTime.MinValue() ? DateTime.Now : datetime;

            ScheduledDate = datetime.Value;

            Value = value;
        }

        public Guid OriginGuid { get; set; }
        public Account Origin { get; private set; }

        public Guid RecipientGuid { get; set; }
        public Account Recipient { get; private set; }

        public string Description { get; private set; }

        public DateTime ScheduledDate { get; private set; }

        public double Value { get; set; }
    }
}
