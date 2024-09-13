﻿using ERP.Domain.Abstractions.Aggregates;
using ERP.Domain.Abstractions.Entities;
using ERP.Domain.Entities.Shared;

namespace ERP.Domain.Entities.KhachHang;
public sealed class KhachHang : AggregateRoot<Guid>, ISoftDelete
{
    private KhachHang()
    {

    }

    public KhachHang(string maKhachHang, ThongTinLienHe thongTinLienHe, DiaChi diaChi)
    {
        Id = Guid.NewGuid();
        MaKhachHang = maKhachHang;
        ThongTinLienHe = thongTinLienHe;
        DiaChi = diaChi;
    }

    public string MaKhachHang { get; init; }

    public ThongTinLienHe ThongTinLienHe { get; private set; }

    public DiaChi DiaChi { get; private set; }

    public bool IsDelete { get; set; }

    public DateTime? DeletedAt { get; set; }

    public static KhachHang Create(string maKhachHang, ThongTinLienHe thongTinLienHe, DiaChi diaChi)
    {
        var kh = new KhachHang(maKhachHang, thongTinLienHe, diaChi);

        return kh;
    }

    public void Remove()
    {
        throw new NotImplementedException();
    }
}
