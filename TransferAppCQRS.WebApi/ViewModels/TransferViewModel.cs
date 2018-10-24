using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransferAppCQRS.WebApi.ViewModels
{
    public class TransferViewModel
    {
        public Guid Origin { get; set; }

        public Guid Recipient { get; set; }

        public string Description { get; set; }

        public DateTime ScheduledDate { get; set; }

        public double Value { get; set; }
    }
}
