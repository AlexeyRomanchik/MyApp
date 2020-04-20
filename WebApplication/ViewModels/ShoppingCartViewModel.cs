using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
        public int ItemsCount { get; set; }
    }
}
