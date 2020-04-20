using System;

namespace WebApplication.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public OrderStatus Status { get; set; }
        public Cart Cart { get; set; }
        public Customer Customer { get; set; }
        public string Comment { get; set; }
    }
}
