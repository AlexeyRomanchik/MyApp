using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repository
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
                .AsNoTracking();
        }

        public override IQueryable<Cpu> FindByCondition(Expression<Func<Cpu, bool>> expression)
        {
            return RepositoryContext.Set<Cpu>()
                .Where(expression)
                .Include(x => x.Product)
                .Include("Product.Manufacturer")
                .Include("Product.Category")
                .AsNoTracking();
        }

    }
}
