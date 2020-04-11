using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.BouncyCastle.Math.EC.Rfc7748;
using WebApplication.Models;

namespace WebApplication.Data.Configurations
{
    public class RamConfiguration : IEntityTypeConfiguration<Ram>
    {
        public void Configure(EntityTypeBuilder<Ram> builder)
        {
            builder.ToTable("Ram");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Product).
                WithOne(y => y.Ram).
                HasForeignKey("Ram");
        }
    }
}
