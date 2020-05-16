using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IShoppingCartService _shoppingCartService;

        public OrderController(IHttpContextAccessor httpContextAccessor, IRepositoryWrapper repositoryWrapper, IMailingSystem mailingSystem, 
            UserManager<User> userManager, IShoppingCartService shoppingCartService)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _repositoryWrapper = repositoryWrapper;
            _mailingSystem = mailingSystem;
            _userManager = userManager;
            _shoppingCartService = shoppingCartService;
            _orderRepository = _repositoryWrapper.OrderRepository;
            _userRepository = _repositoryWrapper.UserRepository;
        }

        [HttpPost]
        public IActionResult Add(OrderViewModel orderViewModel)
        {
            orderViewModel.Order.Id = Guid.NewGuid();
            orderViewModel.Order.Date = DateTime.Now;
            orderViewModel.Order.Customer.Id = Guid.NewGuid();
            orderViewModel.Order.Cart = _httpContext.Session.Get<Cart>(CartSessionKey);
            orderViewModel.Order.Cart.FinalPrice = orderViewModel.Order.Cart.GetFinalPrice();
            orderViewModel.Order.Status = OrderStatus.Processed;

            _mailingSystem.OrderMessage(orderViewModel.Order);

            foreach (var cartItem in orderViewModel.Order.Cart.CartItems)
            {
                cartItem.Product = null;
            }

            var user = _userManager.GetUserAsync(User);
            if (user.Result != null)
            {
                user.Result.Orders.Add(orderViewModel.Order);
                _userRepository.Update(user.Result);
            }


            _orderRepository.Create(orderViewModel.Order);
            _repositoryWrapper.Save();

            _shoppingCartService.Clear();

            return View("SuccessfulOrder", orderViewModel.Order);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);

            User user;

            if (userId != null)
            {
                user = _userRepository
                           .FindByCondition(x => x.Id == userId)
                           .FirstOrDefault() ?? new User();
            }
            else
            {
                user = new User();
            }


            var orderViewModel = new OrderViewModel
            {
                Order = new Order
                {
                    Cart = _httpContext.Session.Get<Cart>(CartSessionKey),
                    Customer = new Customer()
                    {
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Address = new Address
                        {
                            City = user.Address.City,
                            HouseOrFlat = user.Address.HouseOrFlat,
                            Region = user.Address.Region,
                            Street = user.Address.Street
                        }
                        
                    }
                }
            };

            return View(orderViewModel);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Table()
        {
            var orders = _orderRepository.FindAll().ToList();

            return View(orders);
        }

    }
}