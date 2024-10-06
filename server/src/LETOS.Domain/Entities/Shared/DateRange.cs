namespace LETOS.Domain.Entities.Shared;
public sealed record DateRange
{
    private DateRange()
    {
    }

    public DateTime? StartDate { get; init; }

    public DateTime? EndDate { get; init; }

    public static DateRange Create(DateTime? startDate, DateTime? endDate)
    {
        if (startDate > endDate)
        {
            throw new ApplicationException("End date cần lớn hơn start date");
        }

        return new DateRange
        {
            StartDate = startDate,
            EndDate = endDate
        };
    }

}
