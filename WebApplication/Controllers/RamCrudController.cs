using Microsoft.AspNetCore.Mvc;
using WebApplication.Contracts;

namespace WebApplication.Controllers
{
    public class RamCrudController : Controller
    {
        private readonly IRamRepository _ramRepository;
        public RamCrudController(IRepositoryWrapper repositoryWrapper)
        {
            _ramRepository = repositoryWrapper.RamRepository;
        }

        public IActionResult Index()
        {
            var products = _ramRepository.FindAll();
            return View(products);
        }
    }
}