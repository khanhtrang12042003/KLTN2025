using Microsoft.AspNetCore.Mvc;
using KLTN2025.Models;
using KLTN2025.Services;
using System.Security.Cryptography;
using System.Text;
using KLTN2025.DTOs;

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
            return RedirectToAction("XacNhanDangKy");
        }

        // ==================== XÁC NHẬN ĐĂNG KÝ ====================
        [HttpGet]
        public IActionResult XacNhanDangKy()
        {
            return View();
        }

        // ==================== ĐĂNG NHẬP ====================
        // ==================== ĐĂNG NHẬP (GET) ====================
        [HttpGet]
        public IActionResult DangNhap()
        {
            return View();
        }

        // ==================== ĐĂNG NHẬP (POST) ====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DangNhap(DangNhapDTO dangNhapDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(dangNhapDTO);
            }

            string mkMaHoa = Hashpassword.MaHoaMatKhau(dangNhapDTO.MatKhau);

            var nguoiDung = _context.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == dangNhapDTO.TenDangNhap
                && u.MaKhauHash == mkMaHoa);

            if (nguoiDung == null)
            {
                ViewBag.ThongBao = "Thông tin đăng nhập không đúng";
                return View();
            }

            // ✅ Lưu session
            HttpContext.Session.SetInt32("NguoiDungId", nguoiDung.NguoiDungId);
            HttpContext.Session.SetString("UserName", nguoiDung.HoTen);
            HttpContext.Session.SetString("Role", nguoiDung.VaiTro);
            HttpContext.Session.SetInt32("NguoiDungId", nguoiDung.NguoiDungId);

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
            if (nguoiDung.VaiTro.Equals("NhanSu", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index", "NhanSu");
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
        [KiemTraDangNhap]
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
            nguoiDung.MaKhauHash = Hashpassword.MaHoaMatKhau(newPassword);
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
            return RedirectToAction("DangNhap");
        }

        // ==================== HÀM MÃ HÓA MẬT KHẨU ====================
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
        [KiemTraDangNhap]
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
            ViewBag.Layout = nguoiDung.VaiTro switch
            {
                "GiaSu" => "_LayoutGiaSu",
                "PhuHuynh" => "_LayoutPhuHuynh",
                _ => "_Layout"
            };
            CapNhapTKDTO capNhapTKDTO = new CapNhapTKDTO
            {
                NguoiDungId = nguoiDung.NguoiDungId,
                HoTen = nguoiDung.HoTen,
                Email = nguoiDung.Email,
                GioiTinh = nguoiDung.GioiTinh ? "Nữ" : "Nam",
                SDT = nguoiDung?.Sdt ?? ""
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
