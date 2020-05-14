
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SalesStatisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}