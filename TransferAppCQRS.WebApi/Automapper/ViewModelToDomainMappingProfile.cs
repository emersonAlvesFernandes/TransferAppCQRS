using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                .ConstructUsing(c => new RegisterNewTransferCommand(c.Name, c.Email, c.BirthDate));

            //CreateMap<CustomerViewModel, UpdateCustomerCommand>()
            //    .ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.Name, c.Email, c.BirthDate));
        }
    }
}
