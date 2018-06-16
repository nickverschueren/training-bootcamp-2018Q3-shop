using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Business;

namespace Shop.Api.Api.Model.Mapping
{
    public class BusinessErrorProfile : Profile
    {
        public BusinessErrorProfile()
        {
            CreateMap<BusinessErrorCollection, ActionResult>()
                .ConstructUsing((collection, context) =>
                {
                    if (collection.HasNotFound)
                        return context.Mapper.Map<NotFoundObjectResult>(collection);
                    return context.Mapper.Map<ConflictObjectResult>(collection);
                });

            CreateMap<BusinessErrorCollection, NotFoundObjectResult>()
                .ConstructUsing(collection => new NotFoundObjectResult(
                    new ErrorResponse.NotFound(collection.FirstOrDefault()?.Message)));

            CreateMap<BusinessErrorCollection, ConflictObjectResult>()
                .ConstructUsing((collection, context) => new ConflictObjectResult(new ErrorResponse.Conflict
                {
                    Errors = context.Mapper.Map<List<ErrorResponse.Conflict.ConflictItem>>(collection)
                }));

            CreateMap<BusinessError, ErrorResponse.Conflict.ConflictItem>();
        }
    }
}
