using backend.Application.DTO.Product.DesignDTO.DTO;
using backend.Application.Features.Product_Features.Design.Requests.Commands;
using backend.Application.Features.Product_Features.Design.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.WebApi.Controllers.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class DesignController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<DesignResponseDTO>>> fetchAllDesigns()
        {
            var result = await mediator.Send(new GetAllDesign());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<DesignResponseDTO>> fetchDesignById(string id)
        {
            var result = await mediator.Send(new GetDesignById { Id = id });
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<DesignResponseDTO>> CreateDesign(
            [FromBody] CreateDesignRequest command
        )
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<DesignResponseDTO>> UpdateDesignRequest(
            [FromBody] UpdateDesignRequest command
        )
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<DesignResponseDTO>> DeleteDesignRequest(string id)
        {
            var result = await mediator.Send(new DeleteDesignRequest { Id = id });
            return Ok(result);
        }
    }
}
