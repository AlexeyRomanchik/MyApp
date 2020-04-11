using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int QuantityInStock { get; set; }
        public string ImageUrl { get; set; }
        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }
        public List<Rating> Ratings { get; set; }

        public bool IsAvailable() => 0 < QuantityInStock;
    }
}
