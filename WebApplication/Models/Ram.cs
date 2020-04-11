using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Ram
    {
        public Guid Id { get; set; }
        public int RamMemoryTypeId { get; set; }
        public string TotalMemory { get; set; }
        public double Frequency { get; set; }
        public int ContactsNumber { get; set; }
        public double Throughput { get; set; }
        public double SupplyVoltage { get; set; }
        public Guid ProductId { get; set; }

        public RamMemoryType MemoryType { get; set; }
        public Product Product { get; set; }
    }
}
