using System.Linq;
using WebApplication.ViewModels;

namespace WebApplication.Contracts.SortContracts
{
    public interface IProductSortService<TProduct>
    {
        IQueryable<TProduct> SortBy(SortState sortState, IQueryable<TProduct> products);
    }
}