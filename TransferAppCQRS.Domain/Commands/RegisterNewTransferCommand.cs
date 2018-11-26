using System;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Domain.Models;
using TransferAppCQRS.Domain.Validations;

namespace TransferAppCQRS.Domain.Commands
{
    public class RegisterNewTransferCommand : TransferCommand
    {        
        private IAccountRepository _sqlAccountRepository;
        public Account Origin { get; set; }
        public Account Recipient { get; set; }

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
            Validate_OriginAccountIsValid();
            Validate_RecipientAccountIsValid();
            Validate_OriginAccountHasSuficientFunds();
            
            ValidationResult = new RegisterNewTransferCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        private void Validate_OriginAccountHasSuficientFunds()
        {
            var actualBalance = _sqlAccountRepository.GetBalance(this.OriginId);
            
            OriginAccountHasSuficientFunds = actualBalance > this.Value ? true : false;
        }

        private void Validate_OriginAccountIsValid()
        {
            Origin = _sqlAccountRepository.GetById(OriginId);            
            OriginAccountIsValid = Origin == null ? false : true;
        }

        private void Validate_RecipientAccountIsValid()
        {
            Recipient = _sqlAccountRepository.GetById(RecipientId);
            RecipientnAccountIsValid = Recipient == null ? false : true;
        }

        public Transfer ToModel(IAccountRepository sqlAccountRepository)
        {
            if(IsValid(sqlAccountRepository))
                return new Transfer(Guid.NewGuid(), Origin, Recipient, Description, ScheduledDate, Value);            

            return null;
        }
    }
}
