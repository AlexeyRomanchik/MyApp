using System;
using System.Linq;
using System.Linq.Expressions;
using DataProvider.Data;
using DataProvider.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Product;

namespace DataProvider.Repository
{
    public class CpuRepository : RepositoryBase<Cpu>, ICpuRepository
    {
        public CpuRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }

        public override IQueryable<Cpu> FindAll()
        {
            return RepositoryContext.Set<Cpu>().
                Include(x => x.Product)
                .ThenInclude(x => x.Manufacturer)
                .Include("Product.Ratings")
                .Include("Product.Reviews")
                .Include("Product.Reviews.User")
                .AsNoTracking();
        }

        public override IQueryable<Cpu> FindByCondition(Expression<Func<Cpu, bool>> expression)
        {
            return RepositoryContext.Set<Cpu>()
                .Where(expression)
                .Include(x => x.Product)
                .Include("Product.Manufacturer")
                .Include("Product.Category")
                .Include("Product.Ratings")
                .Include("Product.Ratings.User")
                .Include("Product.Reviews")
                .Include("Product.Reviews.User")
                .AsNoTracking();
        }

    }
}
