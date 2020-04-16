using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Contracts;
using WebApplication.Extensions;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class ShoppingCartController : Controller
    {
        private const string CartSessionKey = "CartSessionKey";

        private readonly IShoppingCartService _shoppingCartService;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IRepositoryWrapper repositoryWrapper, IHttpContextAccessor httpContextAccessor)
        {
            _shoppingCartService = shoppingCartService;
            _repositoryWrapper = repositoryWrapper;
            _httpContextAccessor = httpContextAccessor;
            _shoppingCartService.Cart = GetCart(httpContextAccessor.HttpContext);
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
        public ActionResult AddToCart(Guid id)
        {
            var product = _repositoryWrapper.ProductRepository
                .FindByCondition(x => x.Id == id).FirstOrDefault();

            if(product != null)
                _shoppingCartService.AddItem(product, 1);

            _httpContextAccessor.HttpContext.Session.Set<Cart>(CartSessionKey, _shoppingCartService.Cart);

            return Json(_shoppingCartService.Cart.GetTotalItemsCount());

        }

        public ActionResult RemoveFromCart(Guid id)
        {
            var product = _repositoryWrapper.ProductRepository
                .FindByCondition(x => x.Id == id).FirstOrDefault();

            if (product != null)
                _shoppingCartService.RemoveLine(product);

            return RedirectToAction("Index");
        }

        private static Cart GetCart(HttpContext context)
        {
            var cart = context.Session.Get<Cart>(CartSessionKey);

            if (cart != null) return cart;

            cart = new Cart();
            context.Session.Set<Cart>(CartSessionKey, cart);

            return cart;
        }

    }
}