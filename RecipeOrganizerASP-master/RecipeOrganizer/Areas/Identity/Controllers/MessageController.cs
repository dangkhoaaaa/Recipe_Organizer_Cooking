using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Services.Services;

namespace RecipeOrganizer.Areas.Identity.Controllers
{
    public class MessageController : Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string userId, string message)
        {
            // Perform any necessary processing or validation with the message data

            // Send the message using SignalR
            await _hubContext.Clients.User(userId).SendAsync("ReceiveMessage", message);

            // Perform any other desired actions

            return RedirectToAction("Chat"); // Redirect back to the chat view
        }

        [HttpGet]
        public IActionResult Chat()
        {
            return View();
        }
    }
}
