using System.Linq;
using WebApplication.ViewModels;

namespace WebApplication.Interfaces.SortContracts
{
    public interface IProductSortService<TProduct>
    {
        IQueryable<TProduct> SortBy(SortState sortState, IQueryable<TProduct> products);
    }
}