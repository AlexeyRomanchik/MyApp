using System.Linq;
using WebApplication.Contracts.FiltersContracts;
using WebApplication.Models;
using WebApplication.ViewModels.FilterViewModels;

namespace WebApplication.Services.Filters
{
    public class GraphicsCardFilter : IGraphicsCardFilter
    {
        public IQueryable<GraphicsCard> ApplyBaseFilter(BaseFilterViewModel filterViewModel, IQueryable<GraphicsCard> products)
        {
            if (filterViewModel.SelectedManufacturer != BaseFilterViewModel.AllManufacturers)
                products = products.Where(x => x.Product.Manufacturer.Name == filterViewModel.SelectedManufacturer);
            return products;
        }
    }
}
