using System;
using System.Collections.Generic;

namespace KLTN2025.Models;

public partial class GiaSu
{
    public int NguoiDungId { get; set; }
    public string? BangCap { get; set; }
    public string? KinhNghiem { get; set; }
    public string? KyNang { get; set; }
    public string? LichRanh { get; set; }
    public string KhuVucDay { get; set; } = null!;
    public byte TrangThai { get; set; }
    public string? AnhDaiDien { get; set; }
    public DateTime NgaySinh { get; set; }
    public string? TungHocTai { get; set; }
    public string? DaiHoc { get; set; }
    public string? NganhHoc { get; set; }
    public int? NamBatDau { get; set; }
    public int? NamKetThuc { get; set; }
    public string? AnhSinhVien { get; set; }
    public string? BangTotNghiep { get; set; }

    public virtual NguoiDung NguoiDung { get; set; } = null!;

    // 🔁 Quan hệ
    public virtual ICollection<LopHoc> LopHocsGiaSu { get; set; } = new List<LopHoc>();
    public virtual ICollection<UngTuyen> UngTuyens { get; set; } = new List<UngTuyen>();
    public virtual ICollection<PhanCong> PhanCongs { get; set; } = new List<PhanCong>();
    public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
}
