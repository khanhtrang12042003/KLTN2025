using System;
using System.Collections.Generic;

namespace KLTN2025.Models;

public partial class PhuHuynh
{
    public int PhuHuynhId { get; set; }

    public int NguoiDungId { get; set; }

    public string DiaChi { get; set; } = null!;

    public virtual ICollection<LopHoc> LopHocs { get; set; } = new List<LopHoc>();

    public virtual NguoiDung NguoiDung { get; set; } = null!;
}
