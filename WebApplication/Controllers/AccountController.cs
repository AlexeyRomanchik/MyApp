using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Contracts;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Services;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _appDbContext;
        private readonly IEmailService _emailService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IVerificationCodeGenerator _verificationCodeGenerator;
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly AuthMessageSender _authMessageSender;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager,
            ApplicationContext appDbContext, IEmailService emailService, IRepositoryWrapper repositoryWrapper,
            IFileService fileService, IWebHostEnvironment webHostEnvironment, IVerificationCodeGenerator verificationCodeGenerator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appDbContext = appDbContext;
            _emailService = emailService;
            _userRepository = repositoryWrapper.UserRepository;
            _repositoryWrapper = repositoryWrapper;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _verificationCodeGenerator = verificationCodeGenerator;
            _verificationCodeRepository = _repositoryWrapper.VerificationCodeRepository;
            _authMessageSender = new AuthMessageSender();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return View("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                var emailService = new EmailService();
                await emailService.SendEmailAsync(model.Email, "Reset Password",
                    $"Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>link</a>");
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return View("ResetPasswordConfirmation");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(AccountViewModel accountViewModel)
        {
            var user = await _userManager.GetUserAsync(User);

            user.Name = accountViewModel.User.Name;
            user.Surname = accountViewModel.User.Surname;
            user.Email = accountViewModel.User.Email;
            user.TwoFactorAuthenticationEnabled = accountViewModel.User.TwoFactorAuthenticationEnabled;
            user.ReceiveProductNotifications = accountViewModel.User.ReceiveProductNotifications;

            user.Address = new Address
            {
                Region = accountViewModel.User.Address.Region,
                City = accountViewModel.User.Address.City,
                Street = accountViewModel.User.Address.Street,
                HouseOrFlat = accountViewModel.User.Address.HouseOrFlat
            };

            if (accountViewModel.UploadedFile != null)
            {
                var fileExtension = Path.GetExtension(accountViewModel.UploadedFile.FileName);
                var fileName = Path.GetFileNameWithoutExtension(accountViewModel.UploadedFile.FileName);
                var filePath = "/userImages/" + fileName + user.Id + fileExtension;
                _fileService.SaveUploadedFile(accountViewModel.UploadedFile, _webHostEnvironment.WebRootPath + filePath);

                user.UserImageUrl = filePath;
            }

            _userRepository.Update(user);
            _repositoryWrapper.Save();

            return RedirectToAction("Index");
        }



        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var user = _userRepository
                .FindByCondition(x => x.Id == _userManager.GetUserId(User))
                .FirstOrDefault();

            if (user == null)
                return RedirectToAction("Index", "Home");

            var accountViewModel = new AccountViewModel
            {
                User = user
            };

            return View(accountViewModel);
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new {userId = user.Id, code},
                        HttpContext.Request.Scheme
                        );
                    await _emailService.SendEmailAsync(
                        model.Email, "Confirm your account",
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>"
                        );

                    await _appDbContext.SaveChangesAsync();

                    return Content(
                        "Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме"
                        );
                }

                foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return View("Error");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return View("Error");

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            return View(
                new LoginViewModel
                {
                    ReturnUrl = returnUrl,
                    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
                }
                );
        }

        public async Task<IActionResult> ExternalLoginCallback(string remoteError, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            var loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error for external provider {remoteError}");
                return View("Login", loginViewModel);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information");
                return View("Login", loginViewModel);
            }

            var singInResult = await _signInManager.ExternalLoginSignInAsync(
                info.LoginProvider, info.ProviderKey, false, true
                );

            if (singInResult.Succeeded) return LocalRedirect(returnUrl);

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    user = new User
                    {
                        UserName = info.Principal.FindFirstValue(ClaimTypes.Name),
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };

                    await _userManager.CreateAsync(user);
                }

                await _userManager.AddLoginAsync(user, info);
                await _signInManager.SignInAsync(user, false);

                return LocalRedirect(returnUrl);
            }

            return View("Login", loginViewModel);
        }


        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new {returnUrl});

            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [HttpGet]
        public async Task<IActionResult> TwoFactorAuthentication(LoginViewModel model)
        {

            var user = await _userManager.FindByNameAsync(model.Email);
           
            var verificationCode = new VerificationCode
            {
                UserId = user.Id,
                AttemptsNumber = 3,
                Code = _verificationCodeGenerator.GenerateVerificationCode(
                    10000, 100000, DateTime.Now.Millisecond
                    ),
                SendingDate = DateTime.Now,
                NextSendingDate = DateTime.Now.AddHours(5),
                PhoneNumber = user.PhoneNumber
            };



            await _authMessageSender.SendSmsAsync("+" + user.PhoneNumber, verificationCode.Code.ToString());

            _verificationCodeRepository.Create(verificationCode);
            _repositoryWrapper.Save();
            

            ViewBag.Email= model.Email;
            ViewBag.Password = model.Password;
            ViewBag.ExternalLogins = model.ExternalLogins;
            ViewBag.ReturnUrl = model.ReturnUrl;
            ViewBag.RememberMe = model.RememberMe;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TwoFactorAuthentication(VerificationViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            var verificationCode = _verificationCodeRepository
                .FindByCondition(x => x.UserId == user.Id)
                .OrderByDescending(x => x.SendingDate)
                .FirstOrDefault();

            if (verificationCode == null)
                return RedirectToAction("Index", "Home");

            if (verificationCode.AttemptsNumber > 0)
            {
                verificationCode.AttemptsNumber--;
                if (verificationCode.SendingDate.AddMinutes(5) > DateTime.Now)
                {
                    if (verificationCode.Code == model.Code)
                    {
                        var result = await _signInManager.PasswordSignInAsync(
                            model.Email, model.Password, model.RememberMe, false
                            );
                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home");
                    }
                }
                _verificationCodeRepository.Update(verificationCode);
                _repositoryWrapper.Save();

            }
           
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    // проверяем, подтвержден ли email
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "Вы не подтвердили свой email");
                        return View(model);
                    }

                    if (user.TwoFactorAuthenticationEnabled)
                        return RedirectToAction("TwoFactorAuthentication", model);

                    var result = await _signInManager.PasswordSignInAsync(
                        model.Email, model.Password, model.RememberMe, false
                        );
                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}