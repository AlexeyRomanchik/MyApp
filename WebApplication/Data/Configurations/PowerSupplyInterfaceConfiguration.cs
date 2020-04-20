using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication.Models;

namespace WebApplication.Data.Configurations
{
    public class PowerSupplyInterfaceConfiguration : IEntityTypeConfiguration<PowerSupplyInterface>
    {
        public void Configure(EntityTypeBuilder<PowerSupplyInterface> builder)
        {
            builder.ToTable("PowerSupplyInterface");
            builder.HasKey(t => new { t.InterfaceId, t.PowerSupplyId });

            builder.HasOne(x => x.PowerSupply)
                .WithMany(x => x.PowerSupplyInterfaces)
                .HasForeignKey(x => x.PowerSupplyId);

            builder.HasOne(x => x.Interface)
                .WithMany(x => x.PowerSupplyInterfaces)
                .HasForeignKey(x => x.InterfaceId);
        }
    }
}
