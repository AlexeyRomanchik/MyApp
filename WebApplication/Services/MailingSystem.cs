using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Contracts;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class MailingSystem : IMailingSystem
    {
        private readonly IEmailService _emailService;
        public MailingSystem(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async void OrderMessage(Order order)
        {
            const string subject = "Заказ Store";
            var message = new StringBuilder();

            message.AppendLine($"<h3>Здравствуйте, {order.Customer.Name} {order.Customer.Surname}! </h3> ");
            message.AppendLine($"<p>Вы оформили заказ {order.Id} на сумму {order.Cart.GetFinalPrice()}</p>");
            message.AppendLine("<p>Корзина:</p>");

            foreach (var cartItem in order.Cart.CartItems)
            {
                message.AppendLine($"<p>Цена: {cartItem.Product.Name}</p>");
                message.AppendLine($"<p>Стоимость: {cartItem.Product.Price}</p>");
            }

            message.AppendLine("<p>Наши сотрудники свяжутся с вами:</p>");


            await _emailService.SendEmailAsync(order.Customer.Email, subject, message.ToString());
        }
    }
}
