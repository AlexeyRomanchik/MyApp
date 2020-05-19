using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataProvider.Interfaces;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Product;
using WebApplication.Interfaces.FiltersContracts;
using WebApplication.Interfaces.SortContracts;
using WebApplication.ViewModels;
using WebApplication.ViewModels.AddViewModels;
using WebApplication.ViewModels.FilterViewModels;

namespace WebApplication.Controllers
{
    public class GraphicsCardController : Controller
    {
        private const int PageSize = 20;
        private readonly IFileService _fileService;
        private readonly IGraphicsCardFilter _graphicsCardFilter;

        private readonly IGraphicsCardRepository _graphicsCardRepository;
        private readonly IGraphicsCardSortService _graphicsCardSortService;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GraphicsCardController(IRepositoryWrapper repositoryWrapper, ISortServiceWrapper sortServiceWrapper,
            IGraphicsCardFilter graphicsCardFilter, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _repositoryWrapper = repositoryWrapper;
            _graphicsCardFilter = graphicsCardFilter;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _graphicsCardRepository = _repositoryWrapper.GraphicsCardRepository;
            _graphicsCardSortService = sortServiceWrapper.GraphicsCardSortService;
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
            var product = _graphicsCardRepository.FindByCondition(x => x.Product.Id == id).First();

            var infoViewModel = new InfoViewModel<GraphicsCard>
            {
                Product = product,
                PopularGoods = _graphicsCardRepository.FindAll()
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(4).ToList()
            };

            return View(infoViewModel);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Remove(Guid id)
        {
            var product = _graphicsCardRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            if (product == null) return RedirectToAction("Table");

            _fileService.DeleteFile(_webHostEnvironment.WebRootPath + product.Product.ImageUrl);
            _graphicsCardRepository.Delete(product);
            _repositoryWrapper.Save();

            return RedirectToAction("Table");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Add(AddGraphicsCardViewModel graphicsCardViewModel)
        {
            var guid = Guid.NewGuid();
            string filePath;

            if (graphicsCardViewModel.UploadedFile != null)
            {
                var fileExtension = Path.GetExtension(graphicsCardViewModel.UploadedFile.FileName);
                var fileName = Path.GetFileNameWithoutExtension(graphicsCardViewModel.UploadedFile.FileName);
                filePath = "/productsImages/VideoCards/" + fileName + guid + fileExtension;
                _fileService.SaveUploadedFile(
                    graphicsCardViewModel.UploadedFile, _webHostEnvironment.WebRootPath + filePath
                    );
            }
            else
            {
                filePath = "/productsImages/default.jpg";
            }

            var graphicsCard = new GraphicsCard
            {
                Id = guid,
                DirectXSupport = graphicsCardViewModel.DirectXSupport,
                FansNumber = graphicsCardViewModel.FansNumber,
                GpuTurboFrequency = graphicsCardViewModel.GpuTurboFrequency,
                GpuFrequency = graphicsCardViewModel.GpuFrequency,
                MemoryBusWidth = graphicsCardViewModel.MemoryBusWidth,
                StreamProcessorsNumber = graphicsCardViewModel.StreamProcessorsNumber,
                GraphicsCardMemoryTypeId = 1,
                MemoryFrequency = graphicsCardViewModel.MemoryFrequency,
                RecommendedPowerSupply = graphicsCardViewModel.RecommendedPowerSupply,
                VideoMemory = graphicsCardViewModel.VideoMemory,
                InterfaceId = 1,
                Product = new Product
                {
                    Id = guid,
                    CategoryId = 2,
                    Name = graphicsCardViewModel.Name,
                    ImageUrl = filePath,
                    DateAdded = DateTime.Now,
                    Price = graphicsCardViewModel.Price,
                    QuantityInStock = graphicsCardViewModel.QuantityInStock,
                    ManufacturerId = 1
                }
            };

            _graphicsCardRepository.Create(graphicsCard);
            _repositoryWrapper.Save();

            return View();
        }

        private async Task<GraphicsCardViewModel> PrepareData(int page = 1, string name = null,
            SortState sortState = SortState.DateAddedDesc,
            string manufacturer = BaseFilterViewModel.AllManufacturers)
        {
            var products = _graphicsCardRepository.FindAll();
            var manufacturers = products.Select(x => x.Product.Manufacturer.Name).Distinct();

            var filterViewModel = new BaseFilterViewModel(manufacturers.ToList(), manufacturer);

            if (name != null) products = products.Where(x => x.Product.Name.Contains(name));

            products = _graphicsCardFilter.ApplyBaseFilter(filterViewModel, products);

            products = _graphicsCardSortService.SortBy(sortState, products);

            var count = await products.CountAsync();

            var items = await products.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, PageSize);

            var graphicsCardViewModel = new GraphicsCardViewModel
            {
                BaseFilterViewModel = filterViewModel,
                SortBaseViewModel = new SortBaseViewModel(sortState),
                Products = items,
                PageViewModel = pageViewModel,
                NewItems = products
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(20).ToList()
            };

            return graphicsCardViewModel;
        }
    }
}