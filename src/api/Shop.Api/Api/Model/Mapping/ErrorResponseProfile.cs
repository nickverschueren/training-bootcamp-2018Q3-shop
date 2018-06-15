using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shop.Api.Api.Model.Mapping
{
    public class ErrorResponseProfile : Profile
    {
        public ErrorResponseProfile()
        {
            CreateMap<ModelStateDictionary, ErrorResponse.Validation>()
                .ForMember(v => v.Errors, exp => exp.MapFrom(m =>
                    m.Where(v => v.Value.ValidationState == ModelValidationState.Invalid)
                        .SelectMany(ms => ms.Value.Errors.Select(e =>
                            new ErrorResponse.Validation.ValidationItem
                            {
                                Key = ms.Key,
                                Message = e.ErrorMessage
                            }).ToList())));
        }
    }
}
