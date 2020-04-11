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
    }
}
