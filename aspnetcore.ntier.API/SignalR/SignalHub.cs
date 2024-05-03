
using aspnetcore.ntier.DAL.Entities;
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

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try 
            {
                var connectionId = Context.ConnectionId;
                var userToRemove = ConnectedUsers.Ids.FirstOrDefault(x => x.Key == connectionId);
                ConnectedUsers.Ids.Remove(userToRemove);
                _logger.LogInformation("Connected Users {users}", ConnectedUsers.Ids);
            }
            catch
            {
                _logger.LogInformation("Error in Removing user. {1}", exception);
            }

        }


        public async Task Register(int userId)
        {
            try
            {
                var connectionId = Context.ConnectionId;
                ConnectedUsers.Ids.Add(connectionId.ToString(), userId.ToString());
                _logger.LogInformation("Connected Users {users}", ConnectedUsers.Ids);
            }
            catch
            {
                _logger.LogInformation("Error in Register with connectionId:{users}", userId);
            }

        }


        public async Task SendMessage(string targetUserId, string title)
        {
            var connectionId = Context.ConnectionId;
            var sender = ConnectedUsers.Ids.FirstOrDefault(x => x.Key == connectionId);
            var recipients = ConnectedUsers.Ids.Where(x => x.Value == targetUserId.ToString());
            _logger.LogInformation("Sender ID: {1} => Recipients: {@2}", sender.Key, recipients);
            foreach (var recipient in recipients)
            {
                await Clients.Client(recipient.Key).SendAsync("recieveMessage", sender.Value);
            }
        }


    }


}
