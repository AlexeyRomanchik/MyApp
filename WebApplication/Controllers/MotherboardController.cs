using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.Contracts.FiltersContracts;
using WebApplication.Contracts.SortContracts;
using WebApplication.Models;
using WebApplication.ViewModels;
using WebApplication.ViewModels.AddViewModels;
using WebApplication.ViewModels.FilterViewModels;

namespace WebApplication.Controllers
{
    public class MotherboardController : Controller
    {
        private const int PageSize = 20;

        private readonly IMotherboardFilter _motherboardFilter;
        private readonly IMotherboardRepository _motherboardRepository;
        private readonly IMotherboardSortService _motherboardSortService;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MotherboardController(IRepositoryWrapper repositoryWrapper, ISortServiceWrapper sortServiceWrapper,
            IMotherboardFilter motherboardFilter, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _repositoryWrapper = repositoryWrapper;
            _motherboardFilter = motherboardFilter;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _motherboardRepository = _repositoryWrapper.MotherboardRepository;
            _motherboardSortService = sortServiceWrapper.MotherboardSortService;
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
            var product = _motherboardRepository.FindByCondition(x => x.Product.Id == id).First();

            var infoViewModel = new InfoViewModel<Motherboard>
            {
                Product = product,
                PopularGoods = _motherboardRepository.FindAll()
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(4).ToList()
            };

            return View(infoViewModel);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Remove(Guid id)
        {
            var product = _motherboardRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            if (product == null) return RedirectToAction("Table");

            _fileService.DeleteFile(_webHostEnvironment.WebRootPath + product.Product.ImageUrl);
            _motherboardRepository.Delete(product);
            _repositoryWrapper.Save();

            return RedirectToAction("Table");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Add(AddMotherboardViewModel motherboardViewModel)
        {
            var guid = Guid.NewGuid();
            string filePath;

            if (motherboardViewModel.UploadedFile != null)
            {
                var fileExtension = Path.GetExtension(motherboardViewModel.UploadedFile.FileName);
                var fileName = Path.GetFileNameWithoutExtension(motherboardViewModel.UploadedFile.FileName);
                filePath = "/productsImages/Motherboard/" + fileName + guid + fileExtension;
                _fileService.SaveUploadedFile(motherboardViewModel.UploadedFile, _webHostEnvironment.WebRootPath + filePath);
            }
            else
            {
                filePath = "/productsImages/default.jpg";
            }

            var motherboard = new Motherboard()
            {
                Id = guid,
                ChipSet = motherboardViewModel.ChipSet,
                FormFactor = motherboardViewModel.FormFactor,
                MemorySlotsNumber = motherboardViewModel.MemorySlotsNumber,
                Product = new Product
                {
                    Id = guid,
                    CategoryId = 2,
                    Name = motherboardViewModel.Name,
                    ImageUrl = filePath,
                    DateAdded = DateTime.Now,
                    Price = motherboardViewModel.Price,
                    QuantityInStock = motherboardViewModel.QuantityInStock,
                    ManufacturerId = 1
                }
            };

            _motherboardRepository.Create(motherboard);
            _repositoryWrapper.Save();

            return View();
        }



        private async Task<MotherboardViewModel> PrepareData(int page = 1, string name = null,
            SortState sortState = SortState.DateAddedDesc,
            string manufacturer = BaseFilterViewModel.AllManufacturers)
        {
            var products = _motherboardRepository.FindAll();
            var manufacturers = products.Select(x => x.Product.Manufacturer.Name).Distinct();

            var filterViewModel = new BaseFilterViewModel(manufacturers.ToList(), manufacturer);

            if (name != null) products = products.Where(x => x.Product.Name.Contains(name));

            products = _motherboardFilter.ApplyBaseFilter(filterViewModel, products);

            products = _motherboardSortService.SortBy(sortState, products);

            var count = await products.CountAsync();

            var items = await products.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, PageSize);

            var motherboardViewModel = new MotherboardViewModel
            {
                BaseFilterViewModel = filterViewModel,
                SortBaseViewModel = new SortBaseViewModel(sortState),
                Products = items,
                PageViewModel = pageViewModel,
                NewItems = products
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(20).ToList()
            };

            return motherboardViewModel;
        }
    }
}