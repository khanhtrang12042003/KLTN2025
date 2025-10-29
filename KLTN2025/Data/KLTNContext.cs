using System;
using System.Collections.Generic;
using KLTN2025.Models;
using Microsoft.EntityFrameworkCore;

namespace KLTN2025.Data;

public partial class KLTNContext : DbContext
{
    public KLTNContext()
    {
    }

    public KLTNContext(DbContextOptions<KLTNContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GiaSu> GiaSus { get; set; }

    public virtual DbSet<HoSoGiaSu> HoSoGiaSus { get; set; }

    public virtual DbSet<LopHoc> LopHocs { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhanSu> NhanSus { get; set; }

    public virtual DbSet<PhanCong> PhanCongs { get; set; }

    public virtual DbSet<PhanHoi> PhanHois { get; set; }

    public virtual DbSet<PhieuHoTro> PhieuHoTros { get; set; }

    public virtual DbSet<PhuHuynh> PhuHuynhs { get; set; }

    public virtual DbSet<ThanhToan> ThanhToans { get; set; }

    public virtual DbSet<ThongBao> ThongBaos { get; set; }

    public virtual DbSet<UngTuyen> UngTuyens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GiaSu>(entity =>
        {
            entity.HasKey(e => e.GiaSuId).HasName("PK__GiaSu__BE5E202748DD713A");

            entity.ToTable("GiaSu");

            entity.Property(e => e.GiaSuId).HasColumnName("GiaSuID");
            entity.Property(e => e.AnhDaiDien)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValue("");
            entity.Property(e => e.AnhSinhVien)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValue("");
            entity.Property(e => e.BangCap).HasMaxLength(255);
            entity.Property(e => e.BangTotNghiep)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValue("");
            entity.Property(e => e.DaiHoc)
                .HasMaxLength(255)
                .HasDefaultValue("Chua lên d?i h?c");
            entity.Property(e => e.KhuVucDay).HasMaxLength(255);
            entity.Property(e => e.KinhNghiem).HasMaxLength(255);
            entity.Property(e => e.KyNang).HasMaxLength(255);
            entity.Property(e => e.LichRanh).HasMaxLength(500);
            entity.Property(e => e.NamBatDau).HasDefaultValue(0);
            entity.Property(e => e.NamKetThuc).HasDefaultValue(0);
            entity.Property(e => e.NganhHoc)
                .HasMaxLength(255)
                .HasDefaultValue("Chua lên d?i h?c");
            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");
            entity.Property(e => e.TungHocTai).HasMaxLength(255);

            entity.HasOne(d => d.NguoiDung).WithMany(p => p.GiaSus)
                .HasForeignKey(d => d.NguoiDungId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NguoiDungID_GiaSu");
        });

        modelBuilder.Entity<HoSoGiaSu>(entity =>
        {
            entity.HasKey(e => e.HoSoId).HasName("PK__HoSoGiaS__A9F1AA18CB5B53F2");

            entity.ToTable("HoSoGiaSu");

            entity.Property(e => e.HoSoId).HasColumnName("HoSoID");
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.GiaSuId).HasColumnName("GiaSuID");
            entity.Property(e => e.NgayDuyet).HasPrecision(0);
            entity.Property(e => e.NgayGui)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.NhanSuId).HasColumnName("NhanSuID");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(8)
                .HasDefaultValue("Đang chờ");
            entity.Property(e => e.TuChoiVi).HasMaxLength(255);

            entity.HasOne(d => d.GiaSu).WithMany(p => p.HoSoGiaSus)
                .HasForeignKey(d => d.GiaSuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GiaSuID_HoSoGiaSu");
        });

        modelBuilder.Entity<LopHoc>(entity =>
        {
            entity.HasKey(e => e.LopHocId).HasName("PK__LopHoc__DBC489C09B45C547");

            entity.ToTable("LopHoc");

            entity.Property(e => e.LopHocId).HasColumnName("LopHocID");
            entity.Property(e => e.DiaDiem).HasMaxLength(255);
            entity.Property(e => e.GiaSuId).HasColumnName("GiaSuID");
            entity.Property(e => e.HocPhi).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.LichHoc)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.MonHoc).HasMaxLength(255);
            entity.Property(e => e.NgayTao)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.NhanSuId).HasColumnName("NhanSuID");
            entity.Property(e => e.PhuHuynhId).HasColumnName("PhuHuynhID");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .HasDefaultValue("Mới");

            entity.HasOne(d => d.PhuHuynh).WithMany(p => p.LopHocs)
                .HasForeignKey(d => d.PhuHuynhId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhuHuynhID_LopHoc");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.NguoiDungId).HasName("PK__NguoiDun__C4BBA4DDE679DE91");

            entity.ToTable("NguoiDung");

            entity.HasIndex(e => e.TenDangNhap, "UQ__NguoiDun__55F68FC067D430D1").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D105346EFE860E").IsUnique();

            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MaKhauHash)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Sdt)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("SDT");
            entity.Property(e => e.TaoVaoLuc)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.VaiTro).HasMaxLength(50);
        });

        modelBuilder.Entity<NhanSu>(entity =>
        {
            entity.HasKey(e => e.NhanSuId).HasName("PK__NhanSu__F2329F1BB51D7490");

            entity.ToTable("NhanSu");

            entity.Property(e => e.NhanSuId).HasColumnName("NhanSuID");
            entity.Property(e => e.BoPhan).HasMaxLength(50);
            entity.Property(e => e.ChucVu).HasMaxLength(50);
            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

            entity.HasOne(d => d.NguoiDung).WithMany(p => p.NhanSus)
                .HasForeignKey(d => d.NguoiDungId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NguoiDungID_NhanSu");
        });

        modelBuilder.Entity<PhanCong>(entity =>
        {
            entity.HasKey(e => e.PhanCongId).HasName("PK__PhanCong__7EF840DDA0E8DEE5");

            entity.ToTable("PhanCong");

            entity.Property(e => e.PhanCongId).HasColumnName("PhanCongID");
            entity.Property(e => e.GiaSuId).HasColumnName("GiaSuID");
            entity.Property(e => e.LopHocId).HasColumnName("LopHocID");
            entity.Property(e => e.NgayPhanCong)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.NhanSuId).HasColumnName("NhanSuID");

            entity.HasOne(d => d.GiaSu).WithMany(p => p.PhanCongs)
                .HasForeignKey(d => d.GiaSuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GiaSuID_PhanCong");

            entity.HasOne(d => d.LopHoc).WithMany(p => p.PhanCongs)
                .HasForeignKey(d => d.LopHocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LopHocID_PhanCong");

            entity.HasOne(d => d.NhanSu).WithMany(p => p.PhanCongs)
                .HasForeignKey(d => d.NhanSuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NhanSuID_PhanCong");
        });

        modelBuilder.Entity<PhanHoi>(entity =>
        {
            entity.HasKey(e => e.PhanHoiId).HasName("PK__PhanHoi__7480288BB26D542F");

            entity.ToTable("PhanHoi");

            entity.Property(e => e.PhanHoiId).HasColumnName("PhanHoiID");
            entity.Property(e => e.LoaiPhanHoi).HasMaxLength(10);
            entity.Property(e => e.LopHocId).HasColumnName("LopHocID");
            entity.Property(e => e.NgayGui)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Đang chờ");

            entity.HasOne(d => d.LopHoc).WithMany(p => p.PhanHois)
                .HasForeignKey(d => d.LopHocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LopHocID_PhanHoi");

            entity.HasOne(d => d.NguoiDung).WithMany(p => p.PhanHois)
                .HasForeignKey(d => d.NguoiDungId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NguoiDungID_PhanHoi");
        });

        modelBuilder.Entity<PhieuHoTro>(entity =>
        {
            entity.HasKey(e => e.PhieuHoTroId).HasName("PK__PhieuHoT__6A7F0FDCD2C2ECC5");

            entity.ToTable("PhieuHoTro");

            entity.Property(e => e.PhieuHoTroId).HasColumnName("PhieuHoTroID");
            entity.Property(e => e.GhiChu).HasMaxLength(500);
            entity.Property(e => e.NhanSuId).HasColumnName("NhanSuID");
            entity.Property(e => e.PhanHoiId).HasColumnName("PhanHoiID");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Đang chờ");

            entity.HasOne(d => d.NhanSu).WithMany(p => p.PhieuHoTros)
                .HasForeignKey(d => d.NhanSuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NhanSuIID_PhieuHoTro");

            entity.HasOne(d => d.PhanHoi).WithMany(p => p.PhieuHoTros)
                .HasForeignKey(d => d.PhanHoiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhanHoiID_PhieuHoTro");
        });

        modelBuilder.Entity<PhuHuynh>(entity =>
        {
            entity.HasKey(e => e.PhuHuynhId).HasName("PK__PhuHuynh__D0ADD09079B8831A");

            entity.ToTable("PhuHuynh");

            entity.Property(e => e.PhuHuynhId).HasColumnName("PhuHuynhID");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

            entity.HasOne(d => d.NguoiDung).WithMany(p => p.PhuHuynhs)
                .HasForeignKey(d => d.NguoiDungId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NguoiDungID_PhuHuynh");
        });

        modelBuilder.Entity<ThanhToan>(entity =>
        {
            entity.HasKey(e => e.ThanhToanId).HasName("PK__ThanhToa__24A8D684973C0587");

            entity.ToTable("ThanhToan");

            entity.Property(e => e.ThanhToanId).HasColumnName("ThanhToanID");
            entity.Property(e => e.GiaSuId).HasColumnName("GiaSuID");
            entity.Property(e => e.LopHocId).HasColumnName("LopHocID");
            entity.Property(e => e.NgayGui)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.NgayThanhToan).HasPrecision(0);
            entity.Property(e => e.SoTien).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Đang chờ");

            entity.HasOne(d => d.GiaSu).WithMany(p => p.ThanhToans)
                .HasForeignKey(d => d.GiaSuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GiaSuID_ThanhToan");

            entity.HasOne(d => d.LopHoc).WithMany(p => p.ThanhToans)
                .HasForeignKey(d => d.LopHocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LopHocID_ThanhToan");
        });

        modelBuilder.Entity<ThongBao>(entity =>
        {
            entity.HasKey(e => e.ThongBaoId).HasName("PK__ThongBao__6E51A53B0FEA1A6B");

            entity.ToTable("ThongBao");

            entity.Property(e => e.ThongBaoId).HasColumnName("ThongBaoID");
            entity.Property(e => e.NgayGui)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");
            entity.Property(e => e.NoiDung).HasMaxLength(500);
            entity.Property(e => e.TieuDe).HasMaxLength(100);

            entity.HasOne(d => d.NguoiDung).WithMany(p => p.ThongBaos)
                .HasForeignKey(d => d.NguoiDungId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NguoiDungID_ThongBao");
        });

        modelBuilder.Entity<UngTuyen>(entity =>
        {
            entity.HasKey(e => e.UngTuyenId).HasName("PK__UngTuyen__4E510296DAF8E723");

            entity.ToTable("UngTuyen");

            entity.Property(e => e.UngTuyenId).HasColumnName("UngTuyenID");
            entity.Property(e => e.GiaSuId).HasColumnName("GiaSuID");
            entity.Property(e => e.LopHocId).HasColumnName("LopHocID");
            entity.Property(e => e.NgayUngTuyen)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Đang chờ");

            entity.HasOne(d => d.GiaSu).WithMany(p => p.UngTuyens)
                .HasForeignKey(d => d.GiaSuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GiaSuID_UngTuyen");

            entity.HasOne(d => d.LopHoc).WithMany(p => p.UngTuyens)
                .HasForeignKey(d => d.LopHocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LopHocID_UngTuyen");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
