namespace ERP.Domain.Entities.KhachHang;
public sealed record ThongTinLienHe
{

    public string TenCongTy { get; init; }
    public string VanPhongGiaoDich { get; init; }
    public string DiaChiXuatHoaDon { get; init; }

    public string MaSoThue { get; init; }
    public string Website { get; init; }


    public static ThongTinLienHe Create(string tenCongTy, string vanPhongGiaoDich, string diaChiXuatHoaDon, string maSoThue, string website)
    {

        return new ThongTinLienHe
        {
            TenCongTy = tenCongTy,
            VanPhongGiaoDich = vanPhongGiaoDich,
            DiaChiXuatHoaDon = diaChiXuatHoaDon,
            MaSoThue = maSoThue,
            Website = website
        };
    }

}
