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
