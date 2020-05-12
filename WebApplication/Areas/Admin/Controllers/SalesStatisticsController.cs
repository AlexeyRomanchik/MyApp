
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Areas.Admin.Controllers
{
    public class SalesStatisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}