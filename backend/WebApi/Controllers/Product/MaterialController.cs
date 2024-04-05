using Application.DTO.Product.MaterialDTO.DTO;
using Application.Features.Product_Features.Material.Requests.Commands;
using Application.Features.Product_Features.Material.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MaterialController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<MaterialResponseDTO>>> fetchAllMaterials()
        {
            var result = await _mediator.Send(new GetAllMaterial());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<MaterialResponseDTO>> fetchMaterialById(string id)
        {
            var result = await _mediator.Send(new GetMaterialById { Id = id });
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<MaterialResponseDTO>> CreateMaterial(
            [FromBody] CreateMaterialRequest command
        )
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<MaterialResponseDTO>> UpdateMaterialRequest(
            [FromBody] UpdateMaterialRequest command
        )
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<MaterialResponseDTO>> DeleteMaterialRequest(string id)
        {
            var result = await _mediator.Send(new DeleteMaterialRequest { Id = id });
            return Ok(result);
        }
    }
}
