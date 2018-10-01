using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TransferAppCQRS.Domain.Events;

namespace TransferAppCQRS.Domain.EventHandlers
{
    public class TransferEventHandler : INotificationHandler<TransferRegisteredEvent>
    {
        public Task Handle(TransferRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return null;            
            // Enfileirar push notification de notificação de transferência;
            // para cliente de origem e destino
        }
    }
}
