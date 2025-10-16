using System.Diagnostics;
using KLTN2025.Models;
using Microsoft.AspNetCore.Mvc;

namespace KLTN2025.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Khi user ch?n vai tr�
        public IActionResult ChonVaiTro(string role)
        {
            if (role == "giasu" || role == "phuhuynh")
            {
                TempData["Role"] = role; // l?u t?m ?? form ??ng nh?p / ??ng k� bi?t vai tr�
                return RedirectToAction("DangNhap", "TaiKhoan", new { role });
            }

            return RedirectToAction("Index");
        }
    }
}
