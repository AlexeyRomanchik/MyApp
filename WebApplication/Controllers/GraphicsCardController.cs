using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.Contracts.SortContracts;
using WebApplication.Models;
using WebApplication.ViewModels;
using WebApplication.ViewModels.FilterViewModels;

namespace WebApplication.Controllers
{
    public class GraphicsCardController : Controller
    {
        private const int PageSize = 20;

        private readonly IGraphicsCardRepository _graphicsCardRepository;
        private readonly IGraphicsCardSortService _graphicsCardSortService;

        public GraphicsCardController(IRepositoryWrapper repositoryWrapper, ISortServiceWrapper sortServiceWrapper)
        {
            _graphicsCardRepository = repositoryWrapper.GraphicsCardRepository;
            _graphicsCardSortService = sortServiceWrapper.GraphicsCardSortService;
        }

        public async Task<IActionResult> Index(int page = 1, string name = null,
            SortState sortState = SortState.DateAddedDesc,
            string manufacturer = BaseFilterViewModel.AllManufacturers)
        {
            var products = _graphicsCardRepository.FindAll();
            var manufacturers = products.Select(x => x.Product.Manufacturer.Name).Distinct();

            var filterViewModel = new BaseFilterViewModel(manufacturers.ToList(), manufacturer);

            if (name != null)
            {
                products = products.Where(x => x.Product.Name.Contains(name));
            }

            products = _graphicsCardSortService.SortBy(sortState, products);

            var count = await products.CountAsync();

            var items = await products.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, PageSize);

            var graphicsCardViewModel = new GraphicsCardViewModel()
            {
                SortBaseViewModel = new SortBaseViewModel(sortState),
                Products = items,
                PageViewModel = pageViewModel,
                NewItems = products
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(20).ToList()
            };

            return View(graphicsCardViewModel);
        }

        public IActionResult Info(Guid id)
        {
            var product = _graphicsCardRepository.
                FindByCondition(x => x.Product.Id == id).First();

            var infoViewModel = new InfoViewModel<GraphicsCard>()
            {
                Product = product,
                PopularGoods = _graphicsCardRepository.FindAll()
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(4).ToList()
            };

            return View(infoViewModel);
        }

    }
}