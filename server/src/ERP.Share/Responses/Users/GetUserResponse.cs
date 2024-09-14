namespace ERP.Share.Responses.Users;

public sealed record GetUserResponse(
        Guid Id,
        string UserName,
        string HoTen,
        string NgaySinh,
        string GioiTinh,
        string MaPhongBan,
        string? ChucVu,
        string? NgayThuViec,
        string? NgayLamChinhThuc,
        string? EmailCongTy,
        string? SoDienThoai,
        bool IsLeader,
        string? Leader);
