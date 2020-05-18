using System;
using System.Linq;
using Models.Order;

namespace DataProvider.Interfaces
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        IQueryable<IGrouping<DateTime, Order>> FindAllGroupBy();
    }
}