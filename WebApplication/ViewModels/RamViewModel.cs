using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class RamViewModel
    {
        public List<Ram> PopularGoods { get; set; }
        public List<Ram> NewItems { get; set; }
        public List<Ram> Rams { get; set; } 
        public PageViewModel PageViewModel { get; set; }
    }
}
