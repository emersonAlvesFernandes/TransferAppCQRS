using System.Threading.Tasks;
using TransferAppCQRS.Domain.Core.Commands;
using TransferAppCQRS.Domain.Core.Events;

namespace TransferAppCQRS.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;

        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
