using System;
using System.Collections.Generic;

namespace KLTN2025.Models;

public partial class ThanhToan
{
    public int ThanhToanId { get; set; }

    public int GiaSuId { get; set; }

    public int LopHocId { get; set; }

    public decimal SoTien { get; set; }

    public DateTime NgayGui { get; set; }

    public DateTime? NgayThanhToan { get; set; }

    public string TrangThai { get; set; } = null!;

    public virtual GiaSu GiaSu { get; set; } = null!;

    public virtual LopHoc LopHoc { get; set; } = null!;
}
