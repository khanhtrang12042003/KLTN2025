using Microsoft.AspNetCore.Mvc;

namespace KLTN2025.Controllers
{
    public class CSKHController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.UserName = "Nguyễn Hương Lan";
            return View();
        }

        // 📩 UC31 - Danh sách phản hồi / khiếu nại
        [HttpGet]
        public IActionResult DanhSachPhanHoi()
        {
            ViewBag.UserName = "Nguyễn Hương Lan";
            return View();
        }

        // 📑 UC32 - Xử lý phản hồi
        [HttpGet]
        public IActionResult XuLyPhanHoi()
        {
            ViewBag.UserName = "Nguyễn Hương Lan";
            return View();
        }

        // 🔍 Xem chi tiết phản hồi
        [HttpGet]
        public IActionResult PhanHoiChiTiet(int id)
        {
            ViewBag.UserName = "Nguyễn Hương Lan";
            ViewBag.PhanHoiID = id;
            return View();
        }

        // 📊 UC32 - Thống kê & đánh giá hài lòng
        [HttpGet]
        public IActionResult ThongKeDanhGia()
        {
            ViewBag.UserName = "Nguyễn Hương Lan";
            return View();
        }
    }
}
