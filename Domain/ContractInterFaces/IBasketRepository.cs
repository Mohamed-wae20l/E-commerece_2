using Domain.Models.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ContractInterFaces
{
   public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string key);
        Task<CustomerBasket?> CreateOrUpdateBAsketAsync(CustomerBasket customerbasket,TimeSpan?TimetoLive=null);
        Task<bool> DeleteBAsketAsync(string key);
    }
}
