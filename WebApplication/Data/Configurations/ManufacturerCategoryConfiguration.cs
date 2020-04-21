using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication.Models;

namespace WebApplication.Data.Configurations
{
    public class ManufacturerCategoryConfiguration : IEntityTypeConfiguration<ManufacturerCategory>
    {
        public void Configure(EntityTypeBuilder<ManufacturerCategory> builder)
        {
            builder.ToTable("ManufacturerCategory");
            builder.HasKey(t => new { t.ManufacturerId, t.CategoryId });

            builder.HasOne(x => x.Manufacturer)
                .WithMany(x => x.ManufacturerCategories)
                .HasForeignKey(x => x.ManufacturerId);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.ManufacturerCategories)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
