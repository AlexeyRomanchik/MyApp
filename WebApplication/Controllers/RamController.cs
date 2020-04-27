using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.Contracts.FiltersContracts;
using WebApplication.Contracts.SortContracts;
using WebApplication.ViewModels;
using WebApplication.ViewModels.FilterViewModels;

namespace WebApplication.Controllers
{
    public class RamController : Controller
    {
        private const int PageSize = 20;

        private readonly IRamRepository _ramRepository;
        private readonly IRamSortService _ramSortService;
        private readonly IRamFilter _ramFilter;

        public RamController(IRepositoryWrapper repositoryWrapper, ISortServiceWrapper sortServiceWrapper, IRamFilter ramFilter)
        {
            _ramFilter = ramFilter;
            _ramSortService = sortServiceWrapper.RamSortService;
            _ramRepository = repositoryWrapper.RamRepository;
        }

        public async Task<IActionResult> Index(int page = 1, string name = null, 
            SortState sortState = SortState.DateAddedDesc,
            string manufacturer = BaseFilterViewModel.AllManufacturers)
        {
            var ramProducts = _ramRepository.FindAll();
            var manufacturers = ramProducts.Select(x => x.Product.Manufacturer.Name).Distinct();

            var filterViewModel = new BaseFilterViewModel(manufacturers.ToList(), manufacturer);


            if (name != null)
            {
                ramProducts = ramProducts.Where(x => x.Product.Name.Contains(name));
            }

            ramProducts = _ramFilter.ApplyBaseFilter(filterViewModel, ramProducts);

            ramProducts = _ramSortService.SortBy(sortState, ramProducts);

            var count = await ramProducts.CountAsync();
            var pageViewModel = new PageViewModel(count, page, PageSize);
            var items = await ramProducts.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();


            var ramViewModel = new RamViewModel
            {
                BaseFilterViewModel = filterViewModel,
                SortBaseViewModel = new SortBaseViewModel(sortState),
                Products = items,
                PageViewModel = pageViewModel,
                NewItems = ramProducts
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(20).ToList()
            };

            return View(ramViewModel);
        }

        public IActionResult Info(Guid id)
        {
            var product = _ramRepository.FindByCondition(x => x.Product.Id == id).First();

            var ramInfoViewModel = new RamInfoViewModel()
            {
                Ram = product,
                PopularGoods = _ramRepository.FindAll()
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(4).ToList()
            };

            return View(ramInfoViewModel);
        }
    }
}