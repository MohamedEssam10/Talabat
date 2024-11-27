using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Services.Contracts;

namespace Talabat.Services
{
    public class ResponseCacheServices : IResponseCacheServices
    {
        private readonly IDatabase database;
        public ResponseCacheServices(IConnectionMultiplexer Redis)
        {
            database = Redis.GetDatabase();
        }

        public async Task CacheResponseAsync(string CacheKey, object Response, TimeSpan ExpireTime)
        {
            if (Response is null) return;

            var options = new JsonSerializerOptions(){ 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var JsonResponse = JsonSerializer.Serialize(Response, options);

            await database.StringSetAsync(CacheKey, JsonResponse, ExpireTime);
        }

        public async Task<string?> GetCachedResponseAsync(string CacheKey)
        {
            var JsonResult = await database.StringGetAsync(CacheKey);

            if (JsonResult.IsNullOrEmpty) return null;

            return JsonResult;
        }
    }
}
