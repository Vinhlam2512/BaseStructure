namespace LETOS.Share.Requests.Users;
public sealed record CreateUserRequest(
    string Username,
    string Password,
    string HoTen,
    DateTime NgaySinh,
    string GioiTinh,
    string MaPhongBan,
    string? ChucVu,
    DateOnly? NgayThuViec,
    DateOnly? NgayLamChinhThuc,
    string NoiLamViec,
    int? MaChamCong,
    string? Email,
    string? EmailCongTy,
    string? PasswordEmailCongTy,
    string? Avatar,
    bool IsAdmin,
    string MaXacNhan,
    bool IsLeader,
    Guid? LeaderId,
    string TokenSmart,
    string MaKhachHangGiga);

