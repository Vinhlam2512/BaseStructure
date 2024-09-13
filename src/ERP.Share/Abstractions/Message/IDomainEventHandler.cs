using MediatR;

namespace ERP.Share.Abstractions.Message;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
