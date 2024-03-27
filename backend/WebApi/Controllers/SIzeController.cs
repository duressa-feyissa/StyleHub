using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTO.SizeDTO.DTO;
using Application.Features.Size.Requests.Commands;
using Application.Features.Size.Requests.Queries;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SizeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SizeController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<List<SizeResponseDTO>>> fetchAllSizes()
        {
            var result = await _mediator.Send(new GetAllSize());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SizeResponseDTO>> fetchSizeById(string id)
        {
            var result = await _mediator.Send(new GetSizeById { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<SizeResponseDTO>> CreateSize([FromBody] CreateSizeRequest command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<SizeResponseDTO>> UpdateSizeRequest([FromBody] UpdateSizeRequest command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<SizeResponseDTO>> DeleteSizeRequest(string id)
        {
            var result = await _mediator.Send(new DeleteSizeRequest { Id = id });
            return Ok(result);
        }
    }
}