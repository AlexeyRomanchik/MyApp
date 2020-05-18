using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.User;

namespace DataProvider.Data.Configurations
{
    public class UserFavoriteProductsConfiguration : IEntityTypeConfiguration<UserFavoriteProducts>
    {
        public void Configure(EntityTypeBuilder<UserFavoriteProducts> builder)
        {
            builder.ToTable("UserFavoriteProducts");
            builder.HasKey(t => new { t.ProductId, t.UserId });

            builder.HasOne(x => x.Product)
                .WithMany(x => x.FavoriteProducts)
                .HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.FavoriteProducts)
                .HasForeignKey(x => x.UserId);
        }
    }
}
