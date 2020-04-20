
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Interface
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MotherboardInterface> MotherboardInterfaces { get; set; }
        public List<PowerSupplyInterface> PowerSupplyInterfaces { get; set; }
    }
}
