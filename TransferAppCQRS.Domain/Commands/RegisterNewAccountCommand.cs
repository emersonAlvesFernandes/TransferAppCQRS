using System;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Domain.Validations;

namespace TransferAppCQRS.Domain.Commands
{
    public class RegisterNewAccountCommand : AccountCommand
    {
        private IAccountRepository _accountRepository;        

        public RegisterNewAccountCommand(int agency, int number, string address, Guid customerGuid)
        {
            Agency = agency;
            Number = number;
            Address = address;
            CustomerGuid = customerGuid;            
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewAccountValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public bool IsValid(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

            ExistsInDatabase = HasAlreadyRegistered();

            return IsValid();
        }

        private bool HasAlreadyRegistered()
        {
            var account = _accountRepository.Get(Agency, Number);

            return account == null ? false : true;
        }
    }
}
