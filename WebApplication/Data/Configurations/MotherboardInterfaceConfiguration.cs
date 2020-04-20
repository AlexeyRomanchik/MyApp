using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication.Models;

namespace WebApplication.Data.Configurations
{
    public class MotherboardInterfaceConfiguration : IEntityTypeConfiguration<MotherboardInterface>
    {
        public void Configure(EntityTypeBuilder<MotherboardInterface> builder)
        {
            builder.ToTable("MotherboardInterface");
            builder.HasKey(t => new { t.InterfaceId, t.MotherboardId });

            builder.HasOne(x => x.Interface)
                .WithMany(x => x.MotherboardInterfaces)
                .HasForeignKey(x => x.InterfaceId);

            builder.HasOne(x => x.Motherboard)
                .WithMany(x => x.MotherboardInterfaces)
                .HasForeignKey(x => x.MotherboardId);
        }
    }
}
