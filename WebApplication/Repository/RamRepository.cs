using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repository
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
                .AsNoTracking();
        }
    }
}
