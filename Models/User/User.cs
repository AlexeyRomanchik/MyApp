using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Models.Product;

namespace Models.User
{
    public class User : IdentityUser
    {
        public List<Review> Reviews { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<Order.Order> Orders { get; set; }
        public List<UserFavoriteProducts> FavoriteProducts { get; set; }
        public UserAccountState UserAccountState { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Address Address { get; set; }
        public bool ReceiveProductNotifications { get; set; }
        public bool TwoFactorAuthenticationEnabled { get; set; }
        public string UserImageUrl { get; set; }
        public string VkLink { get; set; }
        public string TelegramLink { get; set; }
        public string InstagramLink { get; set; }
        public string TwitterLink{ get; set; }

        public User()
        {
            Orders = new List<Order.Order>();
            Reviews = new List<Review>();
            Ratings = new List<Rating>();
            Address = new Address();
            FavoriteProducts = new List<UserFavoriteProducts>();
        }
    }
}
