using System;

namespace TransferAppCQRS.Domain.Core.Events
{
    public class Event : Message
    {
        public DateTime Timestamp { get; private set; }

        public Guid AggregateId { get; protected set; }

        public Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
