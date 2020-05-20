using System;
using Logic.Services;
using Models.Order;
using Models.Product;
using Xunit;

namespace WebApplication.Tests
{
    public class ShoppingCartServiceTests
    {

        [Fact]
        public void AddItem_Returned_True()
        {
            var shoppingCartService = new ShoppingCartService {Cart = new Cart()};

            shoppingCartService.AddItem(new Product(), 1);

            Assert.True(shoppingCartService.Cart.CartItems.Count > 0);
        }

        [Fact]
        public void RemoveItem_Returned_True()
        {
            var product = new Product()
            {
                Id = new Guid()
            };

            var shoppingCartService = new ShoppingCartService { Cart = new Cart() };

            shoppingCartService.RemoveItem(product, 1);

            Assert.True(shoppingCartService.Cart.CartItems.Count == 0);
        }

        [Fact]
        public void RemoveLine_Returned_True()
        {
            var id = new Guid();
            var shoppingCartService = new ShoppingCartService { Cart = new Cart() };
            shoppingCartService.AddItem(new Product() { Id = id }, 10);

            shoppingCartService.RemoveLine(new Product() { Id = id });

            Assert.True(shoppingCartService.Cart.CartItems.Count == 0);
        }

        [Fact]
        public void CartClear_Returned_True()
        {
            var product = new Product()
            {
                Id = new Guid()
            };

            var shoppingCartService = new ShoppingCartService { Cart = new Cart() };

            shoppingCartService.AddItem(product, 10);

            shoppingCartService.Clear();

            Assert.True(shoppingCartService.Cart.CartItems.Count == 0);
        }

    }
}
