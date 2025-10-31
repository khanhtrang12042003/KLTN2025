using System;
using System.Collections.Generic;

namespace KLTN2025.Models;

public partial class GiaSu
{
    public int GiaSuId { get; set; }

    public int NguoiDungId { get; set; }

    public string? BangCap { get; set; }

    public string? KinhNghiem { get; set; }

    public string? KyNang { get; set; }

    public string? LichRanh { get; set; }

    public string KhuVucDay { get; set; } = null!;

    public string TrangThai { get; set; } = "Chưa duyệt";
    public DateTime? NgayCapNhat { get; set; }

    public string? AnhDaiDien { get; set; }

    public DateOnly NgaySinh { get; set; }

    public string? TungHocTai { get; set; }

    public string? DaiHoc { get; set; }

    public string? NganhHoc { get; set; }

    public int? NamBatDau { get; set; }

    public int? NamKetThuc { get; set; }

    public string? AnhSinhVien { get; set; }

    public string? BangTotNghiep { get; set; }
   


    public virtual ICollection<HoSoGiaSu> HoSoGiaSus { get; set; } = new List<HoSoGiaSu>();

    public virtual NguoiDung NguoiDung { get; set; } = null!;

    public virtual ICollection<PhanCong> PhanCongs { get; set; } = new List<PhanCong>();

    public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();

    public virtual ICollection<UngTuyen> UngTuyens { get; set; } = new List<UngTuyen>();
}
