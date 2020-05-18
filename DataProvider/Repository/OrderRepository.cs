using System;
using System.Linq;
using System.Linq.Expressions;
using DataProvider.Data;
using DataProvider.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Order;

namespace DataProvider.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }

        public IQueryable<IGrouping<DateTime, Order>> FindAllGroupBy()
        {
            return RepositoryContext.Set<Order>().GroupBy(x => x.Date)
                .AsNoTracking();
        }

        public override IQueryable<Order> FindAll()
        {
            return RepositoryContext.Set<Order>()
                .Include("Customer")
                .Include("Customer.Address")
                .Include("Cart")
                .Include("Cart.CartItems")
                .Include("Cart.CartItems.Product")
                .AsNoTracking();
        }

        public override IQueryable<Order> FindByCondition(Expression<Func<Order, bool>> expression)
        {
            return RepositoryContext.Set<Order>()
                .Include("Customer")
                .Include("Customer.Address")
                .Include("Cart")
                .Include("Cart.CartItems")
                .Include("Cart.CartItems.Product")
                .AsNoTracking();
        }

    }
}
