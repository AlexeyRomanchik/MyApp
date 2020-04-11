using System.Linq;
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
                Include(x => x.Product).
                ThenInclude(x => x.Manufacturer)
                .AsNoTracking();
        }
    }
}
