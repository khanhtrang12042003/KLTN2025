using System.ComponentModel.DataAnnotations;

namespace KLTN2025.DTOs;

public class TaoLopDTO
{
    [Required(ErrorMessage = "Anh/chị muốn con học môn nào ạ")]
    public string MonHoc { get; set; } = default!;

    [Required(ErrorMessage = "Không để trống lớp học")]
    [Range(1, 9, ErrorMessage = "Chỉ dạy lớp 1 đến lớp 9")]
    public byte? KhoiLop { get; set; }

    [Required(ErrorMessage = "Đừng quên địa điểm nhé")]
    public string DiaDiem { get; set; } = default!;

    public string? LichHoc { get; set; }
}