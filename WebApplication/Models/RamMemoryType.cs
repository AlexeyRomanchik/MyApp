using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class RamMemoryType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public List<Ram> Rams { get; set; }
    }
}
