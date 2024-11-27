using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Services.Contracts
{
    public interface IResponseCacheServices
    {
        public Task CacheResponseAsync(string CacheKey, object Response, TimeSpan ExpireTime);
        public Task<string?> GetCachedResponseAsync(string CacheKey);


    }
}
