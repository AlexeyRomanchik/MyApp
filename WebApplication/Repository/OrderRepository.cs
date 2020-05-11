using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }

        public override IQueryable<Order> FindAll()
        {
            return RepositoryContext.Set<Order>()
                .Include("Customer")
                .Include("Customer.Address")
                .AsNoTracking();
        }
    }
}
