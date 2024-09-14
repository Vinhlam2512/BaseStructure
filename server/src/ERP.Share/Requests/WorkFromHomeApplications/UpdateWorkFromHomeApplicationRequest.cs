namespace ERP.Share.Requests.WorkFromHomeApplications;
public sealed record class UpdateWorkFromHomeApplicationRequest(
        string Content,
        string? EquipmentBorrow,
        string StartDate,
    string EndDate);
