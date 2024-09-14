using MediatR;

namespace ERP.Share.Abstractions.Message;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}
