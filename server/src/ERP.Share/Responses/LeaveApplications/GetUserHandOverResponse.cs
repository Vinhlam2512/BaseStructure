namespace ERP.Share.Responses.LeaveApplications;
public sealed record GetUserHandOverResponse(
        Guid Id,
        string HoTen,
        string UserName
    );
