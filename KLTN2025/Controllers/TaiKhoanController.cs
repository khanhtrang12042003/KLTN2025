using Microsoft.AspNetCore.Mvc;
using KLTN2025.Data;
using KLTN2025.Models;
using System.Security.Cryptography;
using System.Text;

namespace KLTN2025.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly KLTNContext _context;

        public TaiKhoanController(KLTNContext context)
        {
            _context = context;
        }

        // ==================== CHỌN VAI TRÒ ====================
        public IActionResult ChonVaiTro()
        {
            return View();
        }

        // ==================== ĐĂNG KÝ ====================
        [HttpGet]
        public IActionResult DangKy(string role)
        {
            if (string.IsNullOrEmpty(role))
                return RedirectToAction("ChonVaiTro");

            ViewBag.Role = role;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DangKy(string hoTen, string tenDangNhap, string email, string matKhau, string role)
        {
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                ViewBag.ThongBao = "Tên đăng nhập và mật khẩu không được để trống!";
                ViewBag.Role = role;
                return View();
            }

            // Kiểm tra trùng tên đăng nhập hoặc email
            var tonTai = _context.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == tenDangNhap || u.Email == email);
            if (tonTai != null)
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc email đã tồn tại!";
                ViewBag.Role = role;
                return View();
            }

            // Tạo người dùng mới
            var nguoiDung = new NguoiDung
            {
                TenDangNhap = tenDangNhap,
                MaKhauHash = MaHoaMatKhau(matKhau),
                Email = email,
                HoTen = hoTen,
                VaiTro = role,
                GioiTinh = false,
                Sdt = "",
                TaoVaoLuc = DateTime.Now
            };

            _context.NguoiDungs.Add(nguoiDung);
            _context.SaveChanges();

            TempData["ThongBao"] = $"🎉 Đăng ký {(role == "giasu" ? "Gia sư" : "Phụ huynh")} thành công! Mời bạn đăng nhập.";
            return RedirectToAction("DangNhap", new { role = role });
        }

        // ==================== ĐĂNG NHẬP ====================
        [HttpGet]
        public IActionResult DangNhap(string role)
        {
            if (string.IsNullOrEmpty(role))
                return RedirectToAction("ChonVaiTro");

            ViewBag.Role = role;
            ViewBag.ThongBao = TempData["ThongBao"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DangNhap(string email, string password, string role)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.ThongBao = "Vui lòng nhập đầy đủ thông tin đăng nhập!";
                ViewBag.Role = role;
                return View();
            }

            string mkMaHoa = MaHoaMatKhau(password);
            var nguoiDung = _context.NguoiDungs.FirstOrDefault(u =>
                u.Email == email && u.MaKhauHash == mkMaHoa && u.VaiTro == role);

            if (nguoiDung == null)
            {
                ViewBag.ThongBao = "Sai email hoặc mật khẩu, hoặc bạn chọn sai vai trò!";
                ViewBag.Role = role;
                return View();
            }

            // Lưu session
            HttpContext.Session.SetString("UserName", nguoiDung.HoTen);
            HttpContext.Session.SetString("Role", nguoiDung.VaiTro);

            // Chuyển hướng đúng trang theo vai trò
            if (role == "giasu")
                return RedirectToAction("GiaSuTrangChu", "GiaSu");
            else if (role == "phuhuynh")
                return RedirectToAction("PhuHuynhTrangChu", "PhuHuynh");

            return RedirectToAction("ChonVaiTro");
        }

        // ==================== ĐĂNG XUẤT ====================
        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("ChonVaiTro");
        }

        // ==================== HÀM MÃ HÓA MẬT KHẨU ====================
        private string MaHoaMatKhau(string matKhau)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(matKhau));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
