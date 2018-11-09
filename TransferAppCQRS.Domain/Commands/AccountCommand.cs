using System;
using TransferAppCQRS.Domain.Core.Commands;

namespace TransferAppCQRS.Domain.Commands
{
    public abstract class AccountCommand : Command
    {
        public Guid Id { get; protected set; }
        public int Agency { get; protected set; }
        public int Number { get; protected set; }
        public string Address { get; protected set; }
        public Guid CustomerGuid { get; set; }
    }
}
