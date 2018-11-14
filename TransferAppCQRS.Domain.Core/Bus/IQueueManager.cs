using System.Threading.Tasks;
using TransferAppCQRS.Domain.Core.Commands;

namespace TransferAppCQRS.Domain.Core.Bus
{
    public interface IQueueManager
    {
        void Publish<T>(T command, string routingKey) where T : class;
    }
}
