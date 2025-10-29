using System;
using System.Collections.Generic;

namespace KLTN2025.Models;

public partial class PhieuHoTro
{
    public int PhieuHoTroId { get; set; }

    public int PhanHoiId { get; set; }

    public int NhanSuId { get; set; }

    public string TrangThai { get; set; } = null!;

    public string? GhiChu { get; set; }

    public virtual NhanSu NhanSu { get; set; } = null!;

    public virtual PhanHoi PhanHoi { get; set; } = null!;
}
