using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.API.DTOs;
using Talabat.API.Errors;
using Talabat.Core;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order;
using Talabat.Core.Services.Contracts;
using Talabat.Repository;

namespace Talabat.API.Controllers
{

    public class OrderController : BaseAPIController
    {
        private readonly IOrderServices orderServices;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork unitOfWork;

        public OrderController(IOrderServices orderServices, IMapper mapper, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            this.orderServices = orderServices;
            this.mapper = mapper;
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status400BadRequest)]

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> Create(OrderDto orderDto)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var address = mapper.Map<AddressToReturnDto, Core.Entities.Order.Address>(orderDto.ShippingAddress);
            var Result = await orderServices.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, address);

            return Result is null ? BadRequest(new APIResponse(400, "There is a problem in ur order")) : Ok(Result);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersOfUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var Result = await orderServices.GetOrderAsync(userEmail);

            return Result is null ? NotFound(new APIResponse(404)) : Ok(Result);
        }

        [Authorize]
        [HttpGet("{Id}")]
        public async Task<ActionResult<Order>> GetOrderOfUser(int Id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var Result = await orderServices.GetOrderByIdAsync(userEmail,Id);

            return Result is null ? NotFound(new APIResponse(404)) : Ok(Result);
        }

        [Authorize]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var Result = await unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return Result is null ? NotFound(new APIResponse(404)) : Ok(Result);
        }
    }
}
