using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }

        public override IQueryable<Product> FindByCondition(Expression<Func<Product, bool>> expression)
        {
            return RepositoryContext.Set<Product>()
                .Where(expression)
                .Include(x => x.Manufacturer)
                .Include(x => x.Ratings)
                .Include(x => x.Reviews)
                .Include("Review.User")
                .AsNoTracking();
        }

    }
}
