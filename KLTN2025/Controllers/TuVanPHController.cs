using Microsoft.AspNetCore.Mvc;

namespace KLTN2025.Controllers
{
    public class TuVanPHController : Controller
    {
        // 🏠 Trang tổng quan tư vấn phụ huynh (Dashboard)
        public IActionResult Index()
        {
            ViewBag.UserName = "Nguyễn Hồng Phúc";
            return View();
        }

        // 📋 UC14 - Quản lý lớp học
        [HttpGet]
        public IActionResult QuanLyLopHoc()
        {
            ViewBag.UserName = "Nguyễn Hồng Phúc";
            return View();
        }

        // ➕ UC14 - Thêm lớp học mới
        [HttpGet]
        public IActionResult ThemLopHoc()
        {
            ViewBag.UserName = "Nguyễn Hồng Phúc";
            return View();
        }

        [HttpPost]
        public IActionResult ThemLopHoc(IFormCollection form)
        {
            // Sau này sẽ lưu lớp học vào DB
            TempData["ThongBao"] = "Đã thêm lớp học mới thành công!";
            return RedirectToAction("QuanLyLopHoc");
        }

        // 🛠 UC15 - Cập nhật tình trạng lớp học
        [HttpGet]
        public IActionResult CapNhatTinhTrangLop()
        {
            ViewBag.UserName = "Nguyễn Hồng Phúc";
            return View();
        }

        [HttpPost]
        public IActionResult CapNhatTinhTrangLop(IFormCollection form)
        {
            TempData["ThongBao"] = "Đã cập nhật tình trạng lớp học!";
            return RedirectToAction("QuanLyLopHoc");
        }

        // 🗓 UC18 - Sắp xếp lịch học thử
        [HttpGet]
        public IActionResult SapLichHocThu()
        {
            ViewBag.UserName = "Nguyễn Hồng Phúc";
            return View();
        }

        [HttpPost]
        public IActionResult SapLichHocThu(IFormCollection form)
        {
            TempData["ThongBao"] = "Đã lưu lịch học thử!";
            return RedirectToAction("QuanLyLopHoc");
        }
    }
}
