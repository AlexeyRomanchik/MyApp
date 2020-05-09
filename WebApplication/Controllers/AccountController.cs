using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Contracts;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _appDbContext;
        private readonly IEmailService _emailService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager,
            ApplicationContext appDbContext, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appDbContext = appDbContext;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                    // проверяем, подтвержден ли email
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "Вы не подтвердили свой email");
                        return View(model);
                    }

                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false
                    );
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
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