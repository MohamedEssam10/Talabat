using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs;
using Talabat.API.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repository.Contracts;

namespace Talabat.API.Controllers
{
    public class BasketController : BaseAPIController
    {
        private readonly IBasketRepository basketRepo;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository BasketRepo, IMapper mapper)
        {
            basketRepo = BasketRepo;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string Id)
        {
            var basket = await basketRepo.GetBasketAsync(Id);

            return basket is null ? new CustomerBasket(Id) : Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket Basket)
        { 
            //var MappedBasket = mapper.Map<CustomerBasketDto, CustomerBasket>(Basket);
            var UpdatedOrCreatedBasket = await basketRepo.UpdateBasketAsync(Basket);
            return UpdatedOrCreatedBasket is null ? BadRequest(new APIResponse(400)) : Ok(UpdatedOrCreatedBasket);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string Id)
        {
            return await basketRepo.DeleteBasketAsync(Id);
        }
    }
}
