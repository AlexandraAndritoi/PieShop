using PieShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;

namespace PieShop
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // MVC support
            services.AddControllersWithViews();

            // Razor pages support
            services.AddRazorPages();

            // EF Core support
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            // basic functionality for working with Identity in the application
            services.AddDefaultIdentity<IdentityUser>()
                // indicates that Identity needs to use Entity Framework to store its data
                // and is going to use AppDbContext which inherits from IdentityDbContext
                .AddEntityFrameworkStores<AppDbContext>();

            // session support
            services.AddHttpContextAccessor();
            services.AddSession();

            services.AddScoped<IPieRepository, PieRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped(serviceProvider => ShoppingCart.GetCart(serviceProvider));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // redirects HTTP requests to HTTPS
            app.UseHttpsRedirection();

            // images, JavaScript files, CSS files; searches in wwwroot
            app.UseStaticFiles();

            app.UseSession();

            // enabled MVC routing in our application
            app.UseRouting();

            // enabled ASP.NET Core Identity
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
