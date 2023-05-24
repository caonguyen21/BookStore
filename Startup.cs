using BookStore.Models;
using BookStore.Security;
using Microsoft.EntityFrameworkCore;

namespace BookStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<BookStoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppDb")));

            services.AddControllersWithViews();
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppDb")));

            services.AddIdentity<AppIdentityEmployee, AppIdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/Employee/SignIn";
                    options.AccessDeniedPath = "/Employee/AccessDenied";
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=BookManage}/{action=Index}/{id?}");
            });
        }
    }
}
