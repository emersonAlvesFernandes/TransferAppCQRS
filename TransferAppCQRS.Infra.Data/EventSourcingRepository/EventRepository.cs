using TransferAppCQRS.Domain.Core.Events;

namespace TransferAppCQRS.Infra.Data.EventSourcingRepository
{
    public class EventRepository : IEventStore
    {
        public void Save<T>(T theEvent) where T : Event
        {
            
        }
    }
}
