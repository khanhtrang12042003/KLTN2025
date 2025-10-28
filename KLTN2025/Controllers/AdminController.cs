using Microsoft.AspNetCore.Mvc;

namespace KLTN2025.Controllers
{
    public class AdminController : Controller
    {

        // 🏠 UC33 - Dashboard thống kê nhanh
        public IActionResult Index()
        {
            ViewBag.UserName = "Admin Bảo Tuân";
            return View();
        }

        // 👥 UC06 - Xem danh sách người dùng
        [HttpGet]
        public IActionResult QuanLyNguoiDung()
        {
            ViewBag.UserName = "Admin Bảo Tuân";
            return View();
        }

        // ✏ UC07 - Chỉnh sửa tài khoản
        [HttpGet]
        public IActionResult ChinhSuaTaiKhoan(int id)
        {
            ViewBag.UserName = "Admin Bảo Tuân";
            ViewBag.UserID = id;
            return View();
        }

        // ➕ UC09 - Thêm tài khoản mới
        [HttpGet]
        public IActionResult ThemNhanVien()
        {
            ViewBag.UserName = "Admin Bảo Tuân";
            return View();
        }

        [HttpPost]
        public IActionResult ThemNhanVien(IFormCollection form)
        {
            TempData["ThongBao"] = "Thêm nhân viên thành công!";
            return RedirectToAction("QuanLyNguoiDung");
        }

        // 🗑 UC08 - Xóa tài khoản
        [HttpPost]
        public IActionResult XoaTaiKhoan(int id)
        {
            TempData["ThongBao"] = $"Đã xóa tài khoản có ID {id}";
            return RedirectToAction("QuanLyNguoiDung");
        }

        // 📊 UC33 - Báo cáo thống kê
        [HttpGet]
        public IActionResult ThongKeBaoCao()
        {
            ViewBag.UserName = "Admin Bảo Tuân";
            return View();
        }
    }
}
