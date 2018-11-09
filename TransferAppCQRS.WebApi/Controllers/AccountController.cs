using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransferAppCQRS.Domain.Commands;
using TransferAppCQRS.Domain.Core.Bus;
using TransferAppCQRS.Domain.Core.Notifications;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.WebApi.ViewModels;

namespace TransferAppCQRS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IMediatorHandler Bus;        

        public AccountController(
            INotificationHandler<DomainNotification> notifications,
            IMapper mapper, 
            IAccountRepository accountRepository, 
            IMediatorHandler bus,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            Bus = bus;
        }

        [HttpPost]
        [Route("account")]
        public async Task<IActionResult> PostAsync([FromBody] AccountViewModel accountViewModel)
        {
            if(!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(accountViewModel);
            }

            var registerCommand = _mapper.Map<RegisterNewAccountCommand>(accountViewModel);

            await Bus.SendCommand(registerCommand);

            return Response(accountViewModel);
        }

        [HttpGet]
        [Route("account")]
        public async Task<IActionResult> GetAsync() => Response( await Task.Run(()=> _accountRepository.GetAll()));
        

    }
}