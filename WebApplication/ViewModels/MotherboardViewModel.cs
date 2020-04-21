using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class MotherboardViewModel
    {
        public List<Motherboard> PopularGoods { get; set; }
        public List<Motherboard> NewItems { get; set; }
        public List<Motherboard> Motherboards { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
