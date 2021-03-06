﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Product;

namespace DataProvider.Data.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Rating");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                .WithMany(y => y.Ratings)
                .HasForeignKey(t => t.UserId);
        }
    }
}
