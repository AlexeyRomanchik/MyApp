using System.Threading.Tasks;

namespace WebApplication.Contracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
