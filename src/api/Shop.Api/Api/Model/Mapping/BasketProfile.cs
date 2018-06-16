using System.Linq;
using AutoMapper;

namespace Shop.Api.Api.Model.Mapping
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<Data.Model.Basket, Basket>()
                .ForMember(b => b.Items, exp => exp.MapFrom(b =>
                    b.Items == null ? null : b.Items.OrderBy(i => i.Id)));
        }
    }
}