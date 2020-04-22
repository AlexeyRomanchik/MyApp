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
    public class PowerSupplyController : Controller
    {
        private const int PageSize = 20;

        private readonly IPowerSupplyRepository _powerSupplyRepository;

        public PowerSupplyController(IRepositoryWrapper repositoryWrapper)
        {
            _powerSupplyRepository = repositoryWrapper.PowerSupplyRepository;
        }


        public async Task<IActionResult> Index(int page = 1)
        {
            var powerSupplies = _powerSupplyRepository.FindAll();

            var count = await powerSupplies.CountAsync();

            var items = await powerSupplies.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, PageSize);

            var powerSupplyViewModel = new PowerSupplyViewModel
            {
                PowerSupplies = items,
                PageViewModel = pageViewModel,
                NewItems = powerSupplies
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(20).ToList()
            };

            return View(powerSupplyViewModel);
        }

        public IActionResult Info(Guid id)
        {
            var product = _powerSupplyRepository.
                FindByCondition(x => x.Product.Id == id).First();

            var infoViewModel = new InfoViewModel<PowerSupply>()
            {
                Product = product,
                PopularGoods = _powerSupplyRepository.FindAll()
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(4).ToList()
            };

            return View(infoViewModel);
        }

    }
}