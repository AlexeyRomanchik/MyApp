using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using WebApplication.Models;

namespace WebApplication.Contracts
{
    public interface IShoppingCartService
    {
        Cart Cart { get; }
        IEnumerable<CartItem> Items { get; }
        void AddItem(Product product, int quantity);
        void Clear();
        void RemoveLine(Product product);
        Cart GetCart(HttpContext context);
    }
}