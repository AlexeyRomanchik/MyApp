using System;

namespace Models.User
{
    public class UserFavoriteProducts
    {
        public Guid ProductId { get; set; }
        public Product.Product Product { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
