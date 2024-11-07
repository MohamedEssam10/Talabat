using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order;

namespace Talabat.Core.Services.Contracts
{
    public interface IOrderServices
    {
        public Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress);
        public Task<Order?> GetOrderByIdAsync(string BuyerEmail, int Id);
        public Task<IReadOnlyList<Order>> GetOrderAsync(string BuyerEmail);
    }
}
