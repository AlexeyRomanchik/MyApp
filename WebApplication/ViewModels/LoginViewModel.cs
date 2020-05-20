using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;

namespace WebApplication.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле Email обязательное")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Некорректный формат адреса почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле пароля обязательное")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Пароль должен быть не менее 8 символов")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public List<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

    }
}
