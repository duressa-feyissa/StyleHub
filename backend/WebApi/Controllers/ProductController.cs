using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTO.ProductDTO.DTO;
using Application.Features.Product.Requests.Commands;
using Application.Features.Product.Requests.Queries;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProductResponseDTO>>> fetchAllProducts()
        {
            var result = await _mediator.Send(new GetAllProduct());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDTO>> fetchProductById(string id)
        {
            var result = await _mediator.Send(new GetProductById { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponseDTO>> CreateProduct([FromBody] CreateProductRequest command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductResponseDTO>> DeleteProductRequest(string id)
        {
            var result = await _mediator.Send(new DeleteProductRequest { Id = id });
            return Ok(result);
        }
    }
}