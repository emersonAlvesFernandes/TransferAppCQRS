using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TransferAppCQRS.Domain.Commands;
using TransferAppCQRS.Domain.Core.Bus;
using TransferAppCQRS.Domain.Core.Notifications;
using TransferAppCQRS.Domain.Events;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Domain.CommandHandlers
{
    public class TransferCommandHandler : CommandHandler, INotificationHandler<RegisterNewTransferCommand>
    {
        private readonly TransferSqlRepository _sqlRepository;
        private readonly IAccountRepository _sqlAccountRepository;
        private readonly IMediatorHandler _bus;

        public TransferCommandHandler(
            TransferSqlRepository sqlRepository,
            IAccountRepository sqlAccountRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _sqlRepository = sqlRepository;
            _sqlAccountRepository = sqlAccountRepository;
            _bus = bus;
        }

        public Task Handle(RegisterNewTransferCommand command, CancellationToken cancellationToken)
        {

            if (command.IsValid())
            {
                NotifyValidationErrors(command);
                return Task.FromResult<object>(null);
            }

            ValidateFunds(command);

            ValidateRecipientAccount(command);

            var transfer = new Transfer(Guid.NewGuid(), command.Origin, command.Recipient, command.Description, command.ScheduledDate, command.Value);

            _sqlRepository.Add(transfer);
            _sqlAccountRepository.UpdateBalance(command.Origin.Agency, command.Origin.Number, command.Value);
                        
            if (Commit())
                _bus.RaiseEvent(new TransferRegisteredEvent(transfer.Id, transfer.Origin, transfer.Recipient, transfer.ScheduledDate, transfer.Value));

            return Task.FromResult<RegisterNewTransferCommand>(command);
        }

        private bool ValidateFunds(RegisterNewTransferCommand command)
        {
            var actualBalance = _sqlAccountRepository.GetBalance(command.Origin.Agency, command.Origin.Number);

            if (actualBalance < command.Value)
            {
                _bus.RaiseEvent(new DomainNotification(command.MessageType, "Insuficient funds."));
                return false;
            }

            return true;
        }

        private bool ValidateRecipientAccount(RegisterNewTransferCommand command)
        {
            var destinationAccount = _sqlAccountRepository.Get(command.Recipient.Agency, command.Recipient.Number);

            if (destinationAccount == null)
            {
                _bus.RaiseEvent(new DomainNotification(command.MessageType, "Invalid Recipient."));
                return false;
            }

            return true;
        }
    }
}
