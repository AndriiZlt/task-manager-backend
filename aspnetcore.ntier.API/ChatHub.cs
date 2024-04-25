using aspnetcore.ntier.BLL.Services;
using aspnetcore.ntier.BLL.Services.IServices;
using Microsoft.AspNetCore.SignalR;
using System.Net.NetworkInformation;

namespace aspnetcore.ntier.API
{
    public class ChatHub: Hub
    {

        private readonly ILogger<ChatHub> _logger;


        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        public async Task askServer(string name)
        {

            _logger.LogInformation("In CHatHub:{name}", name);
            await Clients.All.SendAsync("askServerResponse", name);
        }
    }
}
