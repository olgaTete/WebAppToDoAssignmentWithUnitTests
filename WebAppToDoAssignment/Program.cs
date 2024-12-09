using WebAppToDoAssignment.Models;
using WebAppToDoAssignment.Date;
//using Microsoft.EntityFrameworkCore;

namespace WebAppToDoAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            //builder.Services.AddDbContext<ExDbContext>(options
            //    => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
            //    new MySqlServerVersion(new Version(8, 0, 34))));
            
            builder.Services.AddSingleton<PeopleService>();
            builder.Services.AddSingleton<TodoService>();
            
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
