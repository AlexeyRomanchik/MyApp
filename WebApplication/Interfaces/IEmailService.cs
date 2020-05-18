using System.Threading.Tasks;

namespace WebApplication.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
