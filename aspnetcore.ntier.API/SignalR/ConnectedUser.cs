

using Microsoft.Extensions.Caching.Memory;

namespace aspnetcore.ntier.API
{
    public class ConnectedUsers
    {
        private readonly IMemoryCache _memoryCache;
        public ConnectedUsers(IMemoryCache _memoryCache)
        {
            _memoryCache = _memoryCache;
        }



        public static IDictionary<string, string> Ids = new Dictionary<string, string>();

    }
}
