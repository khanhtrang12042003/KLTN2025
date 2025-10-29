using System;
using System.Collections.Generic;

namespace KLTN2025.Models;

public partial class PhanHoi
{
    public int PhanHoiId { get; set; }

    public int LopHocId { get; set; }

    public int NguoiDungId { get; set; }

    public string LoaiPhanHoi { get; set; } = null!;

    public string NoiDung { get; set; } = null!;

    public byte DiemDanhGia { get; set; }

    public DateTime NgayGui { get; set; }

    public string TrangThai { get; set; } = null!;

    public virtual LopHoc LopHoc { get; set; } = null!;

    public virtual NguoiDung NguoiDung { get; set; } = null!;

    public virtual ICollection<PhieuHoTro> PhieuHoTros { get; set; } = new List<PhieuHoTro>();
}
