using Microsoft.AspNetCore.Mvc;
using KLTN2025.Data;
using KLTN2025.Models;
using KLTN2025.Services;
using System.Security.Cryptography;
using System.Text;
using KLTN2025.DTOs;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult DangKy(DangKyDTO dangKyDTO)
        {
            if (string.IsNullOrEmpty(dangKyDTO.TenDangNhap) || string.IsNullOrEmpty(dangKyDTO.MatKhau))
            {
                ViewBag.ThongBao = "Tên đăng nhập và mật khẩu không được để trống!";
                ViewBag.VaiTro = dangKyDTO.VaiTro;
                return View();
            }

            var tonTai = _context.NguoiDungs.FirstOrDefault
            (u => u.TenDangNhap == dangKyDTO.TenDangNhap || u.Email == dangKyDTO.Email);
            if (tonTai != null)
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc email đã tồn tại!";
                ViewBag.VaiTro = dangKyDTO.VaiTro;
                return View();
            }

            var nguoiDung = new NguoiDung
            {
                TenDangNhap = dangKyDTO.TenDangNhap,
                MaKhauHash = MaHoaMatKhau(dangKyDTO.MatKhau),
                Email = dangKyDTO.Email,
                HoTen = dangKyDTO.HoTen,
                VaiTro = dangKyDTO.VaiTro,
                GioiTinh = false,
                Sdt = "",
                TaoVaoLuc = DateTime.Now
            };

            _context.NguoiDungs.Add(nguoiDung);
            _context.SaveChanges();

            TempData["UserId"] = nguoiDung.NguoiDungId;
            TempData["ThongBao"] = $"🎉 Đăng ký {(dangKyDTO.VaiTro == "giasu" ? "Gia sư" : "Phụ huynh")} thành công! Mời bạn đăng nhập để tiếp tục.";
            return RedirectToAction("XacNhanDangKy");

        }

        // ==================== XÁC NHẬN ĐĂNG KÝ ====================
        [HttpGet]
        public IActionResult XacNhanDangKy()
        {
            int? userId = TempData["UserId"] as int?;
            if (userId == null)
                return RedirectToAction("ChonVaiTro");

            var user = _context.NguoiDungs.FirstOrDefault(u => u.NguoiDungId == userId);
            if (user == null)
                return RedirectToAction("ChonVaiTro");

            ViewBag.HoTen = user.HoTen;
            ViewBag.Email = user.Email;
            return View();
        }

        // ==================== ĐĂNG NHẬP ====================
        // ==================== ĐĂNG NHẬP (GET) ====================
        [HttpGet]
        public IActionResult DangNhap(string? role)
        {
            ViewBag.Role = role ?? "phuhuynh";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DangNhap(DangNhapDTO dangNhapDTO)
        {
            if (string.IsNullOrEmpty(dangNhapDTO.TenDangNhap) || string.IsNullOrEmpty(dangNhapDTO.MatKhau))
            {
                ViewBag.ThongBao = "Vui lòng nhập đầy đủ thông tin đăng nhập!";
                ViewBag.VaiTro = dangNhapDTO.VaiTro;
                return View();
            }

            string mkMaHoa = MaHoaMatKhau(dangNhapDTO.MatKhau);

            // ✅ Cho phép đăng nhập bằng Email hoặc Tên đăng nhập
            var nguoiDung = _context.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == dangNhapDTO.TenDangNhap
                && u.MaKhauHash == mkMaHoa);

            if (nguoiDung == null)
            {
                ViewBag.ThongBao = "Sai tài khoản hoặc mật khẩu, hoặc bạn chọn sai vai trò!";
                ViewBag.VaiTro = dangNhapDTO.VaiTro;
                return View();
            }

            // ✅ Lưu session đăng nhập
            HttpContext.Session.SetString("UserName", nguoiDung.HoTen);
            HttpContext.Session.SetString("Role", nguoiDung.VaiTro);
            HttpContext.Session.SetInt32("NguoiDungId", nguoiDung.NguoiDungId);


            // ✅ Chuyển hướng tùy vai trò
            if (nguoiDung.VaiTro.Equals("GiaSu", StringComparison.OrdinalIgnoreCase))
            {
                HttpContext.Session.SetString("Role", "GiaSu");
                return RedirectToAction("GiaSuTrangChu", "GiaSu");
            }
            else if (nguoiDung.VaiTro.Equals("PhuHuynh", StringComparison.OrdinalIgnoreCase))
            {
                HttpContext.Session.SetString("Role", "PhuHuynh");
                return RedirectToAction("PhuHuynhTrangChu", "PhuHuynh");
            }

            return RedirectToAction("ChonVaiTro");
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

        // Cập nhập thông tin
        [HttpGet]
        public async Task<IActionResult> CapNhapTaiKhoan()
        {
            int? nguoiDungId = HttpContext.Session.GetInt32("NguoiDungId");
            if (nguoiDungId is null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            NguoiDung? nguoiDung = await _context.NguoiDungs.FindAsync(nguoiDungId);
            if (nguoiDung is null)
            {
                return NotFound("Người dùng không tồn tại");
            }

            CapNhapTKDTO capNhapTKDTO = new CapNhapTKDTO
            {
                NguoiDungId = nguoiDung.NguoiDungId,
                HoTen = nguoiDung.HoTen,
                Email = nguoiDung.Email,
                GioiTinh = nguoiDung.GioiTinh ? "Nữ" : "Nam",
                SDT = nguoiDung.Sdt
            };
            return View(capNhapTKDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhapTaiKhoan(CapNhapTKDTO capNhapTKDTO)
        {
            NguoiDung? nguoiDung = await _context.NguoiDungs.FindAsync(capNhapTKDTO.NguoiDungId);

            if (nguoiDung is null) return NotFound();

            bool gt = false;
            if (capNhapTKDTO.GioiTinh == "Nữ")
                gt = true;

            if (!string.IsNullOrEmpty(capNhapTKDTO.HoTen)) nguoiDung.HoTen = capNhapTKDTO.HoTen;
            if (!string.IsNullOrEmpty(capNhapTKDTO.Email)) nguoiDung.Email = capNhapTKDTO.Email;
            if (!string.IsNullOrEmpty(capNhapTKDTO.GioiTinh)) nguoiDung.GioiTinh = gt;
            if (!string.IsNullOrEmpty(capNhapTKDTO.SDT)) nguoiDung.Sdt = capNhapTKDTO.SDT;

            await _context.SaveChangesAsync();
            return RedirectToAction("CapNhapTaiKhoan");
        }
    }
}
