using AutoMapper;

namespace Shop.Api.Api.Model.Mapping
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<Data.Model.Basket, Basket>();
        }
    }
}