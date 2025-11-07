using KLTN2025.DTOs;
using KLTN2025.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KLTN2025.Controllers
{
    public class PhuHuynhController : Controller
    {
        private readonly KLTNContext _context;
        public PhuHuynhController(KLTNContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        // GET: /PhuHuynh/PhuHuynhTrangChu
        public IActionResult PhuHuynhTrangChu()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TaoLop()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TaoLop(TaoLopDTO taoLopDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(taoLopDTO);
            }
            Console.WriteLine(HttpContext.Session.GetInt32("NguoiDungId"));
            var lopHoc = new LopHoc
            {
                PhuHuynhId = HttpContext.Session.GetInt32("NguoiDungId") ?? 1,
                MonHoc = taoLopDTO.MonHoc,
                KhoiLop = taoLopDTO.KhoiLop,
                DiaDiem = taoLopDTO.DiaDiem,
                LichHoc = taoLopDTO.LichHoc,
            };
            _context.LopHocs.Add(lopHoc);
            _context.SaveChanges();
            return View();
        }

        public async Task<IActionResult> LopChoDuyet()
        {
            List<LopHoc> lopHocs = await _context.LopHocs.ToListAsync();
            return View(lopHocs);
        }
        public IActionResult TimGiaSu()
        {
            return View();
        }
        public IActionResult ChiTietGiaSu()
        {
            return View();
        }
        public IActionResult QuanLyLopHoc()
        {
            return View();
        }
        public IActionResult ThongTinCaNhan()
        {
            return View();
        }
        public IActionResult DanhGiaGiaSu()
        {
            return View();
        }
    }
}
