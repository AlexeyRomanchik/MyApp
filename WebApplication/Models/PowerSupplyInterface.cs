using System;

namespace WebApplication.Models
{
    public class PowerSupplyInterface
    {
        public Guid PowerSupplyId { get; set; }
        public PowerSupply PowerSupply { get; set; }

        public int InterfaceId { get; set; }
        public Interface Interface { get; set; }
    }
}
