using System;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Domain.Models;
using TransferAppCQRS.Domain.Validations;

namespace TransferAppCQRS.Domain.Commands
{
    public class RegisterNewTransferCommand : TransferCommand
    {        
        private IAccountRepository _sqlAccountRepository;

        public RegisterNewTransferCommand(Guid originId, Guid recipientId, string description, DateTime? scheduledDate, double value)
        {
            OriginId = originId;
            RecipientId = recipientId;
            Description = description;
            ScheduledDate = scheduledDate == null ? DateTime.Now : scheduledDate;
            Value = value;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewTransferCommandValidation().Validate(this);            
            return ValidationResult.IsValid;
        }

        public bool IsValid(IAccountRepository sqlAccountRepository)
        {
            _sqlAccountRepository = sqlAccountRepository;

            ValidationResult = new RegisterNewTransferCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private void Validate_OriginAccountHasSuficientFunds(RegisterNewTransferCommand command)
        {
            var actualBalance = _sqlAccountRepository.GetBalance(command.OriginId);

            if (actualBalance < command.Value)
            {
                //_bus.RaiseEvent(new _transferDb(command.MessageType, "Insuficient funds."));
                OriginAccountHasSuficientFunds = false;
            }

            OriginAccountHasSuficientFunds = true;
        }
    }
}
