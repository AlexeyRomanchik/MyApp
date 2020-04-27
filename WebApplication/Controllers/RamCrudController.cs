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
    public class RamCrudController : Controller
    {
        private const int PageSize = 100;

        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IRamRepository _ramRepository;
        public RamCrudController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _ramRepository = repositoryWrapper.RamRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var products = _ramRepository.FindAll();

            var count = await products.CountAsync();

            var items = await products.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, PageSize);

            var ramViewModel = new CrudViewModel<Ram>
            {
                Products = items,
                PageViewModel = pageViewModel
            };

            return View(ramViewModel);
        }

        public IActionResult Add()
        {
            return View();
        }


        public IActionResult Remove(Guid id)
        {
            var product = _ramRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            if (product == null) return View("Index");
            _ramRepository.Delete(product);
            _repositoryWrapper.Save();

            return RedirectToAction("Index");
        }

    }
}