using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class CpuViewModel
    {
        public List<Cpu> PopularGoods { get; set; }
        public List<Cpu> NewItems { get; set; }
        public List<Cpu> CpuList { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
