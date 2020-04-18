using System.Linq;
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
                .AsNoTracking();
        }
    }
}
