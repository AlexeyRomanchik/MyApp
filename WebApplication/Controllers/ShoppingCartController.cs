using System;
using System.Linq;
using DataProvider.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Order;
using WebApplication.Extensions;
using WebApplication.Interfaces;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class ShoppingCartController : Controller
    {
        private const string CartSessionKey = "CartSessionKey";
        private readonly HttpContext _httpContext;

        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IRepositoryWrapper repositoryWrapper, IHttpContextAccessor httpContextAccessor,
            IShoppingCartService shoppingCartService)
        {
            _repositoryWrapper = repositoryWrapper;
            _shoppingCartService = shoppingCartService;
            _httpContext = httpContextAccessor.HttpContext;
            _shoppingCartService.Cart = GetCart();
        }

        public IActionResult Index()
        {
            var cart = _shoppingCartService.Cart;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                CartItems = cart.CartItems.ToList(),
                ItemsCount = cart.GetTotalItemsCount(),
                TotalPrice = cart.GetFinalPrice()
            };

            return View(shoppingCartViewModel);
        }

        [HttpPost]
        public IActionResult AddToCart(Guid id)
        {
            var product = _repositoryWrapper.ProductRepository
                .FindByCondition(x => x.Id == id).FirstOrDefault();

            if (product != null)
                _shoppingCartService.AddItem(product, 1);

            SaveCart(_shoppingCartService.Cart);

            return Json(_shoppingCartService.Cart.GetTotalItemsCount());
        }

        public IActionResult DeleteProductFromLine(Guid id)
        {
            var product = _repositoryWrapper.ProductRepository
                .FindByCondition(x => x.Id == id).FirstOrDefault();

            if (product != null)
                _shoppingCartService.RemoveItem(product, 1);

            SaveCart(_shoppingCartService.Cart);

            return RedirectToAction("Index");
        }

        public IActionResult AddProductToLine(Guid id)
        {
            var product = _repositoryWrapper.ProductRepository
                .FindByCondition(x => x.Id == id).FirstOrDefault();

            if (product != null)
                _shoppingCartService.AddItem(product, 1);

            SaveCart(_shoppingCartService.Cart);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(Guid id)
        {
            var product = _repositoryWrapper.ProductRepository
                .FindByCondition(x => x.Id == id).FirstOrDefault();

            if (product != null)
                _shoppingCartService.RemoveLine(product);

            SaveCart(_shoppingCartService.Cart);

            return RedirectToAction("Index");
        }

        public IActionResult GetCountItems()
        {
            return Json( _shoppingCartService.Cart.GetTotalItemsCount());
        }

        private Cart GetCart()
        {
            var cart = _httpContext.Session.Get<Cart>(CartSessionKey);

            if (cart != null) return cart;

            cart = new Cart();
            _httpContext.Session.Set(CartSessionKey, cart);

            return cart;
        }

        private void SaveCart(Cart cart)
        {
            _httpContext.Session.Set(CartSessionKey, cart);
        }
    }
}