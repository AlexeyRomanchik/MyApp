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
    public class MotherboardController : Controller
    {
        private const int PageSize = 20;

        private readonly IMotherboardRepository _motherboardRepository;
        private readonly IMotherboardSortService _motherboardSortService;
        private readonly IMotherboardFilter _motherboardFilter;

        public MotherboardController(IRepositoryWrapper repositoryWrapper, ISortServiceWrapper sortServiceWrapper, IMotherboardFilter motherboardFilter)
        {
            _motherboardFilter = motherboardFilter;
            _motherboardRepository = repositoryWrapper.MotherboardRepository;
            _motherboardSortService = sortServiceWrapper.MotherboardSortService;
        }

        public async Task<IActionResult> Index(int page = 1, string name = null,
            SortState sortState = SortState.DateAddedDesc,
            string manufacturer = BaseFilterViewModel.AllManufacturers)
        {
            var products = _motherboardRepository.FindAll();
            var manufacturers = products.Select(x => x.Product.Manufacturer.Name).Distinct();

            var filterViewModel = new BaseFilterViewModel(manufacturers.ToList(), manufacturer);

            if (name != null)
            {
                products = products.Where(x => x.Product.Name.Contains(name));
            }

            products = _motherboardFilter.ApplyBaseFilter(filterViewModel, products);

            products = _motherboardSortService.SortBy(sortState, products);

            var count = await products.CountAsync();

            var items = await products.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, PageSize);

            var motherboardViewModel = new MotherboardViewModel()
            {
                BaseFilterViewModel = filterViewModel,
                SortBaseViewModel = new SortBaseViewModel(sortState),
                Products = items,
                PageViewModel = pageViewModel,
                NewItems = products
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(20).ToList()
            };

            return View(motherboardViewModel);
        }

        public IActionResult Info(Guid id)
        {
            var product = _motherboardRepository.
                FindByCondition(x => x.Product.Id == id).First();

            var infoViewModel = new InfoViewModel<Motherboard>()
            {
                Product = product,
                PopularGoods = _motherboardRepository.FindAll()
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(4).ToList()
            };

            return View(infoViewModel);
        }

    }
}