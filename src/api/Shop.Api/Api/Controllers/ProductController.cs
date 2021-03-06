﻿using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Api.Attributes;
using Shop.Api.Api.Model;
using Shop.Api.Api.Model.Mapping;
using Shop.Api.Business;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shop.Api.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductBusinessComponent _productBusinessComponent;
        private readonly IMapper _mapper;
        private readonly IProductSortExpressionMapper _productSortExpressionMapper;

        public ProductController(IProductBusinessComponent productBusinessComponent, IMapper mapper,
            IProductSortExpressionMapper productSortExpressionMapper)
        {
            _productBusinessComponent = productBusinessComponent;
            _mapper = mapper;
            _productSortExpressionMapper = productSortExpressionMapper;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, typeof(ProductCollection))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, typeof(ErrorResponse.InternalServerError))]
        public async Task<ActionResult> GetProducts(int page = 0, int pageSize = 0, string sort = null)
        {
            if (!_productSortExpressionMapper.ValidateSortExpression(sort))
                return BadRequest(ErrorResponse.Validation.InvalidSortExpression);

            if (pageSize > 0)
                return Ok(_mapper.Map<ProductCollection>(await _productBusinessComponent.GetProductsPaged(page, pageSize, sort)));

            return Ok(_mapper.Map<ProductCollection>(await _productBusinessComponent.GetProducts(sort)));
        }

        [HttpGet("{id:int}")]
        [SwaggerResponse(StatusCodes.Status200OK, typeof(Product))]
        [SwaggerResponse(StatusCodes.Status404NotFound, typeof(ErrorResponse.NotFound))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, typeof(ErrorResponse.InternalServerError))]
        public async Task<ActionResult> GetProduct(int id)
        {
            var result = _mapper.Map<Product>(await _productBusinessComponent.GetProductById(id));
            if (result == null) return NotFound(new ErrorResponse.NotFound());
            return Ok(result);
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, typeof(Product))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, typeof(ErrorResponse.Validation))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, typeof(ErrorResponse.InternalServerError))]
        [ValidateModel]
        public async Task<ActionResult> AddProduct([FromBody] NewProduct newProduct)
        {
            var product = _mapper.Map<Data.Model.Product>(newProduct);
            await _productBusinessComponent.AddProduct(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, _mapper.Map<Product>(product));
        }

        [HttpPut("{id:int}")]
        [SwaggerResponse(StatusCodes.Status200OK, typeof(Product))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, typeof(ErrorResponse.Validation))]
        [SwaggerResponse(StatusCodes.Status404NotFound, typeof(ErrorResponse.NotFound))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, typeof(ErrorResponse.InternalServerError))]
        [ValidateModel]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] UpdateProduct updateProduct)
        {
            var product = await _productBusinessComponent.GetProductById(id);
            if (product == null) return NotFound(new ErrorResponse.NotFound());

            _mapper.Map(updateProduct, product);
            await _productBusinessComponent.UpdateProduct(product);

            return Ok(_mapper.Map<Product>(product));
        }

        [HttpDelete("{id:int}")]
        [SwaggerResponse(StatusCodes.Status200OK, typeof(Product))]
        [SwaggerResponse(StatusCodes.Status404NotFound, typeof(ErrorResponse.NotFound))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, typeof(ErrorResponse.InternalServerError))]
        [ValidateModel]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productBusinessComponent.GetProductById(id);
            if (product == null) return NotFound(new ErrorResponse.NotFound());

            await _productBusinessComponent.DeleteProduct(product);

            return Ok(_mapper.Map<Product>(product));
        }

        [HttpPut("{id:int}/reserved")]
        [SwaggerResponse(StatusCodes.Status200OK, typeof(Product))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, typeof(ErrorResponse.Validation))]
        [SwaggerResponse(StatusCodes.Status404NotFound, typeof(ErrorResponse.NotFound))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, typeof(ErrorResponse.InternalServerError))]
        [ValidateModel]
        public async Task<ActionResult> UpdateProductReserved(int id, [FromBody] UpdateQuantity quantity)
        {
            var (product, errors) = await _productBusinessComponent.UpdateProductReserved(id, quantity.Quantity ?? 0);
            if (errors.IsValid) return Ok(_mapper.Map<Product>(product));
            return _mapper.Map<ActionResult>(errors);
        }
    }
}