using Microsoft.EntityFrameworkCore;
using KLTN2025.Data;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Đăng ký dịch vụ (trước khi Build)
builder.Services.AddDbContext<KLTNContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KLTNConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor(); // ✅ Thêm dòng này


// ✅ Cấu hình Session ở đây
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // thời gian lưu session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// 2️⃣ Cấu hình pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 3️⃣ Kích hoạt Session (sau UseRouting, trước Authorization)
app.UseSession();

app.UseAuthorization();

// 4️⃣ Cấu hình route mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
