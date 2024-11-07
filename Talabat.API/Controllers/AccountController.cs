using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.API.DTOs;
using Talabat.API.Errors;
using Talabat.API.Extensions;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contracts;

namespace Talabat.API.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper Mapper;
        private readonly ITokenService tokenService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper Mapper, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.Mapper = Mapper;
            this.tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if (CheckEmailExists(registerDto.Email).Result)
                return BadRequest(new APIResponse(400, "Email Exists"));

            var appUser = new ApplicationUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DispalyName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split('@')[0],
            };

            var Result = await userManager.CreateAsync(appUser, registerDto.Password);

            if(!Result.Succeeded) return BadRequest(new APIResponse(400));

            return Ok(new UserDto()
            {
                DisplayName = appUser.DisplayName,
                Email = appUser.Email ?? string.Empty,
                Token =await tokenService.CreateTokenAsync(appUser,userManager)
            });
        }

        [HttpPost("LogIn")]
        public async Task<ActionResult<UserDto>> LogIn(LogInUser logInUser)
        {
            var user = await userManager.FindByEmailAsync(logInUser.Email);

            if (user == null) return Unauthorized(new APIResponse(401));

            var Result = await signInManager.CheckPasswordSignInAsync(user,logInUser.Password,false);
            if (!Result.Succeeded) return Unauthorized(new APIResponse(401));

            return Ok(new UserDto()
            {
                Email= user.Email,
                DisplayName = user.DisplayName,
                Token =await tokenService.CreateTokenAsync(user,userManager)
            });

        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await tokenService.CreateTokenAsync(user,userManager)
            });
        }

        [Authorize]
        [HttpGet("GetUserAddress")]
        public async Task<ActionResult<AddressToReturnDto>> GetUserAddress()
        {
            //var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.FindUserWithAddressAsync(User);

            var addressToReturnDto = new AddressToReturnDto()
            { 
                City = user.Address.City,
                Country = user.Address.Country,
                Username = user.Address.Username,
                Street = user.Address.Street
            };

            return Ok(addressToReturnDto);
        }

        [Authorize]
        [HttpPost("UpadteAddress")]
        public async Task<ActionResult<AddressToReturnDto>> UpdateAddressAsync(AddressToReturnDto address)
        {
            
            var user = await userManager.FindUserWithAddressAsync(User);

            var UpdatedAddress = Mapper.Map<Address>(address);
            UpdatedAddress.Id = user.Address.Id;
            user.Address = UpdatedAddress;
            
            var Result = await userManager.UpdateAsync(user);

            if (!Result.Succeeded) return BadRequest(new APIResponse(500));

            return Ok(Mapper.Map<AddressToReturnDto>(user.Address));
        }

        [HttpGet("EmailExists")]
        public async Task<bool> CheckEmailExists(string Email)
         => await userManager.FindByEmailAsync(Email) == null ? false : true;
        
    }
}
