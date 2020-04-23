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
    public class CpuCrudController : Controller
    {
        private const int PageSize = 100;

        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ICpuRepository _cpuRepository;
        public CpuCrudController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _cpuRepository = repositoryWrapper.CpuRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var products = _cpuRepository.FindAll();

            var count = await products.CountAsync();

            var items = await products.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, PageSize);

            var ramViewModel = new CrudViewModel<Cpu>
            {
                Products = items,
                PageViewModel = pageViewModel
            };

            return View(ramViewModel);
        }

        public IActionResult Remove(Guid id)
        {
            var product = _cpuRepository.FindByCondition(x => x.Id == id).FirstOrDefault();

            if (product == null) return RedirectToAction("Index");
            _cpuRepository.Delete(product);
            _repositoryWrapper.Save();

            return RedirectToAction("Index");
        }
    }
}