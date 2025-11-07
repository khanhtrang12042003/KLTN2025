using System.Security.Cryptography;
using System.Text;


namespace KLTN2025.Services;


public static class Hashpassword
{
    public static string MaHoaMatKhau(string matKhau)
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
}