using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication.Models;

namespace WebApplication.Data.Configurations
{
    public class GraphicsCardConfiguration : IEntityTypeConfiguration<GraphicsCard>
    {
        public void Configure(EntityTypeBuilder<GraphicsCard> builder)
        {
            builder.ToTable("GraphicsCard");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Product)
                .WithOne()
                .HasForeignKey("GraphicsCard");
        }
    }
}
