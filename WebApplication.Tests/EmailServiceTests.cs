using Logic.Services;
using Xunit;

namespace WebApplication.Tests
{
    public class EmailServiceTests
    {
        private const string ValidEmail = "romanchik_2014@inbox.ru";

        [Fact]
        public void SendEmail_Returned_True()
        {
            var emailService = new EmailService();

            var result = emailService.SendEmailAsync(
                ValidEmail, "subject", "some message"
                );

            result.Wait();

            Assert.True(result.IsCompletedSuccessfully);
        }

    }
}
