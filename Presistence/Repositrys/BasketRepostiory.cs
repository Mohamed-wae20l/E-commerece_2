using Domain.ContractInterFaces;
using Domain.Models.Basket;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositrys
{
    public class BasketRepostiory(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<CustomerBasket?> GetBasketAsync(string key)
        {
            var Basket = await _database.StringGetAsync(key);

            if(Basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> CreateOrUpdateBAsketAsync(CustomerBasket customerbasket, TimeSpan? TimetoLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(customerbasket);
            var IsCreatedOrUpdated = await _database.StringSetAsync(customerbasket.Id, JsonBasket, TimetoLive ?? TimeSpan.FromDays(30));
            if (IsCreatedOrUpdated)
                return await GetBasketAsync(customerbasket.Id);
            else
                return null;
        }

        public async Task<bool> DeleteBAsketAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

       
    }
}
