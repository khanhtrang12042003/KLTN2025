using System.ComponentModel.DataAnnotations;

namespace KLTN2025.DTOs;

public class DangKyDTO
{
    [Required(ErrorMessage = "Đừng để trống họ tên mình nhé")]
    [MaxLength(100, ErrorMessage = "Đây là tên thật của bạn sao!")]
    public string HoTen { get; set; } = default!;

    [Required(ErrorMessage = "Đừng để trống tên đăng nhập nhé")]
    [MaxLength(100, ErrorMessage = "Tên đăng nhập tối đa 100 kí tự!")]
    public string TenDangNhap { get; set; } = default!;

    [Required(ErrorMessage = "Đừng để trống email mình nhé")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$",
        ErrorMessage = "Email phải là địa chỉ Gmail hợp lệ.")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Đừng để trống mật khẩu mình nhé")]
    [MaxLength(40, ErrorMessage = "Mật khẩu đối đa 40 kí tự!")]
    [MinLength(8, ErrorMessage = "Mật khẩu tối thiểu 8 kí tự")]
    public string MatKhau { get; set; } = default!;

    [Required]
    public string VaiTro { get; set; } = default!;
}