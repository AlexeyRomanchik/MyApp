using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class PowerSupplyViewModel
    {
        public List<PowerSupply> PopularGoods { get; set; }
        public List<PowerSupply> NewItems { get; set; }
        public List<PowerSupply> PowerSupplies { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
