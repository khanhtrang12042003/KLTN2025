using System;
using System.Collections.Generic;

namespace KLTN2025.Models;

public partial class LopHoc
{
    public int LopHocId { get; set; }

    public int PhuHuynhId { get; set; }

    public int GiaSuId { get; set; }

    public int NhanSuId { get; set; }

    public string? MonHoc { get; set; }

    public byte? KhoiLop { get; set; }

    public string? DiaDiem { get; set; }

    public byte? SoBuoiHoc { get; set; }

    public string? LichHoc { get; set; }

    public decimal? HocPhi { get; set; }

    public string TrangThai { get; set; } = null!;

    public DateTime NgayTao { get; set; }

    public virtual ICollection<PhanCong> PhanCongs { get; set; } = new List<PhanCong>();

    public virtual ICollection<PhanHoi> PhanHois { get; set; } = new List<PhanHoi>();

    public virtual PhuHuynh PhuHuynh { get; set; } = null!;

    public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();

    public virtual ICollection<UngTuyen> UngTuyens { get; set; } = new List<UngTuyen>();
}
