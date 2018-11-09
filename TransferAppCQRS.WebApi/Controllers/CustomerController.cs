using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransferAppCQRS.Domain.Commands;
using TransferAppCQRS.Domain.Core.Bus;
using TransferAppCQRS.Domain.Core.Events;
using TransferAppCQRS.Domain.Core.Notifications;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.WebApi.ViewModels;

namespace TransferAppCQRS.WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEventStore _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public CustomerController(
            INotificationHandler<DomainNotification> notifications,
            IMapper mapper,
            ICustomerRepository customerRepository,
            IEventStore eventStoreRepository,
            IMediatorHandler bus,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _eventStoreRepository = eventStoreRepository;
            Bus = bus;
        }

        [HttpPost]
        //[Authorize(Policy = "CanWriteCustomerData")]
        [Route("customer")]
        public IActionResult Post([FromBody]CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(customerViewModel);
            }


            var registerCommand = _mapper.Map<RegisterNewCustomerCommand>(customerViewModel);            

            Bus.SendCommand(registerCommand);

            //_customerAppService.Register(customerViewModel);

            return Response(customerViewModel);
        }
    }
}
