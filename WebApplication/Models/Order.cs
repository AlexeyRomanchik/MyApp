using System;
using Microsoft.VisualBasic;

namespace WebApplication.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateAndTime Date { get; set; }
        public OrderStatus Status { get; set; }
        public Cart Cart { get; set; }
        public Customer Customer { get; set; }
        public string Comment { get; set; }
    }
}
