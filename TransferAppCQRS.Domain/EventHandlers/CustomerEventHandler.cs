using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TransferAppCQRS.Domain.Events;

namespace TransferAppCQRS.Domain.EventHandlers
{
    public class CustomerEventHandler :
        INotificationHandler<CustomerRegisteredEvent>
    {
        public Task Handle(CustomerRegisteredEvent notification, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail
            //return Task.FromResult<object>(null);
            return null;
        }
    }
}
