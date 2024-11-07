using AutoMapper;
using Talabat.API.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order;

namespace Talabat.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {


            CreateMap<Product, ProductToReturnDto>()
                .ForMember(destination => destination.Brand, Source => Source.MapFrom(P => P.Brand.Name))
                .ForMember(destination => destination.Category, Source => Source.MapFrom(P => P.Category.Name))
                //.ForMember(destination => destination.PictureUrl, Source => Source.MapFrom(P => $"{"https://localhost:7023/"}{P.PictureUrl}"));
                .ForMember(destination => destination.PictureUrl, Source => Source.MapFrom<ProductPictureURLResolver>());

            CreateMap<Core.Entities.Identity.Address, AddressToReturnDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressToReturnDto, Core.Entities.Order.Address>();


        }
    }
}
