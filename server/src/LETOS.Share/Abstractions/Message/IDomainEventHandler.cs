using MediatR;

namespace LETOS.Share.Abstractions.Message;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
