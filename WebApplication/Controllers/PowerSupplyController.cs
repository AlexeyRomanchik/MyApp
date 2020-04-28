using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class PowerSupplyController : Controller
    {
        private const int PageSize = 20;
        private readonly IPowerSupplyFilter _powerSupplyFilter;

        private readonly IPowerSupplyRepository _powerSupplyRepository;
        private readonly IPowerSupplySortService _powerSupplySortService;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public PowerSupplyController(IRepositoryWrapper repositoryWrapper, ISortServiceWrapper sortServiceWrapper,
            IPowerSupplyFilter powerSupplyFilter)
        {
            _repositoryWrapper = repositoryWrapper;
            _powerSupplyFilter = powerSupplyFilter;
            _powerSupplyRepository = _repositoryWrapper.PowerSupplyRepository;
            _powerSupplySortService = sortServiceWrapper.PowerSupplySortService;
        }


        public IActionResult Index(int page = 1, string name = null,
            SortState sortState = SortState.DateAddedDesc,
            string manufacturer = BaseFilterViewModel.AllManufacturers)
        {
            var viewModel = PrepareData(page, name, sortState, manufacturer);

            return View(viewModel.Result);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Table(int page = 1, string name = null,
            SortState sortState = SortState.DateAddedDesc,
            string manufacturer = BaseFilterViewModel.AllManufacturers)
        {
            var viewModel = PrepareData(page, name, sortState, manufacturer);
            return View(viewModel.Result);
        }

        public IActionResult Info(Guid id)
        {
            var product = _powerSupplyRepository.FindByCondition(x => x.Product.Id == id).First();

            var infoViewModel = new InfoViewModel<PowerSupply>
            {
                Product = product,
                PopularGoods = _powerSupplyRepository.FindAll()
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(4).ToList()
            };

            return View(infoViewModel);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Remove(Guid id)
        {
            var product = _powerSupplyRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            if (product == null) return RedirectToAction("Table");
            _powerSupplyRepository.Delete(product);
            _repositoryWrapper.Save();

            return RedirectToAction("Table");
        }

        private async Task<PowerSupplyViewModel> PrepareData(int page = 1, string name = null,
            SortState sortState = SortState.DateAddedDesc,
            string manufacturer = BaseFilterViewModel.AllManufacturers)
        {
            var powerSupplies = _powerSupplyRepository.FindAll();
            var manufacturers = powerSupplies.Select(x => x.Product.Manufacturer.Name).Distinct();

            var filterViewModel = new BaseFilterViewModel(manufacturers.ToList(), manufacturer);

            if (name != null) powerSupplies = powerSupplies.Where(x => x.Product.Name.Contains(name));

            powerSupplies = _powerSupplyFilter.ApplyBaseFilter(filterViewModel, powerSupplies);

            powerSupplies = _powerSupplySortService.SortBy(sortState, powerSupplies);

            var count = await powerSupplies.CountAsync();

            var items = await powerSupplies.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, PageSize);

            var powerSupplyViewModel = new PowerSupplyViewModel
            {
                BaseFilterViewModel = filterViewModel,
                SortBaseViewModel = new SortBaseViewModel(sortState),
                Products = items,
                PageViewModel = pageViewModel,
                NewItems = powerSupplies
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(20).ToList()
            };

            return powerSupplyViewModel;
        }
    }
}