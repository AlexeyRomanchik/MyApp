using System.Linq;
using Models.Product;
using WebApplication.Interfaces.FiltersContracts;
using WebApplication.ViewModels.FilterViewModels;

namespace WebApplication.Services.Filters
{
    public class HddFilter : IHddFilter
    {
        public IQueryable<Hdd> ApplyBaseFilter(BaseFilterViewModel filterViewModel, IQueryable<Hdd> products)
        {
            if (filterViewModel.SelectedManufacturer != BaseFilterViewModel.AllManufacturers)
                products = products.Where(x => x.Product.Manufacturer.Name == filterViewModel.SelectedManufacturer);
            return products;
        }
    }
}