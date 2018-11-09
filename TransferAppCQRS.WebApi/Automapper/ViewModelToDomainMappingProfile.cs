using AutoMapper;
using TransferAppCQRS.Domain.Commands;
using TransferAppCQRS.WebApi.ViewModels;

namespace TransferAppCQRS.WebApi.Automapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CustomerViewModel, RegisterNewCustomerCommand>()
                .ConstructUsing(c => new RegisterNewCustomerCommand(c.Name, c.Email, c.BirthDate));

            CreateMap<TransferViewModel, RegisterNewTransferCommand>()
                .ConstructUsing(c => new RegisterNewTransferCommand(c.OriginId, c.RecipientId, c.Description, c.ScheduledDate, c.Value));

            CreateMap<AccountViewModel, RegisterNewAccountCommand>()                
                .ConstructUsing(c => new RegisterNewAccountCommand(c.Agency, c.Number, c.Address, c.CustomerGuId));

            //CreateMap<CustomerViewModel, UpdateCustomerCommand>()
            //    .ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.Name, c.Email, c.BirthDate));
        }
    }
}
