﻿using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TransferAppCQRS.Domain.Commands;
using TransferAppCQRS.Domain.Core.Bus;
using TransferAppCQRS.Domain.Core.Notifications;
using TransferAppCQRS.Domain.Events;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Domain.Models;
using TransferAppCQRS.Domain.ModelsNoSql.Contracts;

namespace TransferAppCQRS.Domain.CommandHandlers
{
    public class CustomerCommandHandler : CommandHandler,
        INotificationHandler<RegisterNewCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerWriteNoSqlRepository _customerWriteNoSql;
        private readonly IMediatorHandler Bus;
        private readonly IQueueManager _queueManager;

        public CustomerCommandHandler(ICustomerRepository customerRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            IQueueManager queueManager,
            ICustomerWriteNoSqlRepository customerWriteNoSql,
            INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _customerRepository = customerRepository;
            _queueManager = queueManager;
            _customerWriteNoSql = customerWriteNoSql;
            Bus = bus;
        }

        Task INotificationHandler<RegisterNewCustomerCommand>.Handle(RegisterNewCustomerCommand message, CancellationToken cancellationToken)
        {
            //Validar
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult<object>(null);
            }

            // Transformar em entidade de domínio
            var customer = new Customer(Guid.NewGuid(), message.Name, message.Email, message.BirthDate);

            //orquestrar ações
            if (_customerRepository.GetByEmail(customer.Email) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The customer e-mail has already been taken."));
                return Task.FromResult<object>(null);
            }

            _customerRepository.Add(customer);

            //registrar evento
            if (Commit())
            {
                Bus.RaiseEvent(new CustomerRegisteredEvent(customer.Id, customer.Name, customer.Email, customer.BirthDate));
                
                _queueManager.Publish(customer, "customerRK");
            }

            return Task.FromResult<RegisterNewCustomerCommand>(message);
        }
    }
}
