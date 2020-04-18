using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication.Models;

namespace WebApplication.Data.Configurations
{
    public class PowerSupplyConfiguration : IEntityTypeConfiguration<PowerSupply>
    {
        public void Configure(EntityTypeBuilder<PowerSupply> builder)
        {
            builder.ToTable("PowerSupply");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Product)
                .WithOne()
                .HasForeignKey("PowerSupply");
        }
    }
}
