using Microsoft.AspNet.SignalR;

namespace SignalRChat
{
    public class ChatHub : Hub
    {
        public void BroadcastMessage(string userName, string message)
        {
            Clients.All.sendMessage(userName, message);
        }
    }
}