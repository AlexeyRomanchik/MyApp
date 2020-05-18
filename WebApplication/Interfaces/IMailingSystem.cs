using Models.Order;

namespace WebApplication.Interfaces
{
    public interface IMailingSystem
    {
        void OrderMessage(Order order);
    }
}