﻿// <auto-generated />
using System;
using ERP.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ERP.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ERP.Domain.Entities.Identity.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Read"
                        });
                });

            modelBuilder.Entity("ERP.Domain.Entities.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "User"
                        });
                });

            modelBuilder.Entity("ERP.Domain.Entities.KhachHang.KhachHang", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("MaKhachHang")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MaKhachHang")
                        .IsUnique();

                    b.ToTable("KhachHang", (string)null);
                });

            modelBuilder.Entity("ERP.Domain.Entities.Users.RolePermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermissions", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            PermissionId = 1,
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("ERP.Domain.Entities.Users.RoleUser", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("RoleUsers", (string)null);
                });

            modelBuilder.Entity("ERP.Domain.Entities.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailed")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ModifiedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PassWordHashed")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("ERP.Persistence.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("OccurredOnUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ProcessedOnUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages", (string)null);
                });

            modelBuilder.Entity("ERP.Domain.Entities.KhachHang.KhachHang", b =>
                {
                    b.OwnsOne("ERP.Domain.Entities.KhachHang.ThongTinLienHe", "ThongTinLienHe", b1 =>
                        {
                            b1.Property<Guid>("KhachHangId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("DiaChiXuatHoaDon")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar")
                                .HasColumnName("DiaChiXuatHoaDon");

                            b1.Property<string>("MaSoThue")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar")
                                .HasColumnName("MaSoThue");

                            b1.Property<string>("TenCongTy")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar")
                                .HasColumnName("TenCongTy");

                            b1.Property<string>("VanPhongGiaoDich")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar")
                                .HasColumnName("VanPhongGiaoDich");

                            b1.Property<string>("Website")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar")
                                .HasColumnName("Website");

                            b1.HasKey("KhachHangId");

                            b1.ToTable("KhachHang");

                            b1.WithOwner()
                                .HasForeignKey("KhachHangId");
                        });

                    b.OwnsOne("ERP.Domain.Entities.Shared.DiaChi", "DiaChi", b1 =>
                        {
                            b1.Property<Guid>("KhachHangId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Duong")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar")
                                .HasColumnName("Duong");

                            b1.Property<string>("Phuong")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar")
                                .HasColumnName("Phuong");

                            b1.Property<string>("ThanhPho")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar")
                                .HasColumnName("ThanhPho");

                            b1.Property<string>("Tinh")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar")
                                .HasColumnName("Tinh");

                            b1.HasKey("KhachHangId");

                            b1.ToTable("KhachHang");

                            b1.WithOwner()
                                .HasForeignKey("KhachHangId");
                        });

                    b.Navigation("DiaChi")
                        .IsRequired();

                    b.Navigation("ThongTinLienHe")
                        .IsRequired();
                });

            modelBuilder.Entity("ERP.Domain.Entities.Users.RolePermission", b =>
                {
                    b.HasOne("ERP.Domain.Entities.Identity.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ERP.Domain.Entities.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ERP.Domain.Entities.Users.RoleUser", b =>
                {
                    b.HasOne("ERP.Domain.Entities.Identity.Role", "Role")
                        .WithMany("RoleUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ERP.Domain.Entities.Users.User", "User")
                        .WithMany("RoleUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ERP.Domain.Entities.Users.User", b =>
                {
                    b.OwnsOne("ERP.Domain.Entities.Users.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar")
                                .HasColumnName("LastName");

                            b1.Property<string>("UserName")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("varchar")
                                .HasColumnName("UserName");

                            b1.HasKey("UserId");

                            b1.HasIndex("UserName")
                                .IsUnique();

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("ERP.Domain.Entities.Identity.Role", b =>
                {
                    b.Navigation("RoleUsers");
                });

            modelBuilder.Entity("ERP.Domain.Entities.Users.User", b =>
                {
                    b.Navigation("RoleUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
