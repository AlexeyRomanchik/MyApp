using System;
using System.Linq;
using Models.Product;
using WebApplication.Interfaces.SortContracts;
using WebApplication.ViewModels;

namespace WebApplication.Services.SortServices
{
    public class CpuSortService : ProductSortService<Cpu>, ICpuSortService
    {
        public override IQueryable<Cpu> SortBy(SortState sortState, IQueryable<Cpu> products)
        {
            switch (sortState)
            {
                case SortState.PriceAsc:
                    return products.OrderBy(x => x.Product.Price);
                case SortState.PriceDesc:
                    return products.OrderByDescending(x => x.Product.Price);
                case SortState.NameAsc:
                    return products.OrderBy(x => x.Product.Name);
                case SortState.NameDesc:
                    return products.OrderByDescending(x => x.Product.Name);
                case SortState.DateAddedAsc:
                    return products.OrderBy(x => x.Product.DateAdded);
                case SortState.DateAddedDesc:
                    return products.OrderByDescending(x => x.Product.DateAdded);
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortState), sortState, null);
            }
        }
    }
}
