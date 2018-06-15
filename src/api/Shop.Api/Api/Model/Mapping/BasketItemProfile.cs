using AutoMapper;

namespace Shop.Api.Api.Model.Mapping
{
    public class BasketItemProfile : Profile
    {
        public BasketItemProfile()
        {
            CreateMap<Data.Model.BasketItem, BasketItem>();
        }
    }
}