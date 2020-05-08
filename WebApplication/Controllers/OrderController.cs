using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Contracts;
using WebApplication.Models;
using WebApplication.Extensions;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class OrderController : Controller
    {
        private const string CartSessionKey = "CartSessionKey";
        private readonly HttpContext _httpContext;
        private readonly IOrderRepository _orderRepository;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMailingSystem _mailingSystem;

        public OrderController(IHttpContextAccessor httpContextAccessor, IRepositoryWrapper repositoryWrapper, IMailingSystem mailingSystem)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _repositoryWrapper = repositoryWrapper;
            _mailingSystem = mailingSystem;
            _orderRepository = _repositoryWrapper.OrderRepository;
        }

        [HttpPost]
        public IActionResult Add(OrderViewModel orderViewModel)
        {
            orderViewModel.Order.Id = Guid.NewGuid();
            orderViewModel.Order.Date = DateTime.Now;
            orderViewModel.Order.Customer.Id = Guid.NewGuid();
            orderViewModel.Order.Cart = _httpContext.Session.Get<Cart>(CartSessionKey);
            orderViewModel.Order.Cart.FinalPrice = orderViewModel.Order.Cart.GetFinalPrice();

            _mailingSystem.OrderMessage(orderViewModel.Order);

            foreach (var cartItem in orderViewModel.Order.Cart.CartItems)
            {
                cartItem.Product = null;
            }

            _orderRepository.Create(orderViewModel.Order);
            _repositoryWrapper.Save();


            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var orderViewModel = new OrderViewModel
            {
                Order = new Order
                {
                    Cart = _httpContext.Session.Get<Cart>(CartSessionKey),
                    Customer = new Customer()
                }
            };

            return View(orderViewModel);
        }

    }
}