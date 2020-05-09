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
    public class RamController : Controller
    {
        private const int PageSize = 20;

        private readonly IFileService _fileService;
        private readonly IRamFilter _ramFilter;
        private readonly IRamRepository _ramRepository;
        private readonly IRamSortService _ramSortService;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RamController(IRepositoryWrapper repositoryWrapper, ISortServiceWrapper sortServiceWrapper,
            IRamFilter ramFilter, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _repositoryWrapper = repositoryWrapper;
            _ramFilter = ramFilter;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _ramSortService = sortServiceWrapper.RamSortService;
            _ramRepository = _repositoryWrapper.RamRepository;
        }

        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
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
            var product = _ramRepository.FindByCondition(x => x.Product.Id == id).First();

            var ramInfoViewModel = new RamInfoViewModel
            {
                Ram = product,
                PopularGoods = _ramRepository.FindAll()
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(4).ToList()
            };

            return View(ramInfoViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(Guid id)
        {
            var product = _ramRepository.FindByCondition(x => x.Product.Id == id).FirstOrDefault();

            if (product == null)
                return RedirectToAction("Table");

            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(Ram ram)
        {
            _ramRepository.Update(ram);
            _repositoryWrapper.Save();

            return RedirectToAction("Table");
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Add(AddRamViewModel ramViewModel)
        {
            var guid = Guid.NewGuid();
            string filePath;

            if (ramViewModel.UploadedFile != null)
            {
                var fileExtension = Path.GetExtension(ramViewModel.UploadedFile.FileName);
                var fileName = Path.GetFileNameWithoutExtension(ramViewModel.UploadedFile.FileName);
                filePath = "/productsImages/Ram/" + fileName + guid + fileExtension;
                _fileService.SaveUploadedFile(ramViewModel.UploadedFile, _webHostEnvironment.WebRootPath + filePath);
            }
            else
            {
                filePath = "/productsImages/default.jpg";
            }

            var ram = new Ram
            {
                Id = guid,
                ContactsNumber = ramViewModel.ContactsNumber,
                Frequency = ramViewModel.Frequency,
                SupplyVoltage = ramViewModel.SupplyVoltage,
                RamMemoryTypeId = 1,
                Throughput = ramViewModel.Throughput,
                TotalMemory = ramViewModel.TotalMemory,
                Product = new Product
                {
                    Id = guid,
                    CategoryId = 2,
                    Name = ramViewModel.Name,
                    ImageUrl = filePath,
                    DateAdded = DateTime.Now,
                    Price = ramViewModel.Price,
                    QuantityInStock = ramViewModel.QuantityInStock,
                    ManufacturerId = 1
                }
            };

            _ramRepository.Create(ram);
            _repositoryWrapper.Save();

            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult Remove(Guid id)
        {
            var product = _ramRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            if (product == null) return RedirectToAction("Table");

            _fileService.DeleteFile(_webHostEnvironment.WebRootPath + product.Product.ImageUrl);
            _ramRepository.Delete(product);
            _repositoryWrapper.Save();

            return RedirectToAction("Table");
        }

        private async Task<RamViewModel> PrepareData(int page = 1, string name = null,
            SortState sortState = SortState.DateAddedDesc,
            string manufacturer = BaseFilterViewModel.AllManufacturers)
        {
            var ramProducts = _ramRepository.FindAll();
            var manufacturers = ramProducts.Select(x => x.Product.Manufacturer.Name).Distinct();

            var filterViewModel = new BaseFilterViewModel(manufacturers.ToList(), manufacturer);


            if (name != null) ramProducts = ramProducts.Where(x => x.Product.Name.Contains(name));

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
            return ramViewModel;
        }
    }
}