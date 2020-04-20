using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class MotherboardInterface
    {
        public Guid MotherboardId { get; set; }
        public Motherboard Motherboard { get; set; }

        public int InterfaceId { get; set; }
        public Interface Interface { get; set; }
    }
}
