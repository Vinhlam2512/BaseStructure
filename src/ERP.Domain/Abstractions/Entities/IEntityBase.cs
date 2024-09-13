namespace ERP.Domain.Abstractions.Entities;
public interface IEntityBase<T>
{
    public T Id { get; protected set; }
}
