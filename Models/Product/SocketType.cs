using System.Collections.Generic;

namespace Models.Product
{
    public class SocketType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public List<Cpu> CpuList { get; set; }
    }
}
