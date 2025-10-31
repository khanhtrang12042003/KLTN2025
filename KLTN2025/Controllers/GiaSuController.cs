using KLTN2025.Data;
using KLTN2025.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KLTN2025.Controllers
{
    public class GiaSuController : Controller
    {
        private readonly KLTNContext _context;
        public GiaSuController(KLTNContext context)
        {
            _context = context;
        }

        // ---------------------- Trang chủ gia sư ------------------------
        public IActionResult GiaSuTrangChu()
        {
            return View();
        }

        // ---------------------- Danh sách lớp mới ------------------------
        [HttpGet]
        public async Task<IActionResult> DanhSachLopMoi(string? monHoc, string? khoiLop, string? khuVuc)
        {
            if (!KiemTraDangNhap()) return ChuyenHuongDangNhap();

            var query = _context.LopHocs
                .Include(l => l.PhuHuynh)
                .Include(l => l.UngTuyens)
                .Where(l => l.TrangThai != "Đã hoàn thành" && l.TrangThai != "Kết thúc");

            // Lọc theo môn học
            if (!string.IsNullOrEmpty(monHoc) && monHoc != "Tất cả")
                query = query.Where(l => l.MonHoc != null && l.MonHoc.Contains(monHoc));

            // Lọc theo khối lớp
            if (!string.IsNullOrEmpty(khoiLop) && khoiLop != "Tất cả")
            {
                if (byte.TryParse(khoiLop, out byte parsedKhoiLop))
                {
                    query = query.Where(l => l.KhoiLop == parsedKhoiLop);
                }
                else
                {
                    // Trường hợp người dùng chọn nhóm (ví dụ: "Lớp 6-9")
                    if (khoiLop.Contains("1-5"))
                        query = query.Where(l => l.KhoiLop >= 1 && l.KhoiLop <= 5);
                    else if (khoiLop.Contains("6-9"))
                        query = query.Where(l => l.KhoiLop >= 6 && l.KhoiLop <= 9);
                    else if (khoiLop.Contains("10-12"))
                        query = query.Where(l => l.KhoiLop >= 10 && l.KhoiLop <= 12);
                }
            }

            // Lọc theo khu vực
            if (!string.IsNullOrEmpty(khuVuc) && khuVuc != "Tất cả")
                query = query.Where(l => l.DiaDiem != null && l.DiaDiem.Contains(khuVuc));

            var lopList = await query.OrderByDescending(l => l.NgayTao).ToListAsync();

            ViewBag.MonHoc = monHoc;
            ViewBag.KhoiLop = khoiLop;
            ViewBag.KhuVuc = khuVuc;

            // Lấy GiaSuId để gắn vào nút Ứng tuyển
            var nguoiDungId = HttpContext.Session.GetInt32("NguoiDungId");
            var giaSu = await _context.GiaSus.FirstOrDefaultAsync(g => g.NguoiDungId == nguoiDungId);
            ViewBag.GiaSuId = giaSu?.GiaSuId ?? 0;

            return View(lopList);
        }



        // ---------------------- Hồ sơ gia sư ------------------------
        public async Task<IActionResult> HoSoGiaSu()
        {
            if (!KiemTraDangNhap()) return ChuyenHuongDangNhap();

            var nguoiDungId = HttpContext.Session.GetInt32("NguoiDungId");

            var giaSu = await _context.GiaSus
                .Include(g => g.NguoiDung)
                .FirstOrDefaultAsync(g => g.NguoiDungId == nguoiDungId.Value);

            if (giaSu == null)
            {
                giaSu = new GiaSu
                {
                    NguoiDungId = nguoiDungId.Value,
                    KhuVucDay = "",
                    TrangThai = "Chưa duyệt",
                    NgayCapNhat = DateTime.Now
                };
                _context.GiaSus.Add(giaSu);
                await _context.SaveChangesAsync();
            }

            // ✅ Nếu đang chờ duyệt thì chuyển qua trang chờ duyệt
            if (giaSu.TrangThai == "Chờ duyệt")
                return RedirectToAction("ChoDuyet");

            // ✅ Nếu bị từ chối thì hiển thị lại form với thông báo
            if (giaSu.TrangThai == "Từ chối")
                ViewBag.ThongBao = "Hồ sơ của bạn bị từ chối. Vui lòng cập nhật lại thông tin.";

            return View(giaSu);
        }


        // ---------------------- Cập nhật hồ sơ ------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CapNhatHoSo(GiaSu model, IFormFile? AnhDaiDien, IFormFile? AnhBangTotNghiep, IFormFile? FileKemTheo)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("NguoiDungId");
                if (userId == null)
                {
                    TempData["Error"] = "Phiên đăng nhập hết hạn, vui lòng đăng nhập lại!";
                    return RedirectToAction("DangNhap", "TaiKhoan");
                }

                var giaSu = _context.GiaSus.FirstOrDefault(g => g.NguoiDungId == userId);
                if (giaSu == null)
                {
                    TempData["Error"] = "Không tìm thấy thông tin gia sư!";
                    return RedirectToAction("GiaSuTrangChu");
                }

                // Cập nhật các trường
                giaSu.KinhNghiem = model.KinhNghiem;
                giaSu.KyNang = model.KyNang;
                giaSu.KhuVucDay = model.KhuVucDay;
                giaSu.LichRanh = model.LichRanh;
                giaSu.DaiHoc = model.DaiHoc;
                giaSu.NganhHoc = model.NganhHoc;
                giaSu.TrangThai = "Chờ duyệt";
                giaSu.NgayCapNhat = DateTime.Now;

                // Upload file
                if (AnhDaiDien != null && AnhDaiDien.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(AnhDaiDien.FileName);
                    string path = Path.Combine("wwwroot/uploads", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        AnhDaiDien.CopyTo(stream);
                    }
                    giaSu.AnhDaiDien = "/uploads/" + fileName;

                }

                if (AnhBangTotNghiep != null && AnhBangTotNghiep.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(AnhBangTotNghiep.FileName);
                    string path = Path.Combine("wwwroot/uploads", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        AnhBangTotNghiep.CopyTo(stream);
                    }
                    giaSu.BangTotNghiep = "/uploads/" + fileName;

                }

                _context.SaveChanges();

                // ✅ Sau khi lưu, chuyển đến trang thông báo
                return RedirectToAction("ChoDuyet");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Đã xảy ra lỗi khi cập nhật hồ sơ: " + ex.Message;
                return RedirectToAction("HoSoGiaSu");
            }
        }


        public IActionResult ChoDuyet()
        {
            return View();
        }

        // ---------------------- Danh sách lớp gợi ý ------------------------
        public async Task<IActionResult> DanhSachLopGoiY()
        {
            if (!KiemTraDangNhap()) return ChuyenHuongDangNhap();

            var nguoiDungId = HttpContext.Session.GetInt32("NguoiDungId");
            var giaSu = await _context.GiaSus.FirstOrDefaultAsync(g => g.NguoiDungId == nguoiDungId.Value);
            if (giaSu == null)
            {
                TempData["Error"] = "Không tìm thấy hồ sơ gia sư.";
                return RedirectToAction(nameof(GiaSuTrangChu));
            }

            var query = _context.LopHocs
                .Include(l => l.PhuHuynh)
                .Include(l => l.UngTuyens)
                .Where(l => l.TrangThai != "Đã nhận" && l.TrangThai != "Đã hoàn thành");

            if (!string.IsNullOrWhiteSpace(giaSu.KhuVucDay))
                query = query.Where(l => l.DiaDiem != null && l.DiaDiem.Contains(giaSu.KhuVucDay));

            var lopGoiY = await query
                .OrderByDescending(l => l.NgayTao)
                .ToListAsync();

            ViewBag.GiaSu = giaSu;
            return View(lopGoiY);
        }

        // ---------------------- Chi tiết lớp học ------------------------
        public async Task<IActionResult> ChiTietLopHoc(int id)
        {
            if (!KiemTraDangNhap()) return ChuyenHuongDangNhap();

            var nguoiDungId = HttpContext.Session.GetInt32("NguoiDungId");
            var giaSu = await _context.GiaSus.FirstOrDefaultAsync(g => g.NguoiDungId == nguoiDungId);

            var lop = await _context.LopHocs
                .Include(l => l.PhuHuynh)
                .Include(l => l.UngTuyens).ThenInclude(u => u.GiaSu)
                .Include(l => l.PhanHois)
                .FirstOrDefaultAsync(l => l.LopHocId == id);

            if (lop == null)
                return NotFound();

            ViewBag.GiaSuId = giaSu?.GiaSuId ?? 0;
            return View(lop);
        }

        // ---------------------- Ứng tuyển lớp ------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UngTuyen(int lopHocId, int giaSuId)
        {
            if (!KiemTraDangNhap()) return ChuyenHuongDangNhap();

            var lop = await _context.LopHocs.FindAsync(lopHocId);
            var gs = await _context.GiaSus.FindAsync(giaSuId);
            if (lop == null || gs == null)
                return NotFound();

            var existed = await _context.UngTuyens
                .AnyAsync(u => u.LopHocId == lopHocId && u.GiaSuId == giaSuId);

            if (existed)
            {
                TempData["Warning"] = "Bạn đã ứng tuyển lớp này rồi.";
                return RedirectToAction(nameof(ChiTietLopHoc), new { id = lopHocId });
            }

            var ung = new UngTuyen
            {
                GiaSuId = giaSuId,
                LopHocId = lopHocId,
                NgayUngTuyen = DateTime.Now,
                TrangThai = "Đang chờ"
            };
            _context.UngTuyens.Add(ung);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Ứng tuyển thành công, vui lòng chờ duyệt.";
            return RedirectToAction(nameof(ChiTietLopHoc), new { id = lopHocId });
        }

        // ---------------------- Danh sách ứng tuyển ------------------------
        public async Task<IActionResult> DanhSachUngTuyen()
        {
            if (!KiemTraDangNhap()) return ChuyenHuongDangNhap();

            var nguoiDungId = HttpContext.Session.GetInt32("NguoiDungId");
            var giaSu = await _context.GiaSus.FirstOrDefaultAsync(g => g.NguoiDungId == nguoiDungId.Value);

            var ungList = await _context.UngTuyens
                .Include(u => u.LopHoc).ThenInclude(l => l.PhuHuynh)
                .Where(u => u.GiaSuId == giaSu.GiaSuId)
                .OrderByDescending(u => u.NgayUngTuyen)
                .ToListAsync();

            ViewBag.GiaSuId = giaSu.GiaSuId;
            return View(ungList);
        }

        // ---------------------- Hủy ứng tuyển ------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HuyUngTuyen(int ungTuyenId)
        {
            if (!KiemTraDangNhap()) return ChuyenHuongDangNhap();

            var ung = await _context.UngTuyens.FindAsync(ungTuyenId);
            if (ung == null)
                return NotFound();

            ung.TrangThai = "Hủy";
            _context.UngTuyens.Update(ung);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đã hủy ứng tuyển.";
            return RedirectToAction(nameof(DanhSachUngTuyen));
        }

        // ---------------------- Thanh toán ------------------------
        public async Task<IActionResult> ThanhToan()
        {
            if (!KiemTraDangNhap()) return ChuyenHuongDangNhap();

            var nguoiDungId = HttpContext.Session.GetInt32("NguoiDungId");
            var giaSu = await _context.GiaSus.FirstOrDefaultAsync(g => g.NguoiDungId == nguoiDungId.Value);
            if (giaSu == null)
            {
                TempData["Error"] = "Không tìm thấy hồ sơ gia sư.";
                return RedirectToAction(nameof(GiaSuTrangChu));
            }

            var danhSachThanhToan = await _context.ThanhToans
                .Include(t => t.LopHoc).ThenInclude(l => l.PhuHuynh)
                .Where(t => t.GiaSuId == giaSu.GiaSuId)
                .OrderByDescending(t => t.NgayThanhToan)
                .ToListAsync();

            return View(danhSachThanhToan);
        }

        // ---------------------- Xác nhận thanh toán ------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XacNhanThanhToan(int id)
        {
            if (!KiemTraDangNhap()) return ChuyenHuongDangNhap();

            var thanhToan = await _context.ThanhToans.FindAsync(id);
            if (thanhToan == null)
            {
                TempData["Error"] = "Không tìm thấy giao dịch.";
                return RedirectToAction(nameof(ThanhToan));
            }

            thanhToan.TrangThai = "Đã thanh toán";
            thanhToan.NgayThanhToan = DateTime.Now;
            _context.ThanhToans.Update(thanhToan);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Xác nhận thanh toán thành công.";
            return RedirectToAction(nameof(ThanhToan));
        }

        // ---------------------- Báo lỗi thanh toán ------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BaoLoiThanhToan(int id)
        {
            if (!KiemTraDangNhap()) return ChuyenHuongDangNhap();

            var thanhToan = await _context.ThanhToans.FindAsync(id);
            if (thanhToan == null)
            {
                TempData["Error"] = "Không tìm thấy giao dịch.";
                return RedirectToAction(nameof(ThanhToan));
            }

            thanhToan.TrangThai = "Gặp sự cố";
            _context.ThanhToans.Update(thanhToan);
            await _context.SaveChangesAsync();

            TempData["Warning"] = "Đã báo lỗi thanh toán. Bộ phận hỗ trợ sẽ kiểm tra.";
            return RedirectToAction(nameof(ThanhToan));
        }

        // ---------------------- Thông báo & hỗ trợ ------------------------
        public IActionResult ThongBao() => View();
        public IActionResult PhanHoiHoTro() => View();

        // ---------------------- HÀM KIỂM TRA ĐĂNG NHẬP ------------------------
        private bool KiemTraDangNhap()
        {
            var nguoiDungId = HttpContext.Session.GetInt32("NguoiDungId");
            var role = HttpContext.Session.GetString("Role");
            return nguoiDungId != null && role == "GiaSu";
        }

        private IActionResult ChuyenHuongDangNhap()
        {
            TempData["Error"] = "Vui lòng đăng nhập bằng tài khoản gia sư.";
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
    }
}
