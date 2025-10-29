using System;
using System.Collections.Generic;

namespace KLTN2025.Models;

public partial class NguoiDung
{
    public int NguoiDungId { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MaKhauHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public bool GioiTinh { get; set; }

    public string Sdt { get; set; } = null!;

    public string VaiTro { get; set; } = null!;

    public DateTime TaoVaoLuc { get; set; }

    public virtual ICollection<GiaSu> GiaSus { get; set; } = new List<GiaSu>();

    public virtual ICollection<NhanSu> NhanSus { get; set; } = new List<NhanSu>();

    public virtual ICollection<PhanHoi> PhanHois { get; set; } = new List<PhanHoi>();

    public virtual ICollection<PhuHuynh> PhuHuynhs { get; set; } = new List<PhuHuynh>();

    public virtual ICollection<ThongBao> ThongBaos { get; set; } = new List<ThongBao>();
}
