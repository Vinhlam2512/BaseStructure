namespace ERP.Domain.Abstractions.Entities;
public abstract class EntityAuditBase<T> : EntityBase<T>, IEntityAuditBase<T>
{
    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? ModifiedBy { get; set; }
}

public abstract class EntityAuditBase : IAuditTable
{
    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? ModifiedBy { get; set; }
}


