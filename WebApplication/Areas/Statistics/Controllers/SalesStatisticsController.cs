using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math.EC.Rfc7748;
using WebApplication.Contracts;
using WebApplication.Models;

namespace WebApplication.Areas.Statistics.Controllers
{
    [Area("Statistics")]
    public class SalesStatisticsController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public SalesStatisticsController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
      
        public JsonResult GetProductsSoldByDay()
        {
            var orders = _repositoryWrapper.OrderRepository.FindAll().ToList();

            var ordersGroupBy = orders.GroupBy(x => x.Date.Date);

            var groupBy = ordersGroupBy as IGrouping<DateTime, Order>[] ?? ordersGroupBy.ToArray();
            var soldProducts = new object[groupBy.Count()];

            var index = 0;
            foreach (var order in groupBy)
            {
                var productsCount = order.Sum(products => products.Cart.FinalPrice);
                soldProducts[index] = new { date = order.Key.Date, value = productsCount };
                index++;
            }
            return Json(soldProducts);
        }
        
        

        public JsonResult GetQuantityProductsSoldByType()
        {
            return GetProductsStatisticsByType(x => x.Quantity);
        }

        public JsonResult GetPriceProductsSoldByType()
        {
            return GetProductsStatisticsByType(x => x.Product.Price);
        }

        private JsonResult GetProductsStatisticsByType(Func<CartItem, decimal> selector)
        {
            var items = _repositoryWrapper.CartItemRepository.FindAll().ToList();

            var itemByCategory = items.GroupBy(x => x.Product.Category.Name).ToList();

            var quantityProductsSoldByType = new object[itemByCategory.Count()];

            var index = 0;
            foreach (var item in itemByCategory)
            {
                quantityProductsSoldByType[index] = new { category = item.Key, quantity = item.Sum(selector) };
                index++;
            }
            return Json(quantityProductsSoldByType);
        }


    }
}