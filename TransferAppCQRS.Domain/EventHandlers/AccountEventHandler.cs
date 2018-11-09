using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TransferAppCQRS.Domain.Events;

namespace TransferAppCQRS.Domain.EventHandlers
{
    public class AccountEventHandler : INotificationHandler<AccountRegisteredEvent>
    {
        public Task Handle(AccountRegisteredEvent notification, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail
            return null;
        }
    }
}
