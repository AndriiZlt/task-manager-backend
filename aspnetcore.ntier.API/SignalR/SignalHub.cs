
using Microsoft.AspNetCore.SignalR;




namespace aspnetcore.ntier.API
{

    public class SignalRHub : Hub
    {
        private readonly ILogger<SignalRHub> _logger;

        public SignalRHub( ILogger<SignalRHub> logger)
        {
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("status", "connected");

            await base.OnConnectedAsync();
        }


        public async Task Register(int userId)
        {
            var connectionId = Context.ConnectionId;
            try
            {
                ConnectedUsers.Ids.Add(userId, connectionId.ToString());
                _logger.LogInformation("Connected Users {users}", ConnectedUsers.Ids);
            }
            catch
            {
                _logger.LogInformation("Error in Register with connectionId:{users}", connectionId);
            }

        }


        public async Task SendMessage(string targetUserId, string title)
        {
            var senderUserId = Context.ConnectionId.ToString();
            var sender = ConnectedUsers.Ids.FirstOrDefault(x => x.Value == senderUserId);
            var recipient = ConnectedUsers.Ids.FirstOrDefault(x => x.Key.ToString() == targetUserId.ToString());
            _logger.LogInformation("Sender ID: {1} => Reciever ID: {2}", sender.Key, recipient.Key);
            await Clients.Client(recipient.Value).SendAsync("recieveMessage", sender.Key);

        }


    }


}
