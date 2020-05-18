using System.Linq;
using DataProvider.Data;
using DataProvider.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Order;

namespace DataProvider.Repository
{
    public class CartItemRepository : RepositoryBase<CartItem>, ICartItemRepository
    {
        public CartItemRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }
        public override IQueryable<CartItem> FindAll()
        {
            return RepositoryContext.Set<CartItem>()
                .Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .AsNoTracking();
        }
    }
}
