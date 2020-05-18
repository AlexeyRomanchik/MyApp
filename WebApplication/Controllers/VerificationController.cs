using System;
using System.Linq;
using System.Threading.Tasks;
using DataProvider.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Authentication;
using Models.User;
using WebApplication.Interfaces;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class VerificationController : Controller
    {
        private const int AttemptsNumber = 3;


        private readonly AuthMessageSender _authMessageSender;
        private readonly UserManager<User> _userManager;
        private readonly IVerificationCodeGenerator _verificationCodeGenerator;
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IUserRepository _userRepository;

        public VerificationController(UserManager<User> userManager, IVerificationCodeGenerator verificationCodeGenerator, 
            IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _userManager = userManager;
            _verificationCodeGenerator = verificationCodeGenerator;
            _verificationCodeRepository = _repositoryWrapper.VerificationCodeRepository;
            _authMessageSender = new AuthMessageSender();
            _userRepository = _repositoryWrapper.UserRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public void AddPhoneNumber(string phoneNumber)
        {

            var verificationCode = new VerificationCode
            {
                UserId = _userManager.GetUserId(User),
                AttemptsNumber = AttemptsNumber,
                Code = _verificationCodeGenerator.GenerateVerificationCode(
                    10000, 100000, DateTime.Now.Millisecond
                    ),
                SendingDate = DateTime.Now,
                NextSendingDate = DateTime.Now.AddHours(5),
                PhoneNumber = phoneNumber
            };


            _authMessageSender.SendSmsAsync("+" + phoneNumber, verificationCode.Code.ToString());

            _verificationCodeRepository.Create(verificationCode);
            _repositoryWrapper.Save();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CheckCode(int code)
        {
            var verificationCode = _verificationCodeRepository
                .FindByCondition(x => x.UserId == _userManager.GetUserId(User))
                .OrderByDescending(x => x.SendingDate)
                .FirstOrDefault();

            if (verificationCode == null)
                return RedirectToAction("Index", "Account");

            if (verificationCode.AttemptsNumber > 0)
            {
                verificationCode.AttemptsNumber--;
                if (verificationCode.SendingDate.AddMinutes(5) > DateTime.Now)
                {
                    if (verificationCode.Code == code)
                    {
                        var user = await _userManager.GetUserAsync(User);
                        user.PhoneNumber = verificationCode.PhoneNumber;
                        user.PhoneNumberConfirmed = true;

                        _userRepository.Update(user);
                        _repositoryWrapper.Save();
                    }
                }
            }

            return RedirectToAction("Index", "Account");
        }
      


        [HttpPost]
        public IActionResult Index(string phoneNumber)
        {
            _authMessageSender.SendSmsAsync("+375297751690", "code: 12345");

            return RedirectToAction("Index", "Home");
        }
    }
}