using System.ComponentModel.DataAnnotations;

namespace KLTN2025.DTOs;

public class DangNhapDTO
{
    [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
    public string TenDangNhap { get; set; } = default!;

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    public string MatKhau { get; set; } = default!;

    [Required]
    public string VaiTro { get; set; } = default!;
}