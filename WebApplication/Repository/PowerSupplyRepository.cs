using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class PowerSupplyRepository : RepositoryBase<PowerSupply>, IPowerSupplyRepository
    {
        public PowerSupplyRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }

        public override IQueryable<PowerSupply> FindAll()
        {
            return RepositoryContext.Set<PowerSupply>().
                Include(x => x.Product)
                .ThenInclude(x => x.Manufacturer)
                .AsNoTracking();
        }
    }
}
