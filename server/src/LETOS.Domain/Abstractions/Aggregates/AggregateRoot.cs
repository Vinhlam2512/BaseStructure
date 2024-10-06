using LETOS.Share.Abstractions.Message;
using LETOS.Domain.Abstractions.Entities;

namespace LETOS.Domain.Abstractions.Aggregates;
public abstract class AggregateRoot<T> : EntityAuditBase<T>
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);
}


