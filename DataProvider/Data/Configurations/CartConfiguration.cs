using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Order;

namespace DataProvider.Data.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Order)
                .WithOne(x => x.Cart)
                .HasForeignKey("Cart")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
