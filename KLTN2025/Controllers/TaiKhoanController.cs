using Microsoft.AspNetCore.Mvc;
using KLTN2025.Models;
using KLTN2025.Services;
using System.Security.Cryptography;
using System.Text;

namespace KLTN2025.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly KLTNContext _context;
        private readonly EmailService _emailService;

        // Tạo dictionary tạm để lưu token reset (vì bạn chưa dùng bảng TokenReset riêng)
        private static Dictionary<string, string> _resetTokens = new Dictionary<string, string>();

        public TaiKhoanController(KLTNContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }



        // ==================== ĐĂNG KÝ (GET) ====================
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        // ==================== ĐĂNG KÝ (POST) ====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DangKy(string hoTen, string tenDangNhap, string email, string matKhau, string role)
        {
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                ViewBag.ThongBao = "⚠️ Tên đăng nhập và mật khẩu không được để trống!";
                return View();
            }

            var tonTai = _context.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == tenDangNhap || u.Email == email);
            if (tonTai != null)
            {
                ViewBag.ThongBao = "❌ Tên đăng nhập hoặc email đã tồn tại!";
                return View();
            }

            var nguoiDung = new NguoiDung
            {
                HoTen = hoTen,
                TenDangNhap = tenDangNhap,
                Email = email,
                MaKhauHash = MaHoaMatKhau(matKhau),
                VaiTro = role, // "GiaSu" hoặc "PhuHuynh"
                GioiTinh = false,
                Sdt = "",
                TaoVaoLuc = DateTime.Now
            };

            _context.NguoiDungs.Add(nguoiDung);
            _context.SaveChanges();

            TempData["ThongBao"] = $"🎉 Đăng ký {role} thành công! Mời bạn đăng nhập để tiếp tục.";
            return RedirectToAction("DangNhap");
        }

        // ==================== ĐĂNG NHẬP (GET) ====================
        [HttpGet]
        public IActionResult DangNhap()
        {
            return View();
        }

        // ==================== ĐĂNG NHẬP (POST) ====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DangNhap(string emailOrUsername, string password, string role)
        {
            if (string.IsNullOrEmpty(emailOrUsername) || string.IsNullOrEmpty(password))
            {
                ViewBag.ThongBao = "⚠️ Vui lòng nhập đầy đủ thông tin đăng nhập!";
                return View();
            }

            string mkMaHoa = MaHoaMatKhau(password);

            var nguoiDung = _context.NguoiDungs.FirstOrDefault(u =>
                (u.Email == emailOrUsername || u.TenDangNhap == emailOrUsername)
                && u.MaKhauHash == mkMaHoa
                && u.VaiTro.ToLower() == role.ToLower());

            if (nguoiDung == null)
            {
                ViewBag.ThongBao = "❌ Sai tài khoản hoặc mật khẩu, hoặc vai trò không khớp!";
                return View();
            }

            // ✅ Lưu session
            HttpContext.Session.SetInt32("NguoiDungId", nguoiDung.NguoiDungId);
            HttpContext.Session.SetString("UserName", nguoiDung.HoTen);
            HttpContext.Session.SetString("Role", nguoiDung.VaiTro);

            // ✅ Điều hướng theo vai trò
            if (nguoiDung.VaiTro.Equals("GiaSu", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("GiaSuTrangChu", "GiaSu");
            }
            else if (nguoiDung.VaiTro.Equals("PhuHuynh", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("PhuHuynhTrangChu", "PhuHuynh");
            }

            // Nếu là nhân viên trung tâm (chạy local)
            if (nguoiDung.VaiTro.Equals("NhanVien", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("DangNhap");
        }

        // ==================== QUÊN MẬT KHẨU (GET) ====================
        [HttpGet]
        public IActionResult QuenMatKhau()
        {
            return View();
        }

        // ==================== QUÊN MẬT KHẨU (POST) ====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuenMatKhau(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.ThongBao = "⚠️ Vui lòng nhập địa chỉ email!";
                return View();
            }

            var nguoiDung = _context.NguoiDungs.FirstOrDefault(u => u.Email == email);
            if (nguoiDung == null)
            {
                ViewBag.ThongBao = "❌ Không tìm thấy tài khoản với email này!";
                return View();
            }

            try
            {
                // ✅ Tạo token và lưu tạm
                string token = Guid.NewGuid().ToString();
                _resetTokens[email] = token;

                // ✅ Tạo link đặt lại
                string resetLink = $"{Request.Scheme}://{Request.Host}/TaiKhoan/DatLaiMatKhau?email={email}&token={token}";

                // ✅ Gửi mail
                string subject = "Đặt lại mật khẩu - Trung tâm Gia sư KLTN 2025";
                string body = $@"
                    <h3>Xin chào {nguoiDung.HoTen},</h3>
                    <p>Bạn vừa yêu cầu đặt lại mật khẩu.</p>
                    <p>Nhấn vào liên kết bên dưới để tạo mật khẩu mới:</p>
                    <p><a href='{resetLink}'>👉 Đặt lại mật khẩu tại đây</a></p>
                    <p>Nếu bạn không yêu cầu, vui lòng bỏ qua email này.</p>";

                await _emailService.SendEmailAsync(email, subject, body);

                ViewBag.ThongBao = "✅ Liên kết đặt lại mật khẩu đã được gửi đến email của bạn.";
            }
            catch (Exception ex)
            {
                ViewBag.ThongBao = $"❌ Lỗi khi gửi mail: {ex.Message}";
            }

            return View();
        }

        // ==================== ĐẶT LẠI MẬT KHẨU (GET) ====================
        [HttpGet]
        public IActionResult DatLaiMatKhau(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                ViewBag.Title = "Liên kết không hợp lệ";
                ViewBag.Message = "Thiếu email hoặc token trong đường dẫn.";
                return View("ThongBao");
            }

            if (!_resetTokens.ContainsKey(email) || _resetTokens[email] != token)
            {
                ViewBag.Title = "Liên kết không hợp lệ hoặc hết hạn";
                ViewBag.Message = "Vui lòng gửi lại yêu cầu quên mật khẩu.";
                return View("ThongBao");
            }

            ViewBag.Email = email;
            ViewBag.Token = token;
            return View();
        }

        // ==================== ĐẶT LẠI MẬT KHẨU (POST) ====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DatLaiMatKhau(string email, string token, string newPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token) || string.IsNullOrEmpty(newPassword))
            {
                ViewBag.Title = "Thiếu thông tin";
                ViewBag.Message = "Vui lòng nhập đầy đủ thông tin để đặt lại mật khẩu.";
                return View("ThongBao");
            }

            if (!_resetTokens.ContainsKey(email) || _resetTokens[email] != token)
            {
                ViewBag.Title = "Liên kết không hợp lệ";
                ViewBag.Message = "Đường dẫn này đã hết hạn hoặc không đúng.";
                return View("ThongBao");
            }

            var nguoiDung = _context.NguoiDungs.FirstOrDefault(u => u.Email == email);
            if (nguoiDung == null)
            {
                ViewBag.Title = "Không tìm thấy tài khoản";
                ViewBag.Message = "Email này không tồn tại trong hệ thống.";
                return View("ThongBao");
            }

            // ✅ Cập nhật mật khẩu mới
            nguoiDung.MaKhauHash = MaHoaMatKhau(newPassword);
            _context.SaveChanges();

            _resetTokens.Remove(email); // Xóa token sau khi dùng

            ViewBag.Title = "Thành công";
            ViewBag.Message = "Mật khẩu đã được đặt lại thành công. Vui lòng đăng nhập lại.";
            return View("ThongBao");
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuenMatKhauAjax(string email)
        {
            if (string.IsNullOrEmpty(email))
                return Json(new { success = false, message = "⚠️ Vui lòng nhập địa chỉ email!" });

            var nguoiDung = _context.NguoiDungs.FirstOrDefault(u => u.Email == email);
            if (nguoiDung == null)
                return Json(new { success = false, message = "❌ Không tìm thấy tài khoản với email này!" });

            try
            {
                string token = Guid.NewGuid().ToString();
                _resetTokens[email] = token;
                string resetLink = $"{Request.Scheme}://{Request.Host}/TaiKhoan/DatLaiMatKhau?email={email}&token={token}";

                string subject = "Đặt lại mật khẩu - Trung tâm Gia sư KLTN 2025";
                string body = $@"
            <h3>Xin chào {nguoiDung.HoTen},</h3>
            <p>Nhấn vào liên kết sau để đặt lại mật khẩu:</p>
            <a href='{resetLink}'>👉 Đặt lại mật khẩu</a>";

                await _emailService.SendEmailAsync(email, subject, body);

                return Json(new { success = true, message = "✅ Liên kết đặt lại mật khẩu đã được gửi đến email của bạn!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"❌ Lỗi gửi mail: {ex.Message}" });
            }
        }

    }
}
