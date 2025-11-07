
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
       public IActionResult DanhSachLopMoi()
        {
            return View();
        }


        // ---------------------- Hồ sơ gia sư ------------------------
       public IActionResult HoSoGiaSu()
        {
            return View();
        }


        // ---------------------- Cập nhật hồ sơ ------------------------
       public IActionResult CapNhatHoSo()
        {
            return View();
        }


        public IActionResult ChoDuyet()
        {
            return View();
        }

        // ---------------------- Danh sách lớp gợi ý ------------------------
       public IActionResult DanhSachLopGoiY()
        {
            return View();
        }
        // ---------------------- Chi tiết lớp học ------------------------
       public IActionResult ChiTietLopHoc(int id)
        {
            return View();
        }
        // ---------------------- Ứng tuyển lớp ------------------------
       public IActionResult UngTuyenLop(int id)
        {
            return View();
        }

        // ---------------------- Danh sách ứng tuyển ------------------------
        

        // ---------------------- Thanh toán ------------------------
        public IActionResult ThanhToan()
        {
            return View();
        }

       
       
        public IActionResult ThongBao() => View();
        public IActionResult PhanHoiHoTro() => View();

        
        
    }
}
