using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Models.Context;
using ProductCatalog.RepoServices;

namespace ProductCatalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("ProductCatalogContextConnection") ?? throw new InvalidOperationException("Connection string 'ProductCatalogContextConnection' not found.");

            builder.Services.AddDbContext<ProductCatalogContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ProductCatalogContext>().AddDefaultUI().AddRoles<IdentityRole>()
                .AddDefaultTokenProviders();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IProductRepository, ProductRepoService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepoService>();
          

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
               name: "area",
               pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
               );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}