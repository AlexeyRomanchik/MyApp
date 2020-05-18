using System;
using System.Linq;
using System.Linq.Expressions;
using DataProvider.Data;
using DataProvider.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Product;

namespace DataProvider.Repository
{
    public class RamRepository : RepositoryBase<Ram>, IRamRepository
    {
        public RamRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }

        public override IQueryable<Ram> FindAll()
        {
            return RepositoryContext.Set<Ram>().
                Include(x => x.Product)
                .Include("Product.Manufacturer")
                .Include("Product.Category")
                .Include("MemoryType")
                .Include("Product.Ratings")
                .Include("Product.Reviews")
                .Include("Product.Reviews.User")
                .AsNoTracking();
        }

        public override IQueryable<Ram> FindByCondition(Expression<Func<Ram, bool>> expression)
        {
            return RepositoryContext.Set<Ram>()
                .Where(expression)
                .Include(x => x.Product)
                .Include("Product.Manufacturer")
                .Include("Product.Category")
                .Include("MemoryType")
                .Include("Product.Ratings")
                .Include("Product.Ratings.User")
                .Include("Product.Reviews")
                .Include("Product.Reviews.User")
                .AsNoTracking();
        }
    }
}
