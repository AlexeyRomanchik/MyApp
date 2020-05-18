using System;
using System.Linq;
using DataProvider.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Order;
using WebApplication.Areas.Admin.ViewModels;

namespace WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public OrdersController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _orderRepository = repositoryWrapper.OrderRepository;
        }

        public IActionResult Index()
        {
            var orders = _orderRepository.FindAll();

            var ordersViewModel = new OrdersViewModel
            {
                Orders = orders.ToList()
            };

            return View(ordersViewModel);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var order = _orderRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            return View(order);
        }

        [HttpPost]
        public IActionResult Edit(Order order)
        {
            _orderRepository.Update(order);
            _repositoryWrapper.Save();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            var order = _orderRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            order.Cart.CartItems = null;

            _orderRepository.Delete(order);
            _repositoryWrapper.Save();

            return RedirectToAction("Index");
        }
    }
}