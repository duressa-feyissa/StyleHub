using backend.Application.DTO.Common.Role.DTO;
using backend.Application.Features.Common_Features.Role.Requests.Commands;
using backend.Application.Features.Common_Features.Role.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.WebApi.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<RoleResponseDTO>>> fetchAllRoles()
        {
            var result = await mediator.Send(new GetAllRole());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<RoleResponseDTO>> fetchRoleById(string id)
        {
            var result = await mediator.Send(new GetRoleById { Id = id });
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<RoleResponseDTO>> CreateRole(
            [FromBody] CreateRoleRequest command
        )
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<RoleResponseDTO>> UpdateRoleRequest(
            [FromBody] UpdateRoleRequest command
        )
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<RoleResponseDTO>> DeleteRoleRequest(string id)
        {
            var result = await mediator.Send(new DeleteRoleRequest { Id = id });
            return Ok(result);
        }
    }
}
