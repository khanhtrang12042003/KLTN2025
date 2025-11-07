using System.Collections.Generic;

namespace KLTN2025.Models;

public partial class NhanSu
{
    public int NguoiDungId { get; set; }
    public string VaiTro { get; set; } = null!;
    public virtual NguoiDung NguoiDung { get; set; } = null!;

    public virtual ICollection<LopHoc> LopHocsNhanSu { get; set; } = new List<LopHoc>();
    public virtual ICollection<UngTuyen> UngTuyens { get; set; } = new List<UngTuyen>();
    public virtual ICollection<PhanCong> PhanCongs { get; set; } = new List<PhanCong>();
    public virtual ICollection<PhieuHoTro> PhieuHoTros { get; set; } = new List<PhieuHoTro>();
    public virtual ICollection<HoSoGiaSu> HoSoGiaSus { get; set; } = new List<HoSoGiaSu>();
}
