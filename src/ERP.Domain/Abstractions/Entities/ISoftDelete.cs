namespace ERP.Domain.Abstractions.Entities;
public interface ISoftDelete
{
    public bool IsDelete { get; set; }
    public DateTime? DeletedAt { get; set; }

    public void Remove();
}
