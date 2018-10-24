using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransferAppCQRS.Domain.Commands;
using TransferAppCQRS.Domain.Core.Bus;
using TransferAppCQRS.Domain.Core.Notifications;
using TransferAppCQRS.WebApi.ViewModels;

namespace TransferAppCQRS.WebApi.Controllers
{
    public class TransferController : ApiController
    {
        private readonly IMediatorHandler _bus;
        private readonly IMapper _mapper;


        public TransferController(IMediatorHandler bus, 
            IMapper mapper, 
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _bus = bus;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]TransferViewModel createModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(createModel);
            }

            var command = _mapper.Map<RegisterNewTransferCommand>(createModel);

            await Task.Run(() => _bus.SendCommand(command));

            return Response(createModel);
        }
    }
}