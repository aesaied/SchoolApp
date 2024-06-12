using Microsoft.EntityFrameworkCore;
using SchoolApp.Data;

namespace SchoolApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connStr = builder.Configuration.GetConnectionString("SchoolApp");
            builder.Services.AddDbContext<SchoolContext>(options => {

                options.UseSqlServer(connStr);
            });

            builder.Services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 10;
              options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
              
            }).AddEntityFrameworkStores<SchoolContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
