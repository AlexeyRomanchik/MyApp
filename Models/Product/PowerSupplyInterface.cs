using System;

namespace Models.Product
{
    public class PowerSupplyInterface
    {
        public Guid PowerSupplyId { get; set; }
        public PowerSupply PowerSupply { get; set; }

        public int InterfaceId { get; set; }
        public Interface Interface { get; set; }
    }
}
