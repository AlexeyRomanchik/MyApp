using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Models
{
    public class User : IdentityUser
    {
        public List<Review> Reviews { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<Order> Orders { get; set; }
        public UserAccountState UserAccountState { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Address Address { get; set; }
        public bool ReceiveProductNotifications { get; set; }
        public string UserImageUrl { get; set; }

        public User()
        {
            Orders = new List<Order>();
            Reviews = new List<Review>();
            Ratings = new List<Rating>();
            Address = new Address();
        }
    }
}
