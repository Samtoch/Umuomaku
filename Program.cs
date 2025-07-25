using Microsoft.EntityFrameworkCore;
using Umuomaku.Data;
using Umuomaku.Repositories;


namespace Umuomaku
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
                builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            }
            else
            {
                builder.Services.AddRazorPages(); // No runtime compilation in production
                builder.Services.AddControllersWithViews();
            }

            builder.Services.AddDbContext<UmuomakuDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 18))));

            builder.Services.AddScoped<IHighlightRepo, HighlightRepo>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
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
