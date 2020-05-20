using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Logic.Services;
using Microsoft.AspNetCore.Identity;
using Models.User;
using Moq;
using WebApplication.Controllers;
using WebApplication.ViewModels;
using Xunit;

namespace WebApplication.Tests
{
    public class AccountControllerTests
    {
        [Fact]
        public void IncorrectUserRegister_Returned_False()
        {

            var registerViewModel = new RegisterViewModel();

            var accountController = new AccountController(null, null, null, null, null, null, null, null);

            var result = accountController.Register(registerViewModel);

            Assert.False(result.IsCompletedSuccessfully);
        }

    }
}
