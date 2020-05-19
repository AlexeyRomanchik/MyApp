using Models.Order;

namespace Logic.Interfaces
{
    public interface IMailingSystem
    {
        void OrderMessage(Order order);
    }
}