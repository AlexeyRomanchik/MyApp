using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Product;

namespace DataProvider.Data.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PublicationDate).HasDefaultValue(DateTime.Now);

            builder.HasOne(x => x.User)
                .WithMany(y => y.Reviews)
                .HasForeignKey(t => t.UserId);

            builder.HasOne(x => x.Product)
                .WithMany(y => y.Reviews)
                .HasForeignKey(t => t.ProductId);
        }
    }
}
