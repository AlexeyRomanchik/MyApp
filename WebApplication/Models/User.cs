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
        public UserSettings UserSettings { get; set; }
        public bool PhoneNumberVerified { get; set; }
        public bool UserSettingsFilled { get; set; }

        public User()
        {
            Reviews = new List<Review>();
            Ratings = new List<Rating>();
            UserSettings = new UserSettings();
        }
    }
}
