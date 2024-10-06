using LETOS.Domain.Entities.KhachHang;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LETOS.Persistence.Configurations;
internal sealed class KhachHangConfiguration : IEntityTypeConfiguration<KhachHang>
{
    public void Configure(EntityTypeBuilder<KhachHang> builder)
    {
        builder.ToTable("KhachHang");
        builder.HasKey(x => x.Id);

        builder.Property(kh => kh.MaKhachHang)
            .HasMaxLength(20)
            .IsRequired()
            .HasColumnType("varchar");


        builder.OwnsOne(kh => kh.ThongTinLienHe, tt =>
        {
            tt.Property(tt => tt.TenCongTy)
                .IsRequired()
                .HasColumnName("TenCongTy")
                .HasColumnType("nvarchar")
                .HasMaxLength(200);

            tt.Property(tt => tt.VanPhongGiaoDich)
                .IsRequired()
                .HasColumnName("VanPhongGiaoDich")
                .HasColumnType("nvarchar")
                .HasMaxLength(200);

            tt.Property(tt => tt.DiaChiXuatHoaDon)
                .IsRequired()
                .HasColumnName("DiaChiXuatHoaDon")
                .HasColumnType("nvarchar")
                .HasMaxLength(200);

            tt.Property(tt => tt.MaSoThue)
                .IsRequired()
                .HasColumnName("MaSoThue")
                .HasColumnType("varchar")
                .HasMaxLength(50);

            tt.Property(tt => tt.Website)
                .IsRequired()
                .HasColumnName("Website")
                .HasColumnType("varchar")
                .HasMaxLength(50);

        });

        builder.OwnsOne(kh => kh.DiaChi, dc =>
        {
            dc.Property(dc => dc.Duong)
                .IsRequired()
                .HasColumnName("Duong")
                .HasColumnType("nvarchar")
                .HasMaxLength(100);

            dc.Property(dc => dc.Phuong)
                .IsRequired()
                .HasColumnName("Phuong")
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            dc.Property(dc => dc.ThanhPho)
                .IsRequired()
                .HasColumnName("ThanhPho")
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            dc.Property(dc => dc.Tinh)
                .IsRequired()
                .HasColumnName("Tinh")
                .HasColumnType("nvarchar")
                .HasMaxLength(50);
        });

        builder.HasIndex(kh => kh.MaKhachHang).IsUnique();

    }
}
