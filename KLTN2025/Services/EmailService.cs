using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace KLTN2025.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // Đọc cấu hình từ appsettings.json
            string fromEmail = _configuration["EmailSettings:From"];
            string password = _configuration["EmailSettings:Password"];

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(fromEmail, password)
            };

            var message = new MailMessage
            {
                From = new MailAddress(fromEmail, "Hệ thống Gia Sư"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(toEmail);

            await smtp.SendMailAsync(message);
        }
    }
}
