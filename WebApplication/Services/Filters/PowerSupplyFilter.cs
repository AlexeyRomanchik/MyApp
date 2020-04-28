using System.Linq;
using WebApplication.Contracts.FiltersContracts;
using WebApplication.Models;
using WebApplication.ViewModels.FilterViewModels;

namespace WebApplication.Services.Filters
{
    public class PowerSupplyFilter : IPowerSupplyFilter
    {
        public IQueryable<PowerSupply> ApplyBaseFilter(BaseFilterViewModel filterViewModel, IQueryable<PowerSupply> products)
        {
            if (filterViewModel.SelectedManufacturer != BaseFilterViewModel.AllManufacturers)
                products = products.Where(x => x.Product.Manufacturer.Name == filterViewModel.SelectedManufacturer);
            return products;
        }
    }
}
