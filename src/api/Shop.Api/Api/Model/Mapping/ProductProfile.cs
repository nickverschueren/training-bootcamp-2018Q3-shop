using System;
using System.Collections.Generic;
using AutoMapper;
using Shop.Api.Data.Model;

namespace Shop.Api.Api.Model.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Data.Model.Product, Product>()
                .ForMember(p => p.Image, exp => exp.MapFrom(p => p.ImageUri))
                .ForMember(p => p.Desc, exp => exp.MapFrom(p => p.Description))
                .ForMember(p => p.Stock, exp => exp.MapFrom(p => p.Stock.Total))
                .ForMember(p => p.Reserved, exp => exp.MapFrom(p => p.Stock.Reserved));

            CreateMap<List<Data.Model.Product>, ProductCollection>()
                .ForMember(c => c.Items, exp => exp.MapFrom(l => l))
                .ForMember(c => c.Total, exp => exp.MapFrom(l => l.Count));

            CreateMap<(List<Data.Model.Product> items, int total), ProductCollection>()
                .ForMember(c => c.Items, exp => exp.MapFrom(t => t.items))
                .ForMember(c => c.Total, exp => exp.MapFrom(t => t.total));

            CreateMap<NewProduct, Data.Model.Product>()
                .ForMember(p => p.Price, exp => exp.MapFrom(np => np.Price ?? np.BasePrice))
                .ForMember(p => p.Description, exp => exp.MapFrom(np => np.Desc))
                .ForMember(p => p.ImageUri, exp => exp.MapFrom(np => np.Image))
                .ForMember(p => p.Stock, exp => exp.MapFrom(np => new Stock
                {
                    Total = np.Stock ?? 0
                }));

            CreateMap<UpdateProduct, Data.Model.Product>()
                .ForMember(p => p.Price, exp => exp.MapFrom(np => np.Price ?? np.BasePrice))
                .ForMember(p => p.Description, exp => exp.MapFrom(np => np.Desc))
                .ForMember(p => p.ImageUri, exp => exp.MapFrom(np => np.Image))
                .ForMember(p => p.Stock, exp => exp.Ignore())
                .AfterMap((up, p) =>
                {
                    p.Stock.Total = up.Stock ?? 0;
                });
        }
    }
}
