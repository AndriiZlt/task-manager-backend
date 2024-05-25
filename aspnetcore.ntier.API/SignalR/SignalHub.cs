
using Microsoft.AspNetCore.SignalR;




namespace aspnetcore.ntier.API
{

    public class SignalRHub : Hub
    {
        private readonly ILogger<SignalRHub> _logger;
        private readonly IConnectionService _connectionService;

        public SignalRHub( ILogger<SignalRHub> logger, IConnectionService connectionService)
        {
            _logger = logger;
            _connectionService = connectionService;
        }

        public override async Task OnConnectedAsync()
        {
            var currentConnectionId = Context.ConnectionId;
            _logger.LogInformation("Connections started for {s}", currentConnectionId);
            await Clients.Client(currentConnectionId.ToString()).SendAsync("status", "connected");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try 
            {
                var currentConnectionId = Context.ConnectionId;
                string userId = _connectionService.ClearConnections(currentConnectionId);
                _logger.LogInformation("User {connectionId} disconnected", userId);
            }
            catch
            {
                _logger.LogInformation("Error in Removing user. {1}", exception);
            }

        }

        public async Task Register(int userId)
        {
            var currentConnectionId = Context.ConnectionId;
            try
            {
                List<string> updatedUserConnections = _connectionService.AddToCashe(userId.ToString(), currentConnectionId);
                _logger.LogInformation("User {userId} connections: {@response}", userId,updatedUserConnections);
            }
            catch
            {
                _logger.LogInformation($"Error in Register user {userId} with connectionId {currentConnectionId}");
            }

        }

        public async Task SendMessage(string targetUserId, string title)
        {
            List<string> targetUserConnections = _connectionService.GetUserConnections(targetUserId);
            var currentConnectionId = Context.ConnectionId;
            var senderId = _connectionService.GetValue(currentConnectionId);
            _logger.LogInformation("Target userId:{id}. Connections:{@connections}", targetUserId,targetUserConnections);

            if (targetUserConnections != null)
            {
                foreach (var connection in targetUserConnections)
                {
                    string connection_id = connection.Split('_')[1];
                    await Clients.Client(connection_id).SendAsync("recieveMessage", $"User with ID:{senderId} created task for you with title:'{title}'!");
                }
            }

        }
    }
}
