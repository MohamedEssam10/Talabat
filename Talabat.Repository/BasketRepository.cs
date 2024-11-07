using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contracts;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase database;

        public BasketRepository(IConnectionMultiplexer Redis)
        {
            database = Redis.GetDatabase();
        }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
            var Redisbasket = await database.StringGetAsync(BasketId);

            return Redisbasket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(Redisbasket.ToString());
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
        {
            var JsonBasket = JsonSerializer.Serialize(Basket);
            var IsDone = await database.StringSetAsync(Basket.Id, JsonBasket, TimeSpan.FromDays(1));

            if(!IsDone)  return null;

            return await GetBasketAsync(Basket.Id);
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await database.KeyDeleteAsync(BasketId);
        }
    }
}
