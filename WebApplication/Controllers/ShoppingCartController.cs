using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Contracts;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductRepository _productRepository;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IProductRepository productRepository)
        {
            _shoppingCartService = shoppingCartService;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var cart = _shoppingCartService.GetCart(HttpContext);

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                CartItems = cart.CartItems.ToList(),
                ItemsCount = cart.GetTotalItemsCount(),
                TotalPrice = cart.GetFinalPrice()
            };

            return View(shoppingCartViewModel);
        }

        public ActionResult AddToCart(Guid id)
        {
            var product = _productRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            if(product != null)
                _shoppingCartService.AddItem(product, 1);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(Guid id)
        {
            var product = _productRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            if (product != null)
                _shoppingCartService.RemoveLine(product);

            return RedirectToAction("Index");
        }

    }
}