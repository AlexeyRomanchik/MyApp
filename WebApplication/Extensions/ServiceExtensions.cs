using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication.Contracts;
using WebApplication.Contracts.FiltersContracts;
using WebApplication.Contracts.SortContracts;
using WebApplication.Data;
using WebApplication.Repository;
using WebApplication.Services;
using WebApplication.Services.Filters;
using WebApplication.Services.SortServices;

namespace WebApplication.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureMsSqlServerContext(this IServiceCollection services, IConfiguration config)
        {
            const string connectionString = "DefaultConnection";

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(config.GetConnectionString(connectionString)));
        }

        public static void ConfigureEmailService(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
        }

        public static void ConfigureMailingSystem(this IServiceCollection services)
        {
            services.AddTransient<IMailingSystem, MailingSystem>();
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureShoppingCartService(this IServiceCollection services)
        {
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
        }

        public static void ConfigureSortWrapper(this IServiceCollection services)
        {
            services.AddScoped<ISortServiceWrapper, SortServiceWrapper>();
        }

        public static void ConfigureFileService(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
        }

        public static void ConfigureVerificationCodeGenerator(this IServiceCollection services)
        {
            services.AddScoped<IVerificationCodeGenerator, VerificationCodeGenerator>();
        }

        public static void ConfigureFiltersService(this IServiceCollection services)
        {
            services.AddScoped<IRamFilter, RamFilter>();
            services.AddScoped<ICpuFilter, CpuFilter>();
            services.AddScoped<IPowerSupplyFilter, PowerSupplyFilter>();
            services.AddScoped<IMotherboardFilter, MotherboardFilter>();
            services.AddScoped<IHddFilter, HddFilter>();
            services.AddScoped<IGraphicsCardFilter, GraphicsCardFilter>();
        }

    }
}
