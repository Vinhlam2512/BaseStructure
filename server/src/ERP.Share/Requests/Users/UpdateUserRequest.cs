
namespace ERP.Share.Requests.Users;
public sealed record UpdateUserRequest(
        string? HoTen,
        string? NgaySinh,
        int GioiTinh,
        string? SoDienThoai,
        string? MaPhongBan,
        string? ChucVu,
        string? NgayThuViec,
        string? NgayLamChinhThuc,
        int? MaChamCong,
        string? Email,
        string? EmailCongTy,
        string? Avatar,
        bool? IsAdmin,
        string? MaXacNhan,
        bool? IsLeader,
        Guid? LeaderId,
        string? MaKhachHangGiga
 );
