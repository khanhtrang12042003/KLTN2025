using Microsoft.AspNetCore.Mvc;

namespace KLTN2025.Controllers
{
    public class GiaSuController : Controller
    {
        public IActionResult GiaSuTrangChu()
        {
            return View();
        }
        // 👉 Thêm action này để hiển thị trang DanhSachLopMoi
        public IActionResult DanhSachLopMoi()
        {
            return View();
        }
        public IActionResult HoSoGiaSu()
        {
            return View();
        }
        public IActionResult DanhSachLopGoiY()
        {
            return View();
        }
        public IActionResult ThanhToan()
        {
            return View();
        }
        public IActionResult ChiTietThanhToan()
        {
            return View();
        }
        public IActionResult ChiTietLopHoc(int id)
        {
            // Sau này sẽ lấy dữ liệu từ DB
            ViewBag.LopHocID = id;
            return View();
        }
        public IActionResult UngTuyenLopHoc()
        {
            return View();
        }
        public IActionResult ThongBao()
        {
            return View();
        }
        public IActionResult PhanHoiHoTro()
        {
            return View();
        }

    }


}
