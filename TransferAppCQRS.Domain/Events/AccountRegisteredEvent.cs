using TransferAppCQRS.Domain.Core.Events;
using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Domain.Events
{
    public class AccountRegisteredEvent : Event
    {
        public AccountRegisteredEvent(Account account)
        {
            Account = account;
        }
        public Account Account { get; set; }
    }
}
