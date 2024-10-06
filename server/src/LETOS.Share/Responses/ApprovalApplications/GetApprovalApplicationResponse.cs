namespace LETOS.Share.Responses.ApprovalApplications;
public record GetApprovalApplicationResponse(
       Guid Id,
       string Code,
       string Content,
       DateTime DateNeedConfirm,
       string Type,
       string Status,
       Guid CreatedByUserId,
       string CreatedByUsername,
       DateTime? StartDate,
       DateTime? EndDate,
       DateTime CreatedAt);
