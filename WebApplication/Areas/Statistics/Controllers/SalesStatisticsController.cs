using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Areas.Statistics.Controllers
{
    public class SalesStatisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}