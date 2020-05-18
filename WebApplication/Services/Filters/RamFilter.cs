using System.Linq;
using Models.Product;
using WebApplication.Interfaces.FiltersContracts;
using WebApplication.ViewModels.FilterViewModels;

namespace WebApplication.Services.Filters
{
    public class RamFilter : IRamFilter
    {
        public IQueryable<Ram> ApplyBaseFilter(BaseFilterViewModel filterViewModel, IQueryable<Ram> products)
        {
            if (filterViewModel.SelectedManufacturer != BaseFilterViewModel.AllManufacturers)
                products = products.Where(x => x.Product.Manufacturer.Name == filterViewModel.SelectedManufacturer);
            return products;
        }
    }
}
