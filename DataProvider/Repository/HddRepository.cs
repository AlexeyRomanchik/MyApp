using System;
using System.Linq;
using System.Linq.Expressions;
using DataProvider.Data;
using DataProvider.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Product;

namespace DataProvider.Repository
{
    public class HddRepository : RepositoryBase<Hdd>, IHddRepository
    {
        public HddRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }

        public override IQueryable<Hdd> FindAll()
        {
            return RepositoryContext.Set<Hdd>().
                Include(x => x.Product)
                .ThenInclude(x => x.Manufacturer)
                .Include("Product.Ratings")
                .Include("Product.Reviews")
                .Include("Product.Reviews.User")
                .AsNoTracking();
        }

        public override IQueryable<Hdd> FindByCondition(Expression<Func<Hdd, bool>> expression)
        {
            return RepositoryContext.Set<Hdd>()
                .Where(expression)
                .Include(x => x.Product)
                .Include("Product.Manufacturer")
                .Include("Product.Category")
                .Include("Interface")
                .Include("Product.Ratings")
                .Include("Product.Ratings.User")
                .Include("Product.Reviews")
                .Include("Product.Reviews.User")
                .AsNoTracking();
        }
    }
}
