using WebApplication.Models;

namespace WebApplication.Contracts
{
    public interface IMailingSystem
    {
        void OrderMessage(Order order);
    }
}