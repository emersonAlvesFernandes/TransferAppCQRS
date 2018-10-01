using MediatR;
using TransferAppCQRS.Domain.Core.Bus;
using TransferAppCQRS.Domain.Core.Commands;
using TransferAppCQRS.Domain.Core.Notifications;
using TransferAppCQRS.Domain.Interfaces;

namespace TransferAppCQRS.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;

        private readonly IMediatorHandler _bus;

        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(IUnitOfWork uow, 
            IMediatorHandler bus, 
            INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
            _bus = bus;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                //  Como o Mediatr Sabe qual é o Handler que recebe a notificação 
                //  se ele passa um Domain Notification ao invés de um CustomerRegisteredEvent
                _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications()) return false;
            var commandResponse = _uow.Commit();
            if (commandResponse.Success) return true;

            _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
            return false;
        }                
    }
}
