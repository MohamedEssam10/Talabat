using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.Core;
using Talabat.Core.Repository.Contracts;
using Talabat.Core.Services.Contracts;
using Talabat.Repository;
using Talabat.Services;

namespace Talabat.API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection Services)
        {
            //builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            //builder.Services.AddScoped<IGenericRepository<Brand>, GenericRepository<Brand>>();
            //builder.Services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            //builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(typeof(MappingProfiles));
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddScoped<IUnitOfWork,  UnitOfWork>();
            Services.AddScoped<IOrderServices, OrderServices>();

            #region Error Handling
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0).SelectMany(P => P.Value.Errors).Select(E => E.ErrorMessage).ToList();

                    var response = new APIValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);

                };
            });
            #endregion

            return Services;
        }
    }
}
