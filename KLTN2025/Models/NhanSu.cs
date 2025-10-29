using System;
using System.Collections.Generic;

namespace KLTN2025.Models;

public partial class NhanSu
{
    public int NhanSuId { get; set; }

    public int NguoiDungId { get; set; }

    public string BoPhan { get; set; } = null!;

    public string ChucVu { get; set; } = null!;

    public virtual NguoiDung NguoiDung { get; set; } = null!;

    public virtual ICollection<PhanCong> PhanCongs { get; set; } = new List<PhanCong>();

    public virtual ICollection<PhieuHoTro> PhieuHoTros { get; set; } = new List<PhieuHoTro>();
}
