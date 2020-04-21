using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class HddViewModel
    {
        public List<Hdd> PopularGoods { get; set; }
        public List<Hdd> NewItems { get; set; }
        public List<Hdd> HddList { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
