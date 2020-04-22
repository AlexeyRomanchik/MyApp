using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class MotherboardController : Controller
    {
        private const int PageSize = 20;

        private readonly IMotherboardRepository _motherboardRepository;

        public MotherboardController(IRepositoryWrapper repositoryWrapper)
        {
            _motherboardRepository = repositoryWrapper.MotherboardRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var ramProducts = _motherboardRepository.FindAll();

            var count = await ramProducts.CountAsync();

            var items = await ramProducts.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, PageSize);

            var cpuViewModel = new MotherboardViewModel
            {
                Motherboards = items,
                PageViewModel = pageViewModel,
                NewItems = ramProducts
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(20).ToList()
            };

            return View(cpuViewModel);
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