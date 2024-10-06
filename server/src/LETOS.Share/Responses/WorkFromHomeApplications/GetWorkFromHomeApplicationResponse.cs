namespace LETOS.Share.Responses.WorkFromHomeApplications;

public sealed record GetWorkFromHomeApplicationResponse(
       Guid Id,
       string Code,
       string Content,
       string? EquipmentBorrow,
       DateTime StartDate,
       DateTime EndDate,
       Guid CreatedByUserId,
       string CreatedByUsername,
       string CreatedByName,
       string Status,
       Guid? ApprovedBy,
       DateTime? ApprovedAt,
       DateTime CreatedAt);
