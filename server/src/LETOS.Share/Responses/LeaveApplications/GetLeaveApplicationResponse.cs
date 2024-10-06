using System.Text.Json.Serialization;

namespace LETOS.Share.Responses.LeaveApplications;

public class GetLeaveApplicationResponse
{
    public Guid Id { get; set; }

    public string Code { get; set; }

    public string Content { get; set; }

    public string HandOverContent { get; set; }

    public Guid HandOverToUserId { get; set; }

    public Guid? ApprovedBy { get; set; }

    public string? ApprovedByName { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public string TypeApplication { get; set; }

    public string Status { get; set; }

    public Guid CreatedByUserId { get; set; }

    public string CreatedByUsername { get; set; }

    public string CreatedByName { get; set; }

    public DateTime CreatedAt { get; set; }

    public int TongSoNgayNghi { get; set; }

    [JsonIgnore]
    public string DetailJson { get; set; }

    public List<GetLeaveApplicationDetailResponse>? Detail { get; set; }

    public GetLeaveApplicationResponse()
    {
        Detail = new List<GetLeaveApplicationDetailResponse>();
    }
}

public class GetLeaveApplicationDetailResponse
{
    public Guid Id { get; set; }

    public DateTime LeaveAt { get; set; }

    public string TimeType { get; set; }
}
