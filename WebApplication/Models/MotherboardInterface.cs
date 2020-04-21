using System;

namespace WebApplication.Models
{
    public class MotherboardInterface
    {
        public Guid MotherboardId { get; set; }
        public Motherboard Motherboard { get; set; }

        public int InterfaceId { get; set; }
        public Interface Interface { get; set; }
        public int Quantity { get; set; }
    }
}
