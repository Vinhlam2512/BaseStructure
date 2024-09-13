namespace ERP.Domain.Abstractions.Entities;
public interface IDateTracking
{
    DateTime CreatedAt { get; set; }

    DateTime? ModifiedAt { get; set; }
}
