using System.Collections.Generic;
using AutoMapper;

namespace Shop.Api.Api.Model.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Data.Model.Product, Product>()
                .ForMember(p => p.Stock, exp => exp.MapFrom(m => m.Stock.Total))
                .ForMember(p => p.Stock, exp => exp.MapFrom(m => m.Stock.Reserved));

            CreateMap<List<Data.Model.Product>, ProductCollection>()
                .ForMember(c => c.Items, exp => exp.MapFrom(p => p))
                .ForMember(c => c.Total, exp => exp.MapFrom(p => p.Count));

            CreateMap<(List<Data.Model.Product> items, int total), ProductCollection>()
                .ForMember(c => c.Items, exp => exp.MapFrom(t => t.items))
                .ForMember(c => c.Total, exp => exp.MapFrom(t => t.total));

        }
    }
}
