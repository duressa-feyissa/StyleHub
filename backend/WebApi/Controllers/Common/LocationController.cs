using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTO.Common.Location.DTO;
using Application.Features.Common_Features.Location.Requests.Queries;
using Application.Features.Common_Features.Location.Requests.Commands;

namespace WebApi.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class LocationController : ControllerBase
	{
		private readonly IMediator _mediator;

		public LocationController(IMediator mediator)
		{
			_mediator = mediator;
		}


		[HttpGet]
		public async Task<ActionResult<List<LocationResponseDTO>>> fetchAllLocations()
		{
			var result = await _mediator.Send(new GetAllLocation());
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<LocationResponseDTO>> fetchLocationById(string id)
		{
			var result = await _mediator.Send(new GetLocationById { Id = id });
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<LocationResponseDTO>> CreateLocation([FromBody] CreateLocationRequest command)
		{
			var result = await _mediator.Send(command);

			return Ok(result);
		}

		[HttpPut]
		public async Task<ActionResult<LocationResponseDTO>> UpdateLocationRequest([FromBody] UpdateLocationRequest command)
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}


		[HttpDelete("{id}")]
		public async Task<ActionResult<LocationResponseDTO>> DeleteLocationRequest(string id)
		{
			var result = await _mediator.Send(new DeleteLocationRequest { Id = id });
			return Ok(result);
		}
	}
}