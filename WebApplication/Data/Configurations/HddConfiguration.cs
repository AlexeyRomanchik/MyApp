using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication.Models;

namespace WebApplication.Data.Configurations
{
    public class HddConfiguration : IEntityTypeConfiguration<Hdd>
    {
        public void Configure(EntityTypeBuilder<Hdd> builder)
        {
            builder.ToTable("Hdd");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Product)
                .WithOne()
                .HasForeignKey("Hdd");
        }
    }
}
