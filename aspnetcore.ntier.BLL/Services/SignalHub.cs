using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;


namespace aspnetcore.ntier.BLL.Services
{
    public class SignalHub: Hub
    {

        private readonly ILogger<SignalHub> _logger;


        public SignalHub(ILogger<SignalHub> logger)
        {
            _logger = logger;
        }

        public async Task askServer(string name)
        {

            _logger.LogInformation("askServer:{name}", name);
            await Clients.All.SendAsync("askServerResponse", name);
        }

        public async Task sendNotification(string userId)
        {
            var userName = Context.ConnectionId;
            _logger.LogInformation("SignalR sendNotification:{name}", userName);
            await Clients.All.SendAsync("send", userId);
            /*            await Clients.Client(Context.ConnectionId).SendAsync("Send", message);*/
        }
    }
}
