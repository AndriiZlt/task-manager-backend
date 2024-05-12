
using aspnetcore.ntier.DAL.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;




namespace aspnetcore.ntier.API
{

    public class SignalRHub : Hub
    {
        private readonly ILogger<SignalRHub> _logger;
        private readonly ConnectedUsers _connectedUsers;

        public SignalRHub( ILogger<SignalRHub> logger, ConnectedUsers connectedUsers)
        {
            _logger = logger;
            _connectedUsers = connectedUsers;
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
/*                Lazy<string> userId ;*/
                /*                var userToRemove = ConnectedUsers.Ids.TryGetValue(connectionId, out userId);*/
                Lazy<string> removedUser;
                ConnectedUsers.Ids.TryRemove(connectionId, out removedUser);
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
                var lazyUserId = new Lazy<string>(() => { return userId.ToString(); });
                var connectionId = Context.ConnectionId;
                ConnectedUsers.Ids.TryAdd(connectionId.ToString(), lazyUserId);
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
            Lazy<string> sender;
            ConnectedUsers.Ids.TryGetValue(connectionId, out sender);
            var recipients = _connectedUsers.GetUsers(targetUserId);
            
/*            _logger.LogInformation("Sender ID: {1} => Recipients: {2}", sender.Value, recipients.Keys);*/
            foreach (var recipient in recipients)

            {
                _logger.LogInformation("recipient:{@users}", recipient);
                await Clients.Client(recipient.Key).SendAsync("recieveMessage", sender.Value);
            }
        }


    }


}
