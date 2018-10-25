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
            
            var origin = _sqlAccountRepository.GetById(command.OriginId);
            if (origin == null)
            {
                _bus.RaiseEvent(new DomainNotification(command.MessageType, "Invalid Origin Account."));
                return Task.CompletedTask;
            }
                
            var recipient = _sqlAccountRepository.GetById(command.OriginId);
            if (recipient == null)
            {                
                _bus.RaiseEvent(new DomainNotification(command.MessageType, "Invalid Recipient."));
                return Task.CompletedTask;
            }

            ValidateFunds(command);

            var transfer = new Transfer(Guid.NewGuid(), origin, recipient, command.Description, command.ScheduledDate, command.Value);

            _sqlRepository.Add(transfer);

            this.UpdateTransferAccountsBalance(command);

            if (Commit())
                _bus.RaiseEvent(new TransferRegisteredEvent(transfer.Id, transfer.Origin, transfer.Recipient, transfer.ScheduledDate, transfer.Value));

            return Task.FromResult<RegisterNewTransferCommand>(command);
        }

        private bool ValidateFunds(RegisterNewTransferCommand command)
        {
            var actualBalance = _sqlAccountRepository.GetBalance(command.OriginId);

            if (actualBalance < command.Value)
            {
                _bus.RaiseEvent(new DomainNotification(command.MessageType, "Insuficient funds."));
                return false;
            }

            return true;
        }

        private void UpdateTransferAccountsBalance(RegisterNewTransferCommand command)
        {

            var originBalance = _sqlAccountRepository.GetBalance(command.OriginId);
            var updatedBalance = originBalance - command.Value;

            _sqlAccountRepository.UpdateBalance(command.OriginId, 1);

            _sqlAccountRepository.UpdateBalance(command.RecipientId, 1);
        }               
    }
}
