using System.Collections.Generic;
using System.Linq;
using WebApplication.ViewModels;

namespace WebApplication.Contracts
{
    public interface IProductSortService<TProduct>
    {
        IQueryable<TProduct> SortBy(SortState sortState, IQueryable<TProduct> products);
    }
}