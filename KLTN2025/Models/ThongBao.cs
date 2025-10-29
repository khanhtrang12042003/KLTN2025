using System;
using System.Collections.Generic;

namespace KLTN2025.Models;

public partial class ThongBao
{
    public int ThongBaoId { get; set; }

    public int NguoiDungId { get; set; }

    public string TieuDe { get; set; } = null!;

    public string NoiDung { get; set; } = null!;

    public DateTime NgayGui { get; set; }

    public virtual NguoiDung NguoiDung { get; set; } = null!;
}
