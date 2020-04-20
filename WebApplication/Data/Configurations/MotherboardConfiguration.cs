using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication.Models;

namespace WebApplication.Data.Configurations
{
    public class MotherboardConfiguration : IEntityTypeConfiguration<Motherboard>
    {
        public void Configure(EntityTypeBuilder<Motherboard> builder)
        {
            builder.ToTable("Motherboard");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Product)
                .WithOne()
                .HasForeignKey("Motherboard");
        }
    }
}
