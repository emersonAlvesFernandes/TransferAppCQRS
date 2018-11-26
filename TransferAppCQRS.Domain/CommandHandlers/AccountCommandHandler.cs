using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransferAppCQRS.Domain.Commands;
using TransferAppCQRS.Domain.Core.Bus;
using TransferAppCQRS.Domain.Core.Notifications;
using TransferAppCQRS.Domain.Events;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Domain.Models;

namespace TransferAppCQRS.Domain.CommandHandlers
{
    public class AccountCommandHandler : CommandHandler, INotificationHandler<RegisterNewAccountCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMediatorHandler _bus;
        private readonly IQueueManager _queueManager;

        public AccountCommandHandler(
            IUnitOfWork uow,
            IAccountRepository accountRepository,
            IMediatorHandler bus,
            IQueueManager queueManager,
            INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _accountRepository = accountRepository;
            _bus = bus;
            _queueManager = queueManager;
        }

        public Task Handle(RegisterNewAccountCommand command, CancellationToken cancellationToken)
        {
            
            if (!command.IsValid(_accountRepository))
            {
                NotifyValidationErrors(command);
                return Task.FromResult<object>(null);
            }

            var account = new Account(command.Agency, command.Number, command.Address, command.CustomerGuid);

            _accountRepository.Add(account);

            if (Commit())
            {
                _bus.RaiseEvent(new AccountRegisteredEvent(account));

                _queueManager.Publish(account, "accountRK");
            }

            return Task.FromResult<RegisterNewAccountCommand>(command);
        }
    }
}
