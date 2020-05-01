using WebApplication.Contracts;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class RatingRepository : RepositoryBase<Rating>, IRatingRepository
    {
        public RatingRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
