using DataProvider.Data;
using DataProvider.Interfaces;
using Models.User;

namespace DataProvider.Repository
{
    public class UserFavoriteProductsRepository : RepositoryBase<UserFavoriteProducts>, IUserFavoriteProductsRepository
    {
        public UserFavoriteProductsRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
