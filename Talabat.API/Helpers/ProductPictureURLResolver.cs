using AutoMapper;
using Talabat.API.DTOs;
using Talabat.Core.Entities;

namespace Talabat.API.Helpers
{
    public class ProductPictureURLResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureURLResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{configuration["APIBaseUrl"]}/{source.PictureUrl}";

            return string.Empty;
        }
    }
}
