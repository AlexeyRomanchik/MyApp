using Microsoft.AspNetCore.Mvc;
using WebApplication.Contracts;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class RamController : Controller
    {
        private readonly IRamRepository _ramRepository;

        public RamController(IRepositoryWrapper repositoryWrapper)
        {
            _ramRepository = repositoryWrapper.RamRepository;
        }

        public IActionResult Index()
        {
            var ramProducts = _ramRepository.FindAll();
            var ramViewModel = new RamViewModel
            {
                Rams = ramProducts
            };

            return View(ramViewModel);
        }
    }
}