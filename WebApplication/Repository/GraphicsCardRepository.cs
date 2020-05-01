using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class GraphicsCardRepository : RepositoryBase<GraphicsCard>, IGraphicsCardRepository
    {
        public GraphicsCardRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }

        public override IQueryable<GraphicsCard> FindAll()
        {
            return RepositoryContext.Set<GraphicsCard>().
                Include(x => x.Product)
                .ThenInclude(x => x.Manufacturer)
                .Include("Product.Ratings")
                .AsNoTracking();
        }

        public override IQueryable<GraphicsCard> FindByCondition(Expression<Func<GraphicsCard, bool>> expression)
        {
            return RepositoryContext.Set<GraphicsCard>()
                .Where(expression)
                .Include(x => x.Product)
                .Include("Product.Manufacturer")
                .Include("Product.Category")
                .Include("Interface")
                .Include("MemoryType")
                .Include("Product.Ratings")
                .Include("Product.Ratings.User")
                .AsNoTracking();
        }
    }
}
