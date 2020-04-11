using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Rating
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public Guid ProductId { get; set; }
        public string UserId { get; set; }
    }
}
