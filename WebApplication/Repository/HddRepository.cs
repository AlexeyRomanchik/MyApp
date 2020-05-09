using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repository
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
