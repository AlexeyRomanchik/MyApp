using System.Linq;
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
    }
}
