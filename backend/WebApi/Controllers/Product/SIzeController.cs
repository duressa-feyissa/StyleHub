using backend.Application.DTO.Product.SizeDTO.DTO;
using backend.Application.Features.Product_Features.Size.Requests.Commands;
using backend.Application.Features.Product_Features.Size.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.WebApi.Controllers.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class SizeController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<SizeResponseDTO>>> fetchAllSizes()
        {
            var result = await mediator.Send(new GetAllSize());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<SizeResponseDTO>> fetchSizeById(string id)
        {
            var result = await mediator.Send(new GetSizeById { Id = id });
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<SizeResponseDTO>> CreateSize(
            [FromBody] CreateSizeRequest command
        )
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<SizeResponseDTO>> UpdateSizeRequest(
            [FromBody] UpdateSizeRequest command
        )
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<SizeResponseDTO>> DeleteSizeRequest(string id)
        {
            var result = await mediator.Send(new DeleteSizeRequest { Id = id });
            return Ok(result);
        }
    }
}
