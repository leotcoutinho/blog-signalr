using Microsoft.AspNetCore.SignalR;

namespace Datum.Blog.API.Hubs
{
    public class BlogHub : Hub
    { 
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        } 
    }
}
