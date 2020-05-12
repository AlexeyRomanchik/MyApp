using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication.Data;
using WebApplication.Extensions;
using WebApplication.Models;
using WebApplication.Services;
using WebApplication.SignalR;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureEmailService();
            services.ConfigureShoppingCartService();

            services.ConfigureMsSqlServerContext(Configuration);

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            services.ConfigureRepositoryWrapper();

            services.AddHttpContextAccessor();

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );


            services.AddSession();
            services.AddDistributedMemoryCache();

            services.AddAuthentication().
                AddGoogle(options =>
                {
                    options.ClientId = "425736496155-pfq97jct0gcbjm8kp2jlf41km06vfq2j.apps.googleusercontent.com";
                    options.ClientSecret = "wwjkpXvNLyICdwfklAVzp6RW";
                });

            services.AddAuthentication().
                AddFacebook(options =>
                {
                    options.ClientId = "266017901438969";
                    options.ClientSecret = "d41bc037d24cfd3beff9fca4b6210637";
                });

            services.ConfigureSortWrapper();

            services.ConfigureFileService();
            services.ConfigureFiltersService();
            services.ConfigureMailingSystem();
            services.AddSignalR();
            services.Configure<SmsOptions>(Configuration);
            services.ConfigureVerificationCodeGenerator();
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Api",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapControllerRoute(
                    name: "Admin",
                    pattern: "{area:exists}/{controller=Statistics}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}