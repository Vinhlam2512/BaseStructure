using System.ComponentModel.DataAnnotations;

namespace LETOS.Domain.Abstractions.Entities;

public abstract class EntityBase<T> : IEntityBase<T>
{
    [Key]
    public T Id { get; set; }
}
