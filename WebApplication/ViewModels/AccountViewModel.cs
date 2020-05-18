using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Models.Order;
using Models.User;

namespace WebApplication.ViewModels
{
    public class AccountViewModel
    {
        public User User { get; set; }
        public List<Order> Orders { get; set; }
        public IFormFile UploadedFile { get; set; }
    }
}
