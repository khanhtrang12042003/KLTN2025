using System;
using System.Collections.Generic;

namespace KLTN2025.Models;

public partial class HoSoGiaSu
{
    public int HoSoId { get; set; }

    public int GiaSuId { get; set; }

    public DateTime NgayGui { get; set; }

    public string TrangThai { get; set; } = null!;

    public DateTime? NgayDuyet { get; set; }

    public int? NhanSuId { get; set; }

    public string? TuChoiVi { get; set; }

    public string? GhiChu { get; set; }

    public virtual GiaSu GiaSu { get; set; } = null!;
}
