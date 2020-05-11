using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class AccountViewModel
    {
        public User User { get; set; }
        public List<Order> Orders { get; set; }
        public IFormFile UploadedFile { get; set; }
    }
}
