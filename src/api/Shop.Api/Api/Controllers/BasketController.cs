using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Api.Attributes;
using Shop.Api.Api.Model;
using Shop.Api.Business;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shop.Api.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/basket")]
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketBusinessComponent _basketBusinessComponent;
        private readonly IProductBusinessComponent _productBusinessComponent;
        private readonly IMapper _mapper;

        public BasketController(IBasketBusinessComponent basketBusinessComponent,
            IProductBusinessComponent productBusinessComponent, IMapper mapper)
        {
            _basketBusinessComponent = basketBusinessComponent;
            _productBusinessComponent = productBusinessComponent;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, typeof(Basket))]
        [SwaggerResponse(StatusCodes.Status404NotFound, typeof(ErrorResponse.NotFound))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, typeof(ErrorResponse.InternalServerError))]
        public async Task<ActionResult> GetBasket()
        {
            var userId = User.Identity.Name;
            var result = _mapper.Map<Basket>(await _basketBusinessComponent.GetBasketByUserId(userId));
            if (result == null) return NotFound(new ErrorResponse.NotFound());
            return Ok(result);
        }


        [HttpPost("products/{id:int}")]
        [SwaggerResponse(StatusCodes.Status201Created, typeof(Basket))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, typeof(ErrorResponse.Validation))]
        [SwaggerResponse(StatusCodes.Status404NotFound, typeof(ErrorResponse.NotFound))]
        [SwaggerResponse(StatusCodes.Status409Conflict, typeof(ErrorResponse.Conflict))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, typeof(ErrorResponse.InternalServerError))]
        [ValidateModel]
        public async Task<ActionResult> AddProduct(int id, [FromBody] ItemQuantity itemQuantity)
        {
            var userId = User.Identity.Name;
            var (basket, errors) = await _basketBusinessComponent.AddProductToBasket(userId, id, itemQuantity.Quantity);
            if (errors == null) return NotFound(new ErrorResponse.NotFound());
            if (!errors.IsValid) return Conflict(_mapper.Map<ErrorResponse.Conflict>(errors));
            return Ok(_mapper.Map<Basket>(basket));
        }


    }
}