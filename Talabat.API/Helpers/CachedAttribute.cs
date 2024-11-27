using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Core.Services.Contracts;

namespace Talabat.API.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int ExpireTimeInSeconds;

        public CachedAttribute(int ExpireTimeInSeconds)
        {
            this.ExpireTimeInSeconds = ExpireTimeInSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Ask CLR to create an instance Explicitly
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheServices>();

            var CacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cacheResponse = await cacheService.GetCachedResponseAsync(CacheKey);

            if (!string.IsNullOrEmpty(cacheResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;
                return;
            }

            var ExecutedEndPoint = await next.Invoke(); // will execute the EndPoint

            if (ExecutedEndPoint.Result is OkObjectResult result)
            {
                await cacheService.CacheResponseAsync(CacheKey, result, TimeSpan.FromSeconds(ExpireTimeInSeconds));
            }

        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuiler = new StringBuilder();

            keyBuiler.Append(request.Path);
             
            foreach (var item in request.Query.OrderBy(Q => Q.Key))
            {
                keyBuiler.Append($"|{item.Key}-{item.Value}");
            }

            return keyBuiler.ToString();
        }
    }
}
