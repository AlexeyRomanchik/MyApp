using System;
using System.Linq;
using WebApplication.Models;

namespace WebApplication.Contracts
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        IQueryable<IGrouping<DateTime, Order>> FindAllGroupBy();
    }
}