using JustFunny.Database;
using JustFunny.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JustFunny
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
            // Add services to the dependency injection container.
            services.AddControllersWithViews();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<Fun_DbContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);
            services.AddSingleton<DbContext>(x => x.GetRequiredService<Fun_DbContext>());
            services.AddSingleton(x => x.GetRequiredService<Fun_DbContext>().Users);
            services.AddSingleton(x => x.GetRequiredService<Fun_DbContext>().Questions);

            services.AddSingleton<IDataService<User>>(x => new UserService(x.GetRequiredService<Fun_DbContext>(), x.GetRequiredService<Fun_DbContext>().Users));
            services.AddSingleton<IDataService<Question>>(x => new QuestionService(x.GetRequiredService<Fun_DbContext>(), x.GetRequiredService<Fun_DbContext>().Questions));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
