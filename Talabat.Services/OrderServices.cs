using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order;
using Talabat.Core.Repository.Contracts;
using Talabat.Core.Services.Contracts;
using Talabat.Core.Specification.Contracts;
using Talabat.Core.Specification.Contracts.Order.Specifications;

namespace Talabat.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBasketRepository BasketRepo;

        public OrderServices(IBasketRepository BasketRepo, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) 
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.BasketRepo = BasketRepo;
        }

        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Core.Entities.Order.Address ShippingAddress)
        {
            var Basket = await BasketRepo.GetBasketAsync(BasketId);
            var OrderItems = new List<OrderItem>();

            if (Basket?.Items.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var Product = await unitOfWork.Repository<Product>().GetAsync(Id: item.Id);
                    var ProductItemOrdered = new ProductItemOrdered(Product.Id, Product.Name, Product.PictureUrl);
                    var OrderItem = new OrderItem(ProductItemOrdered, Product.Price, item.Quantity);

                    OrderItems.Add(OrderItem);
                }
            }

            var SubTotal = OrderItems.Sum(P => P.Price * P.Quantity);
            var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetAsync(DeliveryMethodId);

            var Order = new Order(BuyerEmail, ShippingAddress, deliveryMethod, OrderItems, SubTotal);

            await unitOfWork.Repository<Order>().AddAsync(Order);

            var Result = await unitOfWork.CompleteAsync();

            return Result > 0 ? Order : null;
        }


        public async Task<Order?> GetOrderByIdAsync(string BuyerEmail, int Id)
        {
            var Result = await unitOfWork.Repository<Order>().GetWithSpecAsync(new OrderSpecifications(BuyerEmail, Id));

            return Result;
        }

        public async Task<IReadOnlyList<Order>> GetOrderAsync(string BuyerEmail)
        {
            var Result = await unitOfWork.Repository<Order>().GetAllWithSpecAsync(new OrderSpecifications(BuyerEmail));

            return Result;
        }

    }
}
