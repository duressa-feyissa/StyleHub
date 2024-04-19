using backend.Application.DTO.Common.Location.DTO;
using backend.Application.Features.Common_Features.Location.Requests.Commands;
using backend.Application.Features.Common_Features.Location.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.WebApi.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<LocationResponseDTO>>> fetchAllLocations()
        {
            var result = await mediator.Send(new GetAllLocation());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<LocationResponseDTO>> fetchLocationById(string id)
        {
            var result = await mediator.Send(new GetLocationById { Id = id });
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<LocationResponseDTO>> CreateLocation(
            [FromBody] CreateLocationRequest command
        )
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<LocationResponseDTO>> UpdateLocationRequest(
            [FromBody] UpdateLocationRequest command
        )
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<LocationResponseDTO>> DeleteLocationRequest(string id)
        {
            var result = await mediator.Send(new DeleteLocationRequest { Id = id });
            return Ok(result);
        }
    }
}
