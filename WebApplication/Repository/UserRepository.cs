using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }

        public override IQueryable<User> FindByCondition(Expression<Func<User, bool>> expression)
        {
            return RepositoryContext.Set<User>()
                .Where(expression)
                .Include(x => x.Address)
                .Include(x => x.Orders)
                .Include(x => x.Ratings)
                .Include(x => x.Reviews)
                .Include(x => x.FavoriteProducts)
                .ThenInclude(x => x.Product)
                .Include("FavoriteProducts.Product.Manufacturer")
                .Include("Ratings.Product")
                .Include("Ratings.Product.Manufacturer")
                .Include("Ratings.Product.Category")
                .Include("Orders.Cart")
                .Include("Orders.Cart.CartItems")
                .Include("Orders.Cart.CartItems.Product")
                .AsNoTracking();
        }
    }
}
