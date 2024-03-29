using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTO.Product.ColorDTO.DTO;
using Application.Features.Product_Features.Color.Requests.Queries;
using Application.Features.Product_Features.Color.Requests.Commands;

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
        public async Task<ActionResult<ColorResponseDTO>> fetchColorById(string id)
        {
            var result = await _mediator.Send(new GetColorById { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ColorResponseDTO>> CreateColor([FromBody] CreateColorRequest command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ColorResponseDTO>> UpdateColorRequest([FromBody] UpdateColorRequest command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ColorResponseDTO>> DeleteColorRequest(string id)
        {
            var result = await _mediator.Send(new DeleteColorRequest { Id = id });
            return Ok(result);
        }
    }
}