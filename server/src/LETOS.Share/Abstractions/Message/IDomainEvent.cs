using MediatR;

namespace LETOS.Share.Abstractions.Message;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}
