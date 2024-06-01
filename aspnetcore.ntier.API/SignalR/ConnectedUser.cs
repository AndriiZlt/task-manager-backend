
using Microsoft.Extensions.Caching.Memory;
using NuGet.Protocol.Plugins;


namespace aspnetcore.ntier.API
{
    public class ConnectionService : IConnectionService
    {
        private readonly IMemoryCache _memoryCache;

        public ConnectionService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }


        public List<string> AddToCashe(string userId, string connectionId)
        {
            List<string> connectionList = _memoryCache.Get<List<string>>(userId);
            if(connectionList != null)
            {
                if (connectionList.Find(x => x.Contains(connectionId)) != null)
                {
                    connectionList.Add("ConnectionService_" + connectionId);
                    setValue(userId, connectionList);
                    setValue(connectionId, userId);
                } 
            }
            else
            {
                var newValue = new List<string>
                {
                    "ConnectionService_" + connectionId
                };

                setValue(userId, newValue);
            }

            return connectionList;

        }

        public void setValue(string key, object value)
        {
            var options = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(60),
                /* AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(100)*/
            };

            _memoryCache.Set(key, value, options);
        }

        public List<string> GetUserConnections(string userId)
        {

            List<string> userConnections = _memoryCache.Get<List<string>>(userId);

            return userConnections;
        }

        public string GetValue(string key)
        {
            return _memoryCache.Get<string>(key);
        }

        public string ClearConnections(string connectionId)
        {
            var userId = _memoryCache.Get<string>(connectionId);
            _memoryCache.Remove(connectionId);
            _memoryCache.Remove(userId);
            return userId;
        }


    }
}
