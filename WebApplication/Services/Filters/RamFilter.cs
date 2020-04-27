using System.Linq;
using WebApplication.Contracts.FiltersContracts;
using WebApplication.Models;
using WebApplication.ViewModels.FilterViewModels;

namespace WebApplication.Services.Filters
{
    public class RamFilter : IRamFilter
    {
        public IQueryable<Ram> ApplyBaseFilter(BaseFilterViewModel filterViewModel, IQueryable<Ram> rams)
        {
            if (filterViewModel.SelectedManufacturer != BaseFilterViewModel.AllManufacturers)
                rams = rams.Where(x => x.Product.Manufacturer.Name == filterViewModel.SelectedManufacturer);
            return rams;
        }
    }
}
