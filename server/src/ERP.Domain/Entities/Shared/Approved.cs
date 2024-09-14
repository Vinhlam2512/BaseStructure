namespace ERP.Domain.Entities.Shared;
public sealed record Approved
{
    private Approved()
    {
    }

    public Guid? ApprovedBy { get; init; }

    public DateTime? ApprovedAt { get; init; }


    public static Approved Create(Guid? approvedBy, DateTime? approvedAt)
    {
        return new Approved
        {
            ApprovedBy = approvedBy,
            ApprovedAt = approvedAt
        };
    }
}
