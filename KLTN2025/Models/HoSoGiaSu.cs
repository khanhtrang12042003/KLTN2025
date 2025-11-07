using System;

namespace KLTN2025.Models;

public partial class HoSoGiaSu
{
    public int HoSoId { get; set; }
    public int NguoiDungId { get; set; }
    public DateTime NgayGui { get; set; }
    public DateTime? NgayDuyet { get; set; }
    public int? NhanSuId { get; set; }
    public string? LyDoTuChoi { get; set; }
    public string? GhiChu { get; set; }

    public virtual NguoiDung NguoiDung { get; set; } = null!;
    public virtual NhanSu? NhanSu { get; set; }
}
