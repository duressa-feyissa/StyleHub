using Application.DTO.Product.BrandDTO.DTO;
using Application.Features.Product_Features.Brand.Requests.Commands;
using Application.Features.Product_Features.Brand.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<BrandResponseDTO>>> fetchAllBrands()
        {
            var result = await _mediator.Send(new GetAllBrand());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<BrandResponseDTO>> fetchBrandById(string id)
        {
            var result = await _mediator.Send(new GetBrandById { Id = id });
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<BrandResponseDTO>> CreateBrand(
            [FromBody] CreateBrandRequest command
        )
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<BrandResponseDTO>> UpdateBrandRequest(
            [FromBody] UpdateBrandRequest command
        )
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<BrandResponseDTO>> DeleteBrandRequest(string id)
        {
            var result = await _mediator.Send(new DeleteBrandRequest { Id = id });
            return Ok(result);
        }
    }
}
