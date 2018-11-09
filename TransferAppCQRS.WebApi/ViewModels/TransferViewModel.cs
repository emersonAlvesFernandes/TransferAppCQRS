using System;

namespace TransferAppCQRS.WebApi.ViewModels
{
    public class TransferViewModel
    {
        public Guid OriginId { get; set; }

        public Guid RecipientId { get; set; }

        public string Description { get; set; }

        public DateTime ScheduledDate { get; set; }

        public double Value { get; set; }
    }
}
