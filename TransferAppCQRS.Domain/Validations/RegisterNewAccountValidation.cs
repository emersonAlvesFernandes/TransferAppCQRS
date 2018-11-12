using TransferAppCQRS.Domain.Commands;
using TransferAppCQRS.Domain.Interfaces;

namespace TransferAppCQRS.Domain.Validations
{
    public class RegisterNewAccountValidation : AccountValidation<RegisterNewAccountCommand>
    {
        public RegisterNewAccountValidation() 
        {
            ValidateAddress();
            ValidateAgency();
            ValidateNumber();
            ValidateDuplicity();
        }
    }
}
