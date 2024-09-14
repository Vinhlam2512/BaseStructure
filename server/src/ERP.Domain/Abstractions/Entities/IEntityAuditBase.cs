namespace ERP.Domain.Abstractions.Entities;
public interface IEntityAuditBase<T> : IEntityBase<T>, IAuditTable
{
}
