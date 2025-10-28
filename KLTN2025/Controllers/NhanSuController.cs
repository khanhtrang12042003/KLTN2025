using Microsoft.AspNetCore.Mvc;

namespace KLTN2025.Controllers
{
    public class NhanSuController : Controller
    {
        // 🏠 Trang tổng quan nhân sự
        public IActionResult Index()
        {
            // Sau này có thể truyền dữ liệu thống kê thực tế ở đây
            ViewBag.UserName = "Nguyễn Thị Hạnh";
            return View();
        }

        // 📋 UC11 - Duyệt hồ sơ gia sư
        [HttpGet]
        public IActionResult DuyetHoSoGiaSu()
        {
            ViewBag.UserName = "Nguyễn Thị Hạnh";
            return View();
        }

        // 📄 Chi tiết hồ sơ gia sư (phụ trợ UC11)
        [HttpGet]
        public IActionResult ChiTietHoSo(int id)
        {
            ViewBag.UserName = "Nguyễn Thị Hạnh";
            ViewBag.GiaSuID = id;
            // TODO: lấy thông tin chi tiết gia sư từ DB
            return View();
        }

        // 📑 UC12 - Quản lý hợp đồng & chính sách
        [HttpGet]
        public IActionResult QuanLyHopDongChinhSach()
        {
            ViewBag.UserName = "Nguyễn Thị Hạnh";
            return View();
        }

        // 📚 UC19 - Phân công lớp học
        [HttpGet]
        public IActionResult PhanCongLopHoc()
        {
            ViewBag.UserName = "Nguyễn Thị Hạnh";
            return View();
        }

        // POST: Xử lý phân công (chưa kết nối DB)
        [HttpPost]
        public IActionResult PhanCongLopHoc(IFormCollection form)
        {
            string lopHocID = form["LopHocID"];
            string giaSuID = form["GiaSuID"];
            TempData["ThongBao"] = $"Đã phân công gia sư ID {giaSuID} cho lớp {lopHocID}";
            return RedirectToAction("PhanCongLopHoc");
        }
    }
}
