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
    public class HddCrudController : Controller
    {
        private const int PageSize = 100;

        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IHddRepository _hddRepository;
        public HddCrudController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _hddRepository = repositoryWrapper.HddRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var products = _hddRepository.FindAll();

            var count = await products.CountAsync();

            var items = await products.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, PageSize);

            var ramViewModel = new CrudViewModel<Hdd>
            {
                Products = items,
                PageViewModel = pageViewModel
            };

            return View(ramViewModel);
        }

        public IActionResult Remove(Guid id)
        {
            var product = _hddRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            if (product == null) return RedirectToAction("Index");
            _hddRepository.Delete(product);
            _repositoryWrapper.Save();

            return RedirectToAction("Index");
        }
    }
}