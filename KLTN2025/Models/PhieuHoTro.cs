using System;

namespace KLTN2025.Models;

public partial class PhieuHoTro
{
    public int PhieuHoTroId { get; set; }
    public int PhanHoiId { get; set; }
    public int NhanSuId { get; set; }
    public DateTime? NgayGiaiQuyet { get; set; }
    public string? GhiChu { get; set; }

    public virtual PhanHoi PhanHoi { get; set; } = null!;
    public virtual NhanSu NhanSu { get; set; } = null!;
}
