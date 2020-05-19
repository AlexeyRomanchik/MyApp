using System.Collections.Generic;
using Models.Order;
using Models.Product;

namespace Logic.Interfaces
{
    public interface IShoppingCartService
    {
        Cart Cart { get; set; }
        IEnumerable<CartItem> Items { get; }
        void AddItem(Product product, int quantity);
        void RemoveItem(Product product, int quantity);
        void Clear();
        void RemoveLine(Product product);
    }
}