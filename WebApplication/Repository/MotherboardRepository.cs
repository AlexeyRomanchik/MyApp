using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class MotherboardRepository : RepositoryBase<Motherboard>, IMotherboardRepository
    {
        public MotherboardRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }

        public override IQueryable<Motherboard> FindAll()
        {
            return RepositoryContext.Set<Motherboard>().
                Include(x => x.Product)
                .ThenInclude(x => x.Manufacturer)
                .AsNoTracking();
        }

        public override IQueryable<Motherboard> FindByCondition(Expression<Func<Motherboard, bool>> expression)
        {
            return RepositoryContext.Set<Motherboard>()
                .Where(expression)
                .Include(x => x.Product)
                .Include("Product.Manufacturer")
                .Include("Product.Category")
                .Include("MotherboardInterfaces")
                .Include("MotherboardInterfaces.Interface")
                .Include("SocketType")
                .AsNoTracking();
        }

    }
}
