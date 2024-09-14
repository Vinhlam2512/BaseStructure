namespace ERP.Share.Requests.LeaveApplications;

public sealed record UpdateLeaveApplicationRequest(
           string Content,
           int TypeApplication,
           string HandOverContent,
           Guid HandOverToUserId,
           List<UpdateLeaveApplicationDetailRequest>? Details);

public sealed record UpdateLeaveApplicationDetailRequest(
        Guid? Id,
        string LeaveAt,
        int TimeType
);
