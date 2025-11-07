using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KLTN2025.Models;

public partial class KLTNContext : DbContext
{
    public KLTNContext()
    {
    }

    public KLTNContext(DbContextOptions<KLTNContext> options)
        : base(options)
    {
    }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
    public virtual DbSet<GiaSu> GiaSus { get; set; }
    public virtual DbSet<NhanSu> NhanSus { get; set; }
    public virtual DbSet<HoSoGiaSu> HoSoGiaSus { get; set; }
    public virtual DbSet<LopHoc> LopHocs { get; set; }
    public virtual DbSet<UngTuyen> UngTuyens { get; set; }
    public virtual DbSet<PhanCong> PhanCongs { get; set; }
    public virtual DbSet<PhanHoi> PhanHois { get; set; }
    public virtual DbSet<ThongBao> ThongBaos { get; set; }
    public virtual DbSet<ThanhToan> ThanhToans { get; set; }
    public virtual DbSet<PhieuHoTro> PhieuHoTros { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // =======================
        // NguoiDung
        // =======================
        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.ToTable("NguoiDung");
            entity.HasKey(e => e.NguoiDungId);
            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

            entity.Property(e => e.TenDangNhap).HasMaxLength(80).IsRequired();
            entity.Property(e => e.MaKhauHash).HasMaxLength(256).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(255).IsRequired();
            entity.Property(e => e.HoTen).HasMaxLength(100).IsRequired();
            entity.Property(e => e.GioiTinh).IsRequired();
            entity.Property(e => e.Sdt).HasColumnName("SDT").HasMaxLength(11);
            entity.Property(e => e.VaiTro).HasMaxLength(50).IsRequired();
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.TaoVaoLuc)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(SYSDATETIME())");
        });

        // =======================
        // GiaSu
        // =======================
        modelBuilder.Entity<GiaSu>(entity =>
        {
            entity.ToTable("GiaSu");
            entity.HasKey(e => e.NguoiDungId);
            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");

            entity.Property(e => e.BangCap).HasMaxLength(255);
            entity.Property(e => e.KinhNghiem).HasMaxLength(255);
            entity.Property(e => e.KyNang).HasMaxLength(255);
            entity.Property(e => e.LichRanh).HasMaxLength(500);
            entity.Property(e => e.KhuVucDay).HasMaxLength(255).IsRequired();
            entity.Property(e => e.TrangThai).HasDefaultValue((byte)0);
            entity.Property(e => e.AnhDaiDien).HasMaxLength(255).HasDefaultValue("");
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.TungHocTai).HasMaxLength(255);
            entity.Property(e => e.DaiHoc).HasMaxLength(255).HasDefaultValue("Chưa lên đại học");
            entity.Property(e => e.NganhHoc).HasMaxLength(255).HasDefaultValue("Chưa lên đại học");
            entity.Property(e => e.NamBatDau).HasDefaultValue(0);
            entity.Property(e => e.NamKetThuc).HasDefaultValue(0);
            entity.Property(e => e.AnhSinhVien).HasMaxLength(255).HasDefaultValue("");
            entity.Property(e => e.BangTotNghiep).HasMaxLength(255).HasDefaultValue("");

            entity.HasOne(d => d.NguoiDung)
                .WithMany(p => p.GiaSus)
                .HasForeignKey(d => d.NguoiDungId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_NguoiDungID_GiaSu");
        });

        // =======================
        // NhanSu
        // =======================
        modelBuilder.Entity<NhanSu>(entity =>
        {
            entity.ToTable("NhanSu");
            entity.HasKey(e => e.NguoiDungId);
            entity.Property(e => e.NguoiDungId).HasColumnName("NguoiDungID");
            entity.Property(e => e.VaiTro).HasMaxLength(50).IsRequired();

            entity.HasOne(d => d.NguoiDung)
                .WithMany(p => p.NhanSus)
                .HasForeignKey(d => d.NguoiDungId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_NguoiDungID_NhanSu");
        });

        // =======================
        // HoSoGiaSu
        // =======================
        modelBuilder.Entity<HoSoGiaSu>(entity =>
        {
            entity.ToTable("HoSoGiaSu");
            entity.HasKey(e => e.HoSoId);
            entity.Property(e => e.HoSoId).HasColumnName("HoSoID");

            entity.Property(e => e.NgayGui)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("(SYSDATETIME())");

            entity.Property(e => e.NgayDuyet).HasColumnType("datetime2(0)");
            entity.Property(e => e.LyDoTuChoi).HasMaxLength(100);
            entity.Property(e => e.GhiChu).HasMaxLength(500);

            entity.HasOne(d => d.NguoiDung)
                .WithMany(p => p.HoSoGiaSus)
                .HasForeignKey(d => d.NguoiDungId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NguoiDungID_HoSoGiaSu");

            entity.HasOne(d => d.NhanSu)
                .WithMany(p => p.HoSoGiaSus)
                .HasForeignKey(d => d.NhanSuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NhanSuID_HoSoGiaSu");
        });

        // =======================
        // LopHoc
        // =======================
        modelBuilder.Entity<LopHoc>(entity =>
        {
            entity.ToTable("LopHoc");
            entity.HasKey(e => e.LopHocId);
            entity.Property(e => e.LopHocId).HasColumnName("LopHocID");

            entity.Property(e => e.MonHoc).HasMaxLength(255);
            entity.Property(e => e.KhoiLop);
            entity.Property(e => e.DiaDiem).HasMaxLength(255);
            entity.Property(e => e.LichHoc).HasMaxLength(500);
            entity.Property(e => e.HocPhi).HasColumnType("decimal(10,2)");
            entity.Property(e => e.TrangThai).HasMaxLength(20).HasDefaultValue("Mới");
            entity.Property(e => e.NgayTao).HasColumnType("datetime2(0)").HasDefaultValueSql("(SYSDATETIME())");

            entity.HasOne(d => d.PhuHuynh)
                .WithMany(p => p.LopHocsPhuHuynh)
                .HasForeignKey(d => d.PhuHuynhId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhuHuynhID_LopHoc");

            entity.HasOne(d => d.GiaSu)
                .WithMany(p => p.LopHocsGiaSu)
                .HasForeignKey(d => d.GiaSuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GiaSuID_LopHoc");

            entity.HasOne(d => d.NhanSu)
                .WithMany(p => p.LopHocsNhanSu)
                .HasForeignKey(d => d.NhanSuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NhanSuID_LopHoc");
        });

        // =======================
        // UngTuyen
        // =======================
        modelBuilder.Entity<UngTuyen>(entity =>
        {
            entity.ToTable("UngTuyen");
            entity.HasKey(e => e.UngTuyenId);
            entity.Property(e => e.UngTuyenId).HasColumnName("UngTuyenID");

            entity.Property(e => e.TrangThai).HasMaxLength(50).HasDefaultValue("Đang chờ");
            entity.Property(e => e.NgayUngTuyen).HasColumnType("datetime2(0)").HasDefaultValueSql("(SYSDATETIME())");
            entity.Property(e => e.NgayDuyet).HasColumnType("datetime2(0)");
            entity.Property(e => e.LyDoTuChoi).HasMaxLength(500);

            entity.HasOne(d => d.GiaSu)
                .WithMany(p => p.UngTuyens)
                .HasForeignKey(d => d.GiaSuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GiaSuID_UngTuyen");

            entity.HasOne(d => d.LopHoc)
                .WithMany(p => p.UngTuyens)
                .HasForeignKey(d => d.LopHocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LopHocID_UngTuyen");

            entity.HasOne(d => d.NhanSu)
                .WithMany(p => p.UngTuyens)
                .HasForeignKey(d => d.NhanSuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NhanSuID_UngTuyen");
        });

        // =======================
        // PhanCong
        // =======================
        modelBuilder.Entity<PhanCong>(entity =>
        {
            entity.ToTable("PhanCong");
            entity.HasKey(e => e.PhanCongId);
            entity.Property(e => e.PhanCongId).HasColumnName("PhanCongID");
            entity.Property(e => e.NgayPhanCong).HasColumnType("datetime2(0)").HasDefaultValueSql("(SYSDATETIME())");

            entity.HasOne(d => d.GiaSu)
                .WithMany(p => p.PhanCongs)
                .HasForeignKey(d => d.GiaSuId)
                .HasConstraintName("FK_GiaSuID_PhanCong");

            entity.HasOne(d => d.LopHoc)
                .WithMany(p => p.PhanCongs)
                .HasForeignKey(d => d.LopHocId)
                .HasConstraintName("FK_LopHocID_PhanCong");

            entity.HasOne(d => d.NhanSu)
                .WithMany(p => p.PhanCongs)
                .HasForeignKey(d => d.NhanSuId)
                .HasConstraintName("FK_NhanSuID_PhanCong");
        });

        // =======================
        // PhanHoi
        // =======================
        modelBuilder.Entity<PhanHoi>(entity =>
        {
            entity.ToTable("PhanHoi");
            entity.HasKey(e => e.PhanHoiId);
            entity.Property(e => e.PhanHoiId).HasColumnName("PhanHoiID");
            entity.Property(e => e.LoaiPhanHoi).HasMaxLength(10).IsRequired();
            entity.Property(e => e.NoiDung).IsRequired();
            entity.Property(e => e.DiemDanhGia);
            entity.Property(e => e.NgayGui).HasColumnType("datetime2(0)").HasDefaultValueSql("(SYSDATETIME())");

            entity.HasOne(d => d.LopHoc)
                .WithMany(p => p.PhanHois)
                .HasForeignKey(d => d.LopHocId)
                .HasConstraintName("FK_LopHocID_PhanHoi");

            entity.HasOne(d => d.NguoiDung)
                .WithMany(p => p.PhanHois)
                .HasForeignKey(d => d.NguoiDungId)
                .HasConstraintName("FK_NguoiDungID_PhanHoi");
        });

        // =======================
        // ThongBao
        // =======================
        modelBuilder.Entity<ThongBao>(entity =>
        {
            entity.ToTable("ThongBao");
            entity.HasKey(e => e.ThongBaoId);
            entity.Property(e => e.ThongBaoId).HasColumnName("ThongBaoID");

            entity.Property(e => e.TieuDe).HasMaxLength(100).IsRequired();
            entity.Property(e => e.NoiDung).HasMaxLength(500).IsRequired();
            entity.Property(e => e.TrangThai).HasDefaultValue(false);
            entity.Property(e => e.NgayGui).HasColumnType("datetime2(0)").HasDefaultValueSql("(SYSDATETIME())");

            entity.HasOne(d => d.NguoiDung)
                .WithMany(p => p.ThongBaos)
                .HasForeignKey(d => d.NguoiDungId)
                .HasConstraintName("FK_NguoiDungID_ThongBao");
        });

        // =======================
        // ThanhToan
        // =======================
        modelBuilder.Entity<ThanhToan>(entity =>
        {
            entity.ToTable("ThanhToan");
            entity.HasKey(e => e.ThanhToanId);
            entity.Property(e => e.ThanhToanId).HasColumnName("ThanhToanID");
            entity.Property(e => e.SoTien).HasColumnType("decimal(10,2)").IsRequired();
            entity.Property(e => e.TrangThai).HasMaxLength(50).HasDefaultValue("Đang chờ");
            entity.Property(e => e.NgayGui).HasColumnType("datetime2(0)").HasDefaultValueSql("(SYSDATETIME())");
            entity.Property(e => e.NgayThanhToan).HasColumnType("datetime2(0)");

            entity.HasOne(d => d.GiaSu)
                .WithMany(p => p.ThanhToans)
                .HasForeignKey(d => d.GiaSuId)
                .HasConstraintName("FK_GiaSuID_ThanhToan");

            entity.HasOne(d => d.LopHoc)
                .WithMany(p => p.ThanhToans)
                .HasForeignKey(d => d.LopHocId)
                .HasConstraintName("FK_LopHocID_ThanhToan");
        });

        // =======================
        // PhieuHoTro
        // =======================
        modelBuilder.Entity<PhieuHoTro>(entity =>
        {
            entity.ToTable("PhieuHoTro");
            entity.HasKey(e => e.PhieuHoTroId);
            entity.Property(e => e.PhieuHoTroId).HasColumnName("PhieuHoTroID");
            entity.Property(e => e.NgayGiaiQuyet).HasColumnType("datetime2(0)");
            entity.Property(e => e.GhiChu).HasMaxLength(500);

            entity.HasOne(d => d.PhanHoi)
                .WithMany(p => p.PhieuHoTros)
                .HasForeignKey(d => d.PhanHoiId)
                .HasConstraintName("FK_PhanHoiID_PhieuHoTro");

            entity.HasOne(d => d.NhanSu)
                .WithMany(p => p.PhieuHoTros)
                .HasForeignKey(d => d.NhanSuId)
                .HasConstraintName("FK_NhanSuIID_PhieuHoTro");
        });
    }
}
