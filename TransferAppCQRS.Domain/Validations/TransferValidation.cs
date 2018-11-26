using FluentValidation;
using System;
using TransferAppCQRS.Domain.Commands;

namespace TransferAppCQRS.Domain.Validations
{
    public abstract class TransferValidation<T> : AbstractValidator<T> where T : TransferCommand
    {
        protected void ValidateValue()
        {
            RuleFor(c => c.Value)
                .NotEmpty()
                .GreaterThan(0).WithMessage("Value must be greater than 0");
        }

        protected void ValidateCustomers()
        {
            RuleFor(c => c.OriginId)
                .NotNull();

            RuleFor(c => c.RecipientId)
                .NotNull();            
        }

        protected void ValidateAccounts()
        {
            RuleFor(x => x.OriginAccountIsValid).Must(x => x)
                .WithMessage("Conta de origem não encontrada");

            RuleFor(x => x.RecipientnAccountIsValid).Must(x => x)
                .WithMessage("Conta destino não encontrada");
        }

        protected void ValidateScheduledDatetime()
        {
            RuleFor(c => c.ScheduledDate.Value.Date)
                .GreaterThanOrEqualTo(DateTime.Now.Date);
        }

        protected void AssertOriginAccountHasSuficientFunds()
        {
            RuleFor(c => c.OriginAccountHasSuficientFunds).Must(x => x)
                .WithMessage("Saldo insuficiente");            
        }
    }
}
