using KLTN2025.Services;
using Microsoft.AspNetCore.Mvc;

namespace KLTN2025.Controllers
{
    public class TestController : Controller
    {
        private readonly EmailService _emailService;

        public TestController(EmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<IActionResult> Send()
        {
            await _emailService.SendEmailAsync("YOUR_EMAIL@gmail.com", "Test gửi mail", "<h3>Thành công!</h3>");
            return Content("✅ Đã gửi mail thử!");
        }
    }
}
