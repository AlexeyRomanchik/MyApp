using System.Linq;
using Models.Product;
using WebApplication.Interfaces.FiltersContracts;
using WebApplication.ViewModels.FilterViewModels;

namespace WebApplication.Services.Filters
{
    public class CpuFilter : ICpuFilter
    {
        public IQueryable<Cpu> ApplyBaseFilter(BaseFilterViewModel filterViewModel, IQueryable<Cpu> products)
        {
            if (filterViewModel.SelectedManufacturer != BaseFilterViewModel.AllManufacturers)
                products = products.Where(x => x.Product.Manufacturer.Name == filterViewModel.SelectedManufacturer);
            return products;
        }
    }
}
