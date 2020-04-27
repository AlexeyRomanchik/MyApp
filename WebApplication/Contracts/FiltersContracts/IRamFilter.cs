using System.Linq;
using WebApplication.Models;
using WebApplication.ViewModels.FilterViewModels;

namespace WebApplication.Contracts.FiltersContracts
{
    public interface IRamFilter
    {
        IQueryable<Ram> ApplyBaseFilter(BaseFilterViewModel filterViewModel, IQueryable<Ram> rams);
    }
}