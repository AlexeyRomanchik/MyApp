﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Product;

namespace DataProvider.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal(15,2)");


            builder.HasMany(x => x.Ratings)
                .WithOne(t => t.Product)
                .HasForeignKey(y => y.ProductId);

            builder.HasOne(x => x.Category)
                .WithMany(t => t.Products)
                .HasForeignKey(y => y.CategoryId);


        }
    }
}
