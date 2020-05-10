using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data.Configurations;
using WebApplication.Models;

namespace WebApplication.Data
{
    public sealed class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RamConfiguration());
            builder.ApplyConfiguration(new ManufacturerConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new RatingConfiguration());
            builder.ApplyConfiguration(new ReviewConfiguration());
            builder.ApplyConfiguration(new CartConfiguration());
            builder.ApplyConfiguration(new CartItemConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new PowerSupplyConfiguration());
            builder.ApplyConfiguration(new GraphicsCardConfiguration());
            builder.ApplyConfiguration(new CpuConfiguration());
            builder.ApplyConfiguration(new MotherboardConfiguration());
            builder.ApplyConfiguration(new HddConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new AddressConfiguration());
            builder.ApplyConfiguration(new MotherboardInterfaceConfiguration());
            builder.ApplyConfiguration(new PowerSupplyInterfaceConfiguration());
            builder.ApplyConfiguration(new ManufacturerCategoryConfiguration());
            builder.ApplyConfiguration(new VerificationCodeConfiguration());
        }
    }
}
