using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Product
{
    public class RamMemoryType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Type { get; set; }
        public List<Ram> Rams { get; set; }
    }
}
