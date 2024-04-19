using backend.Application.DTO.Product.MaterialDTO.DTO;
using backend.Application.Features.Product_Features.Material.Requests.Commands;
using backend.Application.Features.Product_Features.Material.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.WebApi.Controllers.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<MaterialResponseDTO>>> fetchAllMaterials()
        {
            var result = await mediator.Send(new GetAllMaterial());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<MaterialResponseDTO>> fetchMaterialById(string id)
        {
            var result = await mediator.Send(new GetMaterialById { Id = id });
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<MaterialResponseDTO>> CreateMaterial(
            [FromBody] CreateMaterialRequest command
        )
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<MaterialResponseDTO>> UpdateMaterialRequest(
            [FromBody] UpdateMaterialRequest command
        )
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<MaterialResponseDTO>> DeleteMaterialRequest(string id)
        {
            var result = await mediator.Send(new DeleteMaterialRequest { Id = id });
            return Ok(result);
        }
    }
}
