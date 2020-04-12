using System.Collections.Generic;
using System.Linq;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.ViewModels
{
    public class RamViewModel
    {
        public List<Ram> Rams { get; set; } 
        public PageViewModel PageViewModel { get; set; }
    }
}
