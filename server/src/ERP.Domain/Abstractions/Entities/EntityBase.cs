using System.ComponentModel.DataAnnotations;

namespace ERP.Domain.Abstractions.Entities;

public abstract class EntityBase<T> : IEntityBase<T>
{
    [Key]
    public T Id { get; set; }
}
