using System;
using System.Linq;
using System.Linq.Expressions;
using DataProvider.Data;
using DataProvider.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Product;

namespace DataProvider.Repository
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
                .Include(x => x.Category)
                .AsNoTracking();
        }

    }
}
