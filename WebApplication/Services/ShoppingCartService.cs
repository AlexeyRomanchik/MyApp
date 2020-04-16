using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using WebApplication.Contracts;
using WebApplication.Models;
using WebApplication.Extensions;

namespace WebApplication.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private const string CartSessionKey = "CartSessionKey";
        public Cart Cart { get; }
        public IEnumerable<CartItem> Items => Cart.CartItems;

        public ShoppingCartService(HttpContext context)
        {
            Cart = GetCart(context);
        }

        public void AddItem(Product product, int quantity)
        {
            var cartItem = Cart.CartItems
                .FirstOrDefault(x => x.ProductId == product.Id);

            if (cartItem == null)
            {
                Cart.CartItems.Add(new CartItem
                {
                    ProductId = product.Id,
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                cartItem.Quantity += quantity;
            }
        }

        public void Clear()
        {
            Cart.CartItems.Clear();
        }

        public void RemoveLine(Product product)
        {
            Cart.CartItems.RemoveAll(x => x.ProductId == product.Id);
        }

        public Cart GetCart(HttpContext context)
        {
            var cart = context.Session.Get<Cart>(CartSessionKey);
            if (cart != null) return cart;

            cart = new Cart();
            context.Session.Set(CartSessionKey, cart);

            return cart;
        }
    }
}
