using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.Contracts.FiltersContracts;
using WebApplication.Contracts.SortContracts;
using WebApplication.Models;
using WebApplication.ViewModels;
using WebApplication.ViewModels.FilterViewModels;

namespace WebApplication.Controllers
{
    public class CpuController : Controller
    {
        private const int PageSize = 20;

        private readonly ICpuRepository _cpuRepository;
        private readonly ICpuSortService _cpuSortService;
        private readonly ICpuFilter _cpuFilter;

        public CpuController(IRepositoryWrapper repositoryWrapper, ISortServiceWrapper sortServiceWrapper, ICpuFilter cpuFilter)
        {
            _cpuFilter = cpuFilter;
            _cpuRepository = repositoryWrapper.CpuRepository;
            _cpuSortService = sortServiceWrapper.CpuSortService;
        }


        public async Task<IActionResult> Index(int page = 1, string name = null,
            SortState sortState = SortState.DateAddedDesc,
            string manufacturer = BaseFilterViewModel.AllManufacturers)
        {
            var products = _cpuRepository.FindAll();
            var manufacturers = products.Select(x => x.Product.Manufacturer.Name).Distinct();

            var filterViewModel = new BaseFilterViewModel(manufacturers.ToList(), manufacturer);

            if (name != null)
            {
                products = products.Where(x => x.Product.Name.Contains(name));
            }

            products = _cpuFilter.ApplyBaseFilter(filterViewModel, products);

            products = _cpuSortService.SortBy(sortState, products);

            var count = await products.CountAsync();

            var items = await products.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, PageSize);

            var cpuViewModel = new CpuViewModel()
            {
                BaseFilterViewModel = filterViewModel,
                SortBaseViewModel = new SortBaseViewModel(sortState),
                Products = items,
                PageViewModel = pageViewModel,
                NewItems = products
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(20).ToList()
            };

            return View(cpuViewModel);
        }

        public IActionResult Info(Guid id)
        {
            var product = _cpuRepository.
                FindByCondition(x => x.Product.Id == id).First();

            var infoViewModel = new InfoViewModel<Cpu>()
            {
                Product = product,
                PopularGoods = _cpuRepository.FindAll()
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(4).ToList()
            };

            return View(infoViewModel);
        }
    }
}