using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class RamController : Controller
    {
        private const int PageSize = 20;

        private readonly IRamRepository _ramRepository;

        public RamController(IRepositoryWrapper repositoryWrapper)
        {
            _ramRepository = repositoryWrapper.RamRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var ramProducts = _ramRepository.FindAll();

            var count = await ramProducts.CountAsync();

            var items = await ramProducts.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, PageSize);

            var ramViewModel = new RamViewModel
            {
                Rams = items,
                PageViewModel = pageViewModel,
                NewItems = ramProducts.ToList()

            };

            return View(ramViewModel);
        }
    }
}