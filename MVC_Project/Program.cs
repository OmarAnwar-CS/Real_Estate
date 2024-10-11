using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_Project.API_Services;
using MVC_Project.Data;

namespace MVC_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();

            // Adding HttpClient
            builder.Services.AddHttpClient<IBase_API_Call, Base_API_Call>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:7197/api/"); // عنوان الـ API
            });

            // إضافة إعدادات الجلسة
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // وقت انتهاء الجلسة
                options.Cookie.HttpOnly = true; // يجعل الكوكيز غير قابلة للوصول عبر JavaScript
                options.Cookie.IsEssential = true; // يجعل الكوكيز ضرورية
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            // Configure the session middleware
            app.UseSession(); // تأكد من إضافة هذه السطر قبل استخدام التوجيه

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
