using DataProvider.Data;
using DataProvider.Interfaces;
using Models.Product;

namespace DataProvider.Repository
{
    public class ManufacturerRepository : RepositoryBase<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
