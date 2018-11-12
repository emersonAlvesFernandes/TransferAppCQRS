using FluentValidation;
using TransferAppCQRS.Domain.Commands;

namespace TransferAppCQRS.Domain.Validations
{
    public abstract class AccountValidation<T> : AbstractValidator<T> where T : AccountCommand
    {                
        protected void ValidateAgency()
        {
            RuleFor(c => c.Agency).GreaterThan(0);
        }

        protected void ValidateNumber()
        {
            RuleFor(c => c.Number).NotNull();
        }

        protected void ValidateAddress()
        {
            RuleFor(c => c.Agency).NotEmpty();
        }

        protected void ValidateDuplicity()
        {
            RuleFor(c => c.ExistsInDatabase).Must(x => !x)
                .WithMessage("Agência já cadastrada");
        }
    }
}
