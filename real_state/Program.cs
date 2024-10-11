using _Services.Contracts;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using MVC_Project.API_Services;
using System.Net.Http.Headers;

namespace real_state
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            
            // Adding HttpClient
            builder.Services.AddHttpClient<IBase_API_Call, Base_API_Call>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:7197/api/"); // استبدل رابط الـ API الخاص بك هنا
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // إضافة هذه السطر لرؤية الأخطاء بشكل أكثر تفصيلاً
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "property",
                pattern: "{controller=Home}/{action=Index}");

            app.Run();
        }
    }
}
