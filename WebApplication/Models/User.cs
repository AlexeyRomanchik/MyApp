using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WebApplication.Models
{
    public class User : IdentityUser
    {
        public List<Review> Reviews { get; set; }
        public List<Rating> Ratings { get; set; }
    }
}
