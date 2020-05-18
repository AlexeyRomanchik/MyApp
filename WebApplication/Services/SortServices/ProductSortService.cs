using System.Linq;
using WebApplication.Interfaces.SortContracts;
using WebApplication.ViewModels;

namespace WebApplication.Services.SortServices
{
    public abstract class ProductSortService<TProduct> : IProductSortService<TProduct>
    {
        public abstract IQueryable<TProduct> SortBy(SortState sortState, IQueryable<TProduct> products);
    }
}
