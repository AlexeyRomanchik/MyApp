using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication.Models;

namespace WebApplication.Data.Configurations
{
    public class CpuConfiguration : IEntityTypeConfiguration<Cpu>
    {
        public void Configure(EntityTypeBuilder<Cpu> builder)
        {
            builder.ToTable("Cpu");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Product)
                .WithOne()
                .HasForeignKey("Cpu");
        }
    }
}
