using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Product;

namespace DataProvider.Data.Configurations
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.ToTable("Manufacturer");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(300).IsRequired();

            builder.HasMany(x => x.Products)
                .WithOne(y => y.Manufacturer)
                .HasForeignKey(t => t.ManufacturerId);
        }
    }
}
