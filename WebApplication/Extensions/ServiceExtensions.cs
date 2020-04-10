using Microsoft.Extensions.DependencyInjection;
using WebApplication.Contracts;
using WebApplication.Services;

namespace WebApplication.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureEmailService(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
