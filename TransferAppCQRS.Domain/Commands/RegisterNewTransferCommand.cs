using System;
using TransferAppCQRS.Domain.Models;
using TransferAppCQRS.Domain.Validations;

namespace TransferAppCQRS.Domain.Commands
{
    public class RegisterNewTransferCommand : TransferCommand
    {
        public RegisterNewTransferCommand(Account origin, Account recipient, string description, DateTime? scheduledDate, double value)
        {
            Origin = origin;
            Recipient = recipient;
            Description = description;
            ScheduledDate = scheduledDate == null ? DateTime.Now : scheduledDate;
            Value = value;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewTransferCommandValidation().Validate(this);            
            return ValidationResult.IsValid;
        }
    }
}
