

using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;

namespace aspnetcore.ntier.API
{
    public class ConnectedUsers
    {

        private readonly IMemoryCache _memoryCache;
        private const string KEY = "user_cache";

        public static ConcurrentDictionary<string, Lazy<string>> Ids = new ConcurrentDictionary<string, Lazy<string>>();

        public ConnectedUsers(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        
        public void AddToCashe(ConcurrentDictionary<string, Lazy<string>> IDs)
        {
            var options = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(10),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
            };

            _memoryCache.Set<ConcurrentDictionary<string, Lazy<string>>>(KEY,IDs, options);
        }

        public IEnumerable<KeyValuePair<string, Lazy<string>>> GetUsers(string targetUserId)
        {

            var cachedUsers = _memoryCache.Get<ConcurrentDictionary<string, Lazy<string>>>(KEY);
            if (cachedUsers is null)
            {
                var recipients = Ids.Where(x => x.Value.ToString() == targetUserId);
                AddToCashe(Ids);
                return recipients;
            }
            return cachedUsers.Where(u => u.Value.ToString() == targetUserId.ToString());
            ;
        }
    }
}
