using DataProvider.Data;
using DataProvider.Interfaces;
using Models.Product;

namespace DataProvider.Repository
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
