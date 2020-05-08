using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class VerificationController : Controller
    {
        private readonly AuthMessageSender _authMessageSender;

        public VerificationController()
        {
            _authMessageSender = new AuthMessageSender();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string phoneNumber)
        {
            _authMessageSender.SendSmsAsync("+375297751690", "code: 12345");

            return RedirectToAction("Index", "Home");
        }
    }
}