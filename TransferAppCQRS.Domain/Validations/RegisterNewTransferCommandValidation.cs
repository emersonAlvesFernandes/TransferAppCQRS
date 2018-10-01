using TransferAppCQRS.Domain.Commands;

namespace TransferAppCQRS.Domain.Validations
{
    public class RegisterNewTransferCommandValidation : TransferValidation<RegisterNewTransferCommand>
    {
        public RegisterNewTransferCommandValidation()
        {
            ValidateCustomers();
            ValidateValue();
            ValidateScheduledDatetime();
        }
    }
}
