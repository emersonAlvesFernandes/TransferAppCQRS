using System;
using MediatR;

namespace TransferAppCQRS.Domain.Core.Events
{
    public abstract class Message : INotification
    {
        public string MessageType { get; set; }

        public Guid AgregatedId { get; set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
