using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataProvider.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Product;
using WebApplication.Interfaces.FiltersContracts;
using WebApplication.Interfaces;
using WebApplication.Interfaces.SortContracts;
using WebApplication.ViewModels;
using WebApplication.ViewModels.AddViewModels;
using WebApplication.ViewModels.FilterViewModels;

namespace WebApplication.Controllers
{
    public class CpuController : Controller
    {
        private const int PageSize = 20;

        private readonly ICpuFilter _cpuFilter;
        private readonly ICpuRepository _cpuRepository;
        private readonly ICpuSortService _cpuSortService;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CpuController(IRepositoryWrapper repositoryWrapper, ISortServiceWrapper sortServiceWrapper,
            ICpuFilter cpuFilter, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _repositoryWrapper = repositoryWrapper;
            _cpuFilter = cpuFilter;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _cpuRepository = _repositoryWrapper.CpuRepository;
            _cpuSortService = sortServiceWrapper.CpuSortService;
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
            var product = _cpuRepository.FindByCondition(x => x.Product.Id == id).First();

            var infoViewModel = new InfoViewModel<Cpu>
            {
                Product = product,
                PopularGoods = _cpuRepository.FindAll()
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(4).ToList()
            };

            return View(infoViewModel);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Remove(Guid id)
        {
            var product = _cpuRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            if (product == null) return RedirectToAction("Table");

            _fileService.DeleteFile(_webHostEnvironment.WebRootPath + product.Product.ImageUrl);
            _cpuRepository.Delete(product);
            _repositoryWrapper.Save();

            return RedirectToAction("Table");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Add(AddCpuViewModel cpuViewModel)
        {
            var guid = Guid.NewGuid();
            string filePath;

            if (cpuViewModel.UploadedFile != null)
            {
                var fileExtension = Path.GetExtension(cpuViewModel.UploadedFile.FileName);
                var fileName = Path.GetFileNameWithoutExtension(cpuViewModel.UploadedFile.FileName);
                filePath = "/productsImages/Cpu/" + fileName + guid + fileExtension;
                _fileService.SaveUploadedFile(cpuViewModel.UploadedFile, _webHostEnvironment.WebRootPath + filePath);
            }
            else
            {
                filePath = "/productsImages/default.jpg";
            }

            var cpu = new Cpu
            {
                Id = guid,
                BaseClock = cpuViewModel.BaseClock,
                L2Cache = cpuViewModel.L2Cache,
                L3Cache = cpuViewModel.L2Cache,
                MaxMemoryFrequency = cpuViewModel.MaxMemoryFrequency,
                MaximumFrequency = cpuViewModel.MaximumFrequency,
                MaximumNumberThreads = cpuViewModel.MaximumNumberThreads,
                NumberCores = cpuViewModel.NumberCores,
                NumberMemoryChannels = cpuViewModel.NumberMemoryChannels,
                Socket = cpuViewModel.Socket,
                MemorySupport = cpuViewModel.MemorySupport,
                SocketTypeId = 1,
                Tdp = cpuViewModel.Tdp,
                Product = new Product
                {
                    Id = guid,
                    CategoryId = 2,
                    Name = cpuViewModel.Name,
                    ImageUrl = filePath,
                    DateAdded = DateTime.Now,
                    Price = cpuViewModel.Price,
                    QuantityInStock = cpuViewModel.QuantityInStock,
                    ManufacturerId = 1
                }
            };

            _cpuRepository.Create(cpu);
            _repositoryWrapper.Save();

            return View();
        }

        private async Task<CpuViewModel> PrepareData(int page = 1, string name = null,
            SortState sortState = SortState.DateAddedDesc,
            string manufacturer = BaseFilterViewModel.AllManufacturers)
        {
            var products = _cpuRepository.FindAll();
            var manufacturers = products.Select(x => x.Product.Manufacturer.Name).Distinct();

            var filterViewModel = new BaseFilterViewModel(manufacturers.ToList(), manufacturer);

            if (name != null) products = products.Where(x => x.Product.Name.Contains(name));

            products = _cpuFilter.ApplyBaseFilter(filterViewModel, products);

            products = _cpuSortService.SortBy(sortState, products);

            var count = await products.CountAsync();

            var items = await products.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, PageSize);

            var cpuViewModel = new CpuViewModel
            {
                BaseFilterViewModel = filterViewModel,
                SortBaseViewModel = new SortBaseViewModel(sortState),
                Products = items,
                PageViewModel = pageViewModel,
                NewItems = products
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(20).ToList()
            };
            return cpuViewModel;
        }
    }
}