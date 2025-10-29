using System;
using System.Collections.Generic;

namespace KLTN2025.Models;

public partial class PhanCong
{
    public int PhanCongId { get; set; }

    public int LopHocId { get; set; }

    public int GiaSuId { get; set; }

    public int NhanSuId { get; set; }

    public DateTime NgayPhanCong { get; set; }

    public virtual GiaSu GiaSu { get; set; } = null!;

    public virtual LopHoc LopHoc { get; set; } = null!;

    public virtual NhanSu NhanSu { get; set; } = null!;
}
