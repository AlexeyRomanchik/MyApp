using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication.SignalR
{
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Receive", message);
        }
    }
}
