using Microsoft.AspNetCore.Mvc;

namespace KLTN2025.Controllers
{
    public class PhuHuynhController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // GET: /PhuHuynh/PhuHuynhTrangChu
        public IActionResult PhuHuynhTrangChu()
        {
            return View();
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
