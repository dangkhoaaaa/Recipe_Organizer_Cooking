using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string recipientUserId, string message)
        {
            string senderUserId = Context.UserIdentifier;

            // Perform any necessary processing or validation with the message data

            // Send the message to the recipient user
            await Clients.User(recipientUserId).SendAsync("ReceiveMessage", senderUserId, message);

            // Perform any other desired actions, such as saving the message to a database
        }

        public override async Task OnConnectedAsync()
        {
            string userId = Context.UserIdentifier;

            // Perform any necessary logic when a client is connected
            // For example, update user status, add user to a group, etc.

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string userId = Context.UserIdentifier;

            // Perform any necessary logic when a client is disconnected
            // For example, update user status, remove user from a group, etc.

            await base.OnDisconnectedAsync(exception);
        }
    }
}
