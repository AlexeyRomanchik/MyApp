using System.Linq;
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
    }
}
