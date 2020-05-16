using WebApplication.Contracts;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class UserFavoriteProductsRepository : RepositoryBase<UserFavoriteProducts>, IUserFavoriteProductsRepository
    {
        public UserFavoriteProductsRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
