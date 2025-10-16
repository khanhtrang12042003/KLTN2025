using Microsoft.AspNetCore.Mvc;

namespace KLTN2025.Controllers
{
    public class TaiKhoanController : Controller
    {
        public IActionResult DangNhap(string role)
        {
            ViewBag.Role = role ?? TempData["Role"]?.ToString();
            return View();
        }

        [HttpPost]
        public IActionResult DangNhap(string tenDangNhap, string matKhau, string role)
        {
            // xử lý đăng nhập
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DangKy(string role)
        {
            ViewBag.Role = role ?? TempData["Role"]?.ToString();
            return View();
        }

        [HttpPost]
        public IActionResult DangKy(string hoTen, string tenDangNhap, string email, string matKhau, string role)
        {
            // Lưu user với Role = Gia sư / Phụ huynh
            return RedirectToAction("DangNhap", new { role });
        }
        public IActionResult ChonVaiTro()
        {
            return View();
        }

    }
}
