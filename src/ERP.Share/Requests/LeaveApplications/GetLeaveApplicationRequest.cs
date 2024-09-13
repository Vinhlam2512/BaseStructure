namespace ERP.Share.Requests.LeaveApplications;

public sealed record GetWFHApplicationRequest(
    string? FromDate,
    string? ToDate);
