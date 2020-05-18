using DataProvider.Data;
using DataProvider.Interfaces;
using Models.Product;

namespace DataProvider.Repository
{
    public class RatingRepository : RepositoryBase<Rating>, IRatingRepository
    {
        public RatingRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
