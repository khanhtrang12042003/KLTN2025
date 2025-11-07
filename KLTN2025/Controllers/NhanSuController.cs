using KLTN2025.DTOs;
using KLTN2025.Models;
using Microsoft.AspNetCore.Mvc;
using KLTN2025.Services;

namespace KLTN2025.Controllers
{
    public class NhanSuController : Controller
    {
        private readonly KLTNContext _context;

        public NhanSuController(KLTNContext context)
        {
            _context = context;
        }
        // 🏠 Trang tổng quan nhân sự
        public IActionResult Index()
        {
            // Sau này có thể truyền dữ liệu thống kê thực tế ở đây
            ViewBag.UserName = "Nguyễn Thị Hạnh";
            return View();
        }

        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DangKy(DangKyDTO dangKyDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(dangKyDTO);
            }

            var tonTai = _context.NguoiDungs.FirstOrDefault
            (u => u.TenDangNhap == dangKyDTO.TenDangNhap || u.Email == dangKyDTO.Email);
            if (tonTai != null)
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc email đã tồn tại!";
                return View();
            }

            var nguoiDung = new NguoiDung
            {
                TenDangNhap = dangKyDTO.TenDangNhap,
                MaKhauHash = Hashpassword.MaHoaMatKhau(dangKyDTO.MatKhau),
                Email = dangKyDTO.Email,
                HoTen = dangKyDTO.HoTen,
                VaiTro = dangKyDTO.VaiTro,
                GioiTinh = dangKyDTO?.GioiTinh ?? false,
                Sdt = "",
                TaoVaoLuc = DateTime.Now
            };

            _context.NguoiDungs.Add(nguoiDung);
            _context.SaveChanges();
            return RedirectToAction("XacNhanDangKy", "TaiKhoan");
        }


        [HttpGet]
        public IActionResult DangNhap()
        {
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        // 📋 UC11 - Duyệt hồ sơ gia sư
        [HttpPost]
        public IActionResult DuyetHoSo()
        {
            string name = "Nguyễn Thị Hạnh";
            return Json(new { name });
        }

        [HttpGet]
        public IActionResult XemHoSoGiaSu()
        {
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
