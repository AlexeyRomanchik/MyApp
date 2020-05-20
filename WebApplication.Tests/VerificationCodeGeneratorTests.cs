using System;
using Logic.Services;
using Models.Order;
using Models.Product;
using Xunit;

namespace WebApplication.Tests
{
    public class VerificationCodeGeneratorTests
    {
        [Fact] 
        public void CodesNotRepeat_Returned_True()
        {
            var verificationCodeGenerator = new VerificationCodeGenerator();

            var code = verificationCodeGenerator
                .GenerateVerificationCode(10000,100000 ,DateTime.Now.Millisecond);

            var nextCode = verificationCodeGenerator
                .GenerateVerificationCode(10000, 100000, DateTime.Now.Second);

            Assert.False(code == nextCode);
        }
    }
}
