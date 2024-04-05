using Application.DTO.Product.ColorDTO.DTO;
using Application.Features.Product_Features.Color.Requests.Commands;
using Application.Features.Product_Features.Color.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ColorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ColorResponseDTO>>> fetchAllColors()
        {
            var result = await _mediator.Send(new GetAllColor());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ColorResponseDTO>> fetchColorById(string id)
        {
            var result = await _mediator.Send(new GetColorById { Id = id });
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ColorResponseDTO>> CreateColor(
            [FromBody] CreateColorRequest command
        )
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ColorResponseDTO>> UpdateColorRequest(
            [FromBody] UpdateColorRequest command
        )
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ColorResponseDTO>> DeleteColorRequest(string id)
        {
            var result = await _mediator.Send(new DeleteColorRequest { Id = id });
            return Ok(result);
        }
    }
}
