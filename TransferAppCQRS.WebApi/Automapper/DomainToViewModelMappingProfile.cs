using AutoMapper;
using TransferAppCQRS.Domain.Models;
using TransferAppCQRS.WebApi.ViewModels;

namespace TransferAppCQRS.WebApi.Automapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<Account, AccountViewModel>();
        }
    }
}
