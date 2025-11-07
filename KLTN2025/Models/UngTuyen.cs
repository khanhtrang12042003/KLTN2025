using System;

namespace KLTN2025.Models;

public partial class UngTuyen
{
    public int UngTuyenId { get; set; }
    public int GiaSuId { get; set; }
    public int LopHocId { get; set; }
    public int NhanSuId { get; set; }
    public DateTime NgayUngTuyen { get; set; }
    public string TrangThai { get; set; } = null!;
    public DateTime? NgayDuyet { get; set; }
    public string? LyDoTuChoi { get; set; }

    public virtual GiaSu GiaSu { get; set; } = null!;
    public virtual LopHoc LopHoc { get; set; } = null!;
    public virtual NhanSu NhanSu { get; set; } = null!;
}
