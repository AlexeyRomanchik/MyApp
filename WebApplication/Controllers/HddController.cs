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
using WebApplication.Interfaces;
using WebApplication.Interfaces.FiltersContracts;
using WebApplication.Interfaces.SortContracts;
using WebApplication.ViewModels;
using WebApplication.ViewModels.AddViewModels;
using WebApplication.ViewModels.FilterViewModels;

namespace WebApplication.Controllers
{
    public class HddController : Controller
    {
        private const int PageSize = 20;
        private readonly IFileService _fileService;

        private readonly IHddFilter _hddFilter;
        private readonly IHddRepository _hddRepository;
        private readonly IHddSortService _hddSortService;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HddController(IRepositoryWrapper repositoryWrapper, ISortServiceWrapper sortServiceWrapper,
            IHddFilter hddFilter, IFileService fileService, IWebHostEnvironment webHostEnvironment)
        {
            _repositoryWrapper = repositoryWrapper;
            _hddFilter = hddFilter;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _hddRepository = _repositoryWrapper.HddRepository;
            _hddSortService = sortServiceWrapper.HddSortService;
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
            var product = _hddRepository.FindByCondition(x => x.Product.Id == id).First();

            var infoViewModel = new InfoViewModel<Hdd>
            {
                Product = product,
                PopularGoods = _hddRepository.FindAll()
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(4).ToList()
            };

            return View(infoViewModel);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Remove(Guid id)
        {
            var product = _hddRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            if (product == null) return RedirectToAction("Table");

            _fileService.DeleteFile(_webHostEnvironment.WebRootPath + product.Product.ImageUrl);
            _hddRepository.Delete(product);
            _repositoryWrapper.Save();

            return RedirectToAction("Table");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Add(AddHddViewModel hddViewModel)
        {
            var guid = Guid.NewGuid();
            string filePath;

            if (hddViewModel.UploadedFile != null)
            {
                var fileExtension = Path.GetExtension(hddViewModel.UploadedFile.FileName);
                var fileName = Path.GetFileNameWithoutExtension(hddViewModel.UploadedFile.FileName);
                filePath = "/productsImages/Hdd/" + fileName + guid + fileExtension;
                _fileService.SaveUploadedFile(hddViewModel.UploadedFile, _webHostEnvironment.WebRootPath + filePath);
            }
            else
            {
                filePath = "/productsImages/default.jpg";
            }

            var hdd = new Hdd
            {
                Id = guid,
                FormFactor = hddViewModel.FormFactor,
                Buffer = hddViewModel.Buffer,
                NoiseReadingWriting = hddViewModel.NoiseReadingWriting,
                PowerConsumptionForReadWrite = hddViewModel.PowerConsumptionForReadWrite,
                SectorSize = hddViewModel.SectorSize,
                PowerConsumptionStandby = hddViewModel.PowerConsumptionStandby,
                SpindleSpeed = hddViewModel.SpindleSpeed,
                NoiseStandby = hddViewModel.NoiseStandby,
                Volume = hddViewModel.Volume,
                SequentialReadSpeed = hddViewModel.SequentialReadSpeed,
                InterfaceId = 1,
                SequentialWriteSpeed = hddViewModel.SequentialWriteSpeed,
                Product = new Product
                {
                    Id = guid,
                    CategoryId = 2,
                    Name = hddViewModel.Name,
                    ImageUrl = filePath,
                    DateAdded = DateTime.Now,
                    Price = hddViewModel.Price,
                    QuantityInStock = hddViewModel.QuantityInStock,
                    ManufacturerId = 1
                }
            };

            _hddRepository.Create(hdd);
            _repositoryWrapper.Save();

            return View();
        }

        private async Task<HddViewModel> PrepareData(int page = 1, string name = null,
            SortState sortState = SortState.DateAddedDesc,
            string manufacturer = BaseFilterViewModel.AllManufacturers)
        {
            var products = _hddRepository.FindAll();
            var manufacturers = products.Select(x => x.Product.Manufacturer.Name).Distinct();

            var filterViewModel = new BaseFilterViewModel(manufacturers.ToList(), manufacturer);

            if (name != null) products = products.Where(x => x.Product.Name.Contains(name));

            products = _hddFilter.ApplyBaseFilter(filterViewModel, products);

            products = _hddSortService.SortBy(sortState, products);

            var count = await products.CountAsync();

            var items = await products.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, PageSize);

            var hddViewModel = new HddViewModel
            {
                BaseFilterViewModel = filterViewModel,
                SortBaseViewModel = new SortBaseViewModel(sortState),
                Products = items,
                PageViewModel = pageViewModel,
                NewItems = products
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(20).ToList()
            };

            return hddViewModel;
        }
    }
}