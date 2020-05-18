using System.Linq;
using Models.Product;
using WebApplication.Interfaces.FiltersContracts;
using WebApplication.ViewModels.FilterViewModels;

namespace WebApplication.Services.Filters
{
    public class MotherboardFilter : IMotherboardFilter
    {
        public IQueryable<Motherboard> ApplyBaseFilter(BaseFilterViewModel filterViewModel, IQueryable<Motherboard> products)
        {
            if (filterViewModel.SelectedManufacturer != BaseFilterViewModel.AllManufacturers)
                products = products.Where(x => x.Product.Manufacturer.Name == filterViewModel.SelectedManufacturer);
            return products;
        }
    }
}
