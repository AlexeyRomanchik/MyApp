using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication.Models;

namespace WebApplication.Data.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItem");
            builder.HasKey(x => new{x.CartId, x.ProductId});

            builder.HasOne(x => x.Cart)
                .WithMany(t => t.CartItems)
                .HasForeignKey(y => y.CartId);
        }
    }
}
