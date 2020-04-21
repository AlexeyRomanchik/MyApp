using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class HddController : Controller
    {
        private const int PageSize = 20;

        private readonly IHddRepository _hddRepository;

        public HddController(IRepositoryWrapper repositoryWrapper)
        {
            _hddRepository = repositoryWrapper.HddRepository;
        }


        public async Task<IActionResult> Index(int page = 1)
        {
            var ramProducts = _hddRepository.FindAll();

            var count = await ramProducts.CountAsync();

            var items = await ramProducts.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, PageSize);

            var cpuViewModel = new HddViewModel
            {
                HddList = items,
                PageViewModel = pageViewModel,
                NewItems = ramProducts
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(20).ToList()
            };

            return View(cpuViewModel);
        }
    }
}